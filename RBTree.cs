// Реализация левостороннего красно-черного дерева
// с методами добавления новых элементов с балансировкой.

using System;

class RBTree<T> where T : IComparable
{
    private enum Color { Black, Red }

    private class Node
    {
        public T Value { get; set; }
        public Color Color { get; set; }
        public Node LeftChild { get; set; }
        public Node RigthChild { get; set; }
    }

    private Node root;

    public bool add(T value)
    {
        if (root != null)
        {
            bool result = addNode(root, value);
            root = rebalance(root);
            root.Color = Color.Black;
            return result;
        }
        else
        {
            root = new Node();
            root.Value = value;
            root.Color = Color.Black;
            return true;
        }
    }

    public bool contains(T value)
    {
        Node curNode = root;

        while (curNode != null)
        {
            if (curNode.Value.CompareTo(value) == 0)
                return true;

            curNode = (curNode.Value.CompareTo(value) > 0) ? curNode.LeftChild : curNode.RigthChild;
        }
        return false;
    }

    private bool addNode(Node node, T value)
    {
        if (node.Value.CompareTo(value) == 0)
        {
            return false;
        }
        else
        {
            if (node.Value.CompareTo(value) > 0)
            {
                if (node.LeftChild != null)
                {
                    bool result = addNode(node.LeftChild, value);
                    node.LeftChild = rebalance(node.LeftChild);
                    return result;
                }
                else
                {
                    node.LeftChild = new Node();
                    node.LeftChild.Value = value;
                    node.LeftChild.Color = Color.Red;
                    return true;
                }
            }
            else
            {
                if (node.RigthChild != null)
                {
                    bool result = addNode(node.RigthChild, value);
                    node.RigthChild = rebalance(node.RigthChild);
                    return result;
                }
                else
                {
                    node.RigthChild = new Node();
                    node.RigthChild.Value = value;
                    node.RigthChild.Color = Color.Red;
                    return true;
                }
            }
        }

    }

    private Node rebalance(Node node)
    {
        Node result = node;
        bool needRebalance;

        do
        {
            needRebalance = false;

            if (result.RigthChild != null && result.RigthChild.Color == Color.Red &&
                (result.LeftChild == null || result.LeftChild.Color == Color.Black))
            {
                needRebalance = true;
                result = rotateRight(result);

            }
            if (result.LeftChild != null && result.LeftChild.Color == Color.Red &&
                result.LeftChild.LeftChild != null && result.LeftChild.LeftChild.Color == Color.Red)
            {
                needRebalance = true;
                result = rotateLeft(result);
            }

            if (result.LeftChild != null && result.LeftChild.Color == Color.Red &&
                result.RigthChild != null && result.RigthChild.Color == Color.Red)
            {
                needRebalance = true;
                colorSwap(result);
            }
        } while (needRebalance);

        return result;
    }

    private Node rotateLeft(Node Y_node)
    {
        Node X_Node_leftChild = Y_node.LeftChild;
        Node betweenChild = X_Node_leftChild.RigthChild;
        X_Node_leftChild.RigthChild = Y_node;
        Y_node.LeftChild = betweenChild;
        X_Node_leftChild.Color = Y_node.Color;
        Y_node.Color = Color.Red;
        return X_Node_leftChild;
    }

    private Node rotateRight(Node node)
    {
        Node rightChild = node.RigthChild;
        Node betweenChild = rightChild.LeftChild;
        rightChild.LeftChild = node;
        node.RigthChild = betweenChild;
        rightChild.Color = node.Color;
        node.Color = Color.Red;
        return rightChild;
    }

    private void colorSwap(Node node)
    {
        node.Color = Color.Red;
        node.LeftChild.Color = Color.Black;
        node.RigthChild.Color = Color.Black;
    }
}
