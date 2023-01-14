using System.Collections.ObjectModel;

namespace Selenium.FramesSearcher.Extensions;

public class TreeNode<T> where T : ExtendedWebElement
{
    private readonly T _value;
    private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

    public TreeNode(T value)
    {
        _value = value;
    }

    public TreeNode<T> this[int i] => _children[i];

    public TreeNode<T>? Parent { get; private set; }

    public T Value => _value;

    public ReadOnlyCollection<TreeNode<T>> Children => _children.AsReadOnly();

    public TreeNode<T> AddChild(T value)
    {
        var node = new TreeNode<T>(value) {Parent = this};
        _children.Add(node);
        return node;
    }

    public TreeNode<T>[] AddChildren(params T[] values)
    {
        return values.Select(AddChild).ToArray();
    }

    public bool RemoveChild(TreeNode<T> node)
    {
        return _children.Remove(node);
    }

    public void Traverse(Action<T> action)
    {
        action(Value);
        foreach (var child in _children)
            child.Traverse(action);
    }

    public TreeNode<T>? FindInTree(T? value)
    {
        if (value == null)
            return null;
        return value.Equals(_value) ? this : _children.Select(child => child.FindInTree(value)).FirstOrDefault(found => found != null);
    }

    public IList<TreeNode<T>> GetNodesPathToParent()
    {
        var parent = Parent;
        var path = new List<TreeNode<T>>() { this };

        while (parent != null)
        {
            path.Add(parent);
            parent = parent.Parent;
        }

        path.Reverse();
        return path;
    }


    public IEnumerable<T> Flatten()
    {
        return new[] {Value}.Concat(_children.SelectMany(x => x.Flatten()));
    }
}