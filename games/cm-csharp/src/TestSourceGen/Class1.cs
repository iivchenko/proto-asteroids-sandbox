using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace TestSourceGen
{
    /* Links
     * https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.cookbook.md#additional-file-transformation
     * https://andrewlock.net/creating-a-source-generator-part-1-creating-an-incremental-source-generator/
     * https://posts.specterops.io/dotnet-source-generators-in-2024-part-1-getting-started-76d619b633f5
     * https://www.youtube.com/watch?v=Yf8t7GqA6zA
    */
    [Generator]
    public class TestGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var pipeline = context.AdditionalTextsProvider
                .Where(asset => asset.Path.StartsWith("Assets/"))
                .Select((asset, _) =>
                {
                    var name = Path.GetFileName(asset.Path);
                    var code = "";
                    return (name, code);
                });

            context.RegisterSourceOutput(pipeline,
                static (context, pair) =>
                    // Note: this AddSource is simplified. You will likely want to include the path in the name of the file to avoid
                    // issues with duplicate file names in different paths in the same project.
                    context.AddSource($"{pair.name}generated.cs", SourceText.From(pair.code, Encoding.UTF8)));
        }
    }
}
