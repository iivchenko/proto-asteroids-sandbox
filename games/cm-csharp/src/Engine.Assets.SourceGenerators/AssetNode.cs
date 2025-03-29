namespace Engine.Assets.SourceGenerators;

internal sealed class AssetNode
{
    private enum ClassType
    {
        Root,
        Node,
        Leaf
    }

    public AssetNode(string value)
    {
        Value = value;
        Parent = null;
    }

    public AssetNode(string value, AssetNode? parent)
    {
        Value = value;
        Parent = parent;
    }

    public string Value { get; }

    public AssetNode? Parent { get; }

    public List<AssetNode> Children { get; } = [];

    public static AssetNode BuildTree(string root, IEnumerable<string> paths)
    {
        var node = new AssetNode(root);

        foreach (var path in paths)
        {
            BuildTree(new Queue<string>(path.Split('/', '\\')), node);
        }

        return node;
    }

    private static void BuildTree(Queue<string> queue, AssetNode parent)
    {
        if (queue.Count == 0)
        {
            return;
        }

        var value = queue.Dequeue();

        var child = parent.Children.FirstOrDefault(x => x.Value == value);
        if (child is null)
        {
            child = new AssetNode(value, parent);
            parent.Children.Add(child);
        }

        BuildTree(queue, child);
    }

    public string GenerateSource(string @namespace, string @class)
    { 
        var code = $$"""
                namespace {{@namespace}};

                public static partial class {{@class}}
                {
                {{GenerateProperties(0)}}
                {{GenerateChildrenSource(0)}}
                }
                """;

        return code;
    }

    private string GenerateSource(int identation)
    {
        switch (GetNodeType())
        {
            case ClassType.Node:
                return GenerateNodeClassSource(identation);

            case ClassType.Leaf:
                return GenerateLeafClassSource(identation);

            default:
                return string.Empty;
        }
    }

    private string GenerateNodeClassSource(int identation)
    {
        var ident = new string('\t', identation);
        var @class = "Asset" + Value.Replace(".", "_");

        var code = $$"""
                {{ident}}public class {{@class}}
                {{ident}}{
                {{GenerateProperties(identation)}}
                {{GenerateChildrenSource(identation)}}
                {{ident}}}
                """;

        return code;
    }

    private string GenerateLeafClassSource(int identation)
    {
        var ident = new string('\t', identation);
        var @class = "Asset" + Value.Replace(".", "_");
        var path = BuildPath(Parent, Value);

        return $$"""
            {{ident}}public sealed class {{@class}}
            {{ident}}{
                {{ident}}public string Path { get; } = "{{path}}";
            {{ident}}}
            """;
    }

    private ClassType GetNodeType()
    {
        if (Parent is null)
        {
            return ClassType.Root;
        }

        if (!Children.Any())
        {
            return ClassType.Leaf;
        }

        return ClassType.Node;
    }

    private string GenerateProperties(int identation)
    {
        var ident = new string('\t', identation + 1);
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