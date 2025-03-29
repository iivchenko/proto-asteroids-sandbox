using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Engine.Assets.SourceGenerators;

/* Links
 * https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.cookbook.md#additional-file-transformation
 * https://andrewlock.net/creating-a-source-generator-part-1-creating-an-incremental-source-generator/
 * https://posts.specterops.io/dotnet-source-generators-in-2024-part-1-getting-started-76d619b633f5
 * https://www.youtube.com/watch?v=Yf8t7GqA6zA
*/
[Generator]
public sealed class AssetStaticPathGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var rootProvider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (node, _) =>
                (node is ClassDeclarationSyntax @class) &&
                @class
                    .Modifiers
                    .Any(modifier => modifier.IsKind(SyntaxKind.StaticKeyword)) &&
                @class
                    .AttributeLists
                    .SelectMany(attrList => attrList.Attributes)
                    .Select(attr => attr.Name.ToString())
                    .Any(attr => 
                        attr == typeof(AssetsAttribute).Name || 
                        attr == typeof(AssetsAttribute).Name.Substring(0, typeof(AssetsAttribute).Name.IndexOf("Attribute")) ||
                        $"{attr}Attribute" == typeof(AssetsAttribute).Name)
            ,
            transform: (ctx, _) =>
            {
                var @class = (ClassDeclarationSyntax)ctx.Node;
                var @attribute =
                    @class
                        .AttributeLists
                        .SelectMany(attrList => attrList.Attributes)
                        .First(attr =>
                        {
                            var name = attr.Name.ToString();

                            return
                                name == typeof(AssetsAttribute).Name ||
                                name == typeof(AssetsAttribute).Name.Substring(0, typeof(AssetsAttribute).Name.IndexOf("Attribute")) ||
                                $"{name}Attribute" == typeof(AssetsAttribute).Name;
                        });

                return (@class, attribute);
            });

        var assetsProviders =
            context
                .AdditionalTextsProvider
                .Select((asset, _) => asset.Path)
                .Collect();       

        var compilationProvider = context.CompilationProvider.Combine(rootProvider.Collect().Combine(assetsProviders));

        context.RegisterSourceOutput(compilationProvider, Execute);
    }

    private void Execute(SourceProductionContext context, (Compilation Left, (System.Collections.Immutable.ImmutableArray<(ClassDeclarationSyntax @class, AttributeSyntax attribute)> Left, System.Collections.Immutable.ImmutableArray<string> Right) Right) tuple)
    {
        var (_, (classes, paths)) = tuple;

        foreach (var (@class, @attribute) in classes)
        {
            var classPath = Path.GetDirectoryName(@class.SyntaxTree.FilePath);
            var classDirectory = new DirectoryInfo(classPath).Name;
            var namespaceSyntax = @class.Parent as BaseNamespaceDeclarationSyntax;  

            var tree = AssetNode.BuildTree(
                classDirectory,
                paths
                    .Where(path => path.StartsWith(classPath))
                    .Select(path => path.Substring(classPath.Length + 1)));

            context.AddSource($"{@class.Identifier.Text}.g.cs", tree.GenerateSource(namespaceSyntax.Name.ToString(), @class.Identifier.Text));            
        }
    }
}
