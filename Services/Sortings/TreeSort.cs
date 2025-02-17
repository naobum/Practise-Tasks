using Practise_Tasks.Interfaces;

namespace Practise_Tasks.Services.Sortings
{
    public class TreeSort : ISorter<string, char>
    {
        public void Sort(ref string symbols)
        {
            if (symbols == null || symbols.Length == 0)
                return;

            var root = new TreeNode(symbols[0]);

            for (int i = 1; i < symbols.Length; i++)
                root.Insert(symbols[i]);

            List<char> result = new();
            root.Traverse(result);

            symbols = new string(result.ToArray());
        }
        private class TreeNode
        {
            public char Value { get; }
            public TreeNode? Left { get; set; }
            public TreeNode? Right { get; set; }

            public TreeNode(char value) => 
                Value = value;

            public void Insert(char value)
            {
                if (value <= Value)
                    Left = Insert(Left, value);
                else
                    Right = Insert(Right, value);
            }

            private TreeNode Insert(TreeNode? node, char value)
            {
                if (node == null) 
                    return new TreeNode(value);
                node.Insert(value);
                return node;
            }

            public void Traverse(List<char> result)
            {
                Left?.Traverse(result);
                result.Add(Value);
                Right?.Traverse(result);
            }
        }
    }
}
