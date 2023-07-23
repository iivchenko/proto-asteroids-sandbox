using Godot;
using System.Collections.Generic;

public static class NodeExtensions
{
    public static IEnumerable<TNode> OnlyChildren<TNode>(this Node node)
        where TNode : class
    {
        foreach(var child in node.GetChildren())
        {
            if (child is TNode)
            {
                yield return child as TNode;
            }
        }
    }
}
