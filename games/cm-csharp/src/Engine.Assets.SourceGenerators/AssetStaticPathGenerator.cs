using Microsoft.CodeAnalysis;

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
    public const string Root = "Assets";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var pipeline =
            context
                .AdditionalTextsProvider
                .Where(text => text.Path.Contains(Root))
                .Select((asset, _) =>
                {
                    var index = asset.Path.IndexOf("\\" + Root + "\\");

                    return asset.Path.Substring(index + Root.Length + 2);
                })
                .Collect();

        context
            .RegisterSourceOutput(
                pipeline,
                (context, paths) =>
                {
                    var tree = new AssetNode(Root, null);

                    foreach (var path in paths)
                    {
                        ProcessFile(tree, new Queue<string>(path.Split('/', '\\')));
                    }

                    context.AddSource($"GameAssets.g.cs", tree.GenerateSource(0));
                });
    }

    private AssetNode? ProcessFile(AssetNode node, Queue<string> items)
    {
        if (items.Count == 0)
        {
            return node;
        }

        var item = items.Dequeue();

        var child = node.Children.FirstOrDefault(x => x.Value == item);
        if (child is null)
        {
            child = new AssetNode(item, node);
            node.Children.Add(child);
        }

        ProcessFile(child, items);


        return node;
    }
}

internal sealed class AssetNode
{
    public AssetNode(string value, AssetNode? parent)
    {
        Value = value;
        Parent = parent;
    }

    public bool IsBuild { get; set; } = false;

    public string Value { get; }

    public AssetNode? Parent { get; }

    public List<AssetNode> Children { get; } = new List<AssetNode>();

    public string GenerateSource(int identation)
    {
        var ident = new string('\t', identation);
        var namespa = identation == 0 ? "namespace GameAssets;\n" : string.Empty;
        var classType = identation == 0 ? "static" : "sealed";
        var className = "Asset" + Value.Replace(".", "_");
        var propertyType = identation == 0 ? "static" : string.Empty;

        if (Children.Any())
        {
            return $$"""
                {{namespa}}
                {{ident}}public {{classType}} class {{className}}
                {{ident}}{
                {{GenerateProperties(identation)}}
                {{GenerateChildrenSource(identation)}}
                {{ident}}}
                """;
        }
        else
        {
            var path = BuildPath(this.Parent, Value);

            return $$"""
                {{namespa}}
                {{ident}}public {{classType}} class {{className}}
                {{ident}}{
                    {{ident}}public{{propertyType}} string Path { get; } = "{{path}}";
                {{ident}}}
                """;
        }
    }

    private string GenerateProperties(int identation)
    {
        var ident = new string('\t', identation + 1);
        var namespa = identation == 0 ? "namespace GameAssets;\n" : string.Empty;
        var classType = identation == 0 ? "static" : "sealed";
        var toClassName = (string name) => "Asset" + name.Replace(".", "_");
        var propertyType = identation == 0 ? "static" : string.Empty;
        var toPropertyName = (string name) => name.Replace(".", "_");

        var properties =
            Children
                .Select(x => $$"""{{ident}}public {{propertyType}} {{toClassName(x.Value)}} {{toPropertyName(x.Value)}} { get; } = new {{toClassName(x.Value)}}();""");

        return string.Join(Environment.NewLine, properties);

    }

    private string GenerateChildrenSource(int identation)
    {
        return string.Join(
            Environment.NewLine,
            Children.Select(x => x.GenerateSource(identation + 1)));
    }

    private string BuildPath(AssetNode? node, string path)
    {
        return node is null
            ? path
            : BuildPath(node.Parent, node.Value + "/" + path);
    }
}