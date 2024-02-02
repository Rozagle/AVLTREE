using System;
using System.Collections;
using System.Collections.Generic;

namespace Trees
{
class AVL<T> where T:IComparable
{
    public class Node
    {
        public T data;
        public Node left;
        public Node right;
        public Node(T data)
        {
            this.data = data;
        }
    }
    Node root;
    public AVL()
    {
    }
    public void Add(T data)
    {
        Node newItem = new Node(data);
        if (root == null)
        {
            root = newItem;
        }
        else
        {
            root = RecursiveInsert(root, newItem);
        }
    }
    private Node RecursiveInsert(Node current, Node n)
    {
        if (current == null)
        {
            current = n;
            return current;
        }
        else if (n.data.CompareTo(current.data) < 0)
        {
            current.left = RecursiveInsert(current.left, n);
            current = balance_tree(current);
        }
        else if (n.data.CompareTo(current.data) > 0)
        {
            current.right = RecursiveInsert(current.right, n);
            current = balance_tree(current);
        }
        return current;
    }
    private Node balance_tree(Node current)
    {
        int b_factor = balance_factor(current);
        if (b_factor > 1)
        {
            if (balance_factor(current.left) > 0)
            {
                current = RotateLL(current);
            }
            else
            {
                current = RotateLR(current);
            }
        }
        else if (b_factor < -1)
        {
            if (balance_factor(current.right) > 0)
            {
                current = RotateRL(current);
            }
            else
            {
                current = RotateRR(current);
            }
        }
        return current;
    }
    public void Delete(T target)
    {//and here
        root = Delete(root, target);
    }
    private Node Delete(Node current, T target)
    {
        Node parent;
        if (current == null)
        { return null; }
        else
        {
            //left subtree
            if (target.CompareTo(current.data) < 0)
            {
                current.left = Delete(current.left, target);
                current = balance_tree(current);
            }
            //right subtree
            else if (target.CompareTo(current.data) > 0)
            {
                current.right = Delete(current.right, target);
                current = balance_tree(current);
            }
            //if target is found
            else
            {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        current = balance_tree(current);
                    }
                    else
                    {   
                        return current.left;
                    }
            }
        }
        return current;
    }
    public Node Find(T key)
    {
         return Find(key, root);
    }
    private Node Find(T target, Node current)
    {
         if (current == null)
                return null;
         else if (target.CompareTo(current.data) == 0)
                return current;
         else if (target.CompareTo(current.data) < 0)
                return Find(target, current.left);
         else
                return Find(target, current.right);

    }
    public bool Contains(T key)
    {
           return Contains(key, root);
            
    }

    private bool Contains(T target, Node current)
    {
            if (current == null)
                return false;
            else if (target.CompareTo(current.data) == 0)
                return true;
            else if (target.CompareTo(current.data) < 0)
                return Contains(target, current.left);
            else
                return Contains(target, current.right);
    }

    public void DisplayTree()
    {
        if (root == null)
        {
            Console.WriteLine("Tree is empty");
            return;
        }
        InOrderDisplayTree(root);
        Console.WriteLine();
    }
    private void InOrderDisplayTree(Node current)
    {
        if (current != null)
        {
            InOrderDisplayTree(current.left);
            Console.Write("(Data: {0}, Height: {1}) ", current.data, getHeight(current));
            InOrderDisplayTree(current.right);
        }
    }
    private int max(int l, int r)
    {
        return l > r ? l : r;
    }

    public int getHeight()
    {
        return getHeight(root);
    }
    private int getHeight(Node current)
    {
        int height = 0;
        if (current != null)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int m = max(l, r);
            height = m + 1;
        }
        return height;
    }
    private int balance_factor(Node current)
    {
        int l = getHeight(current.left);
        int r = getHeight(current.right);
        int b_factor = l - r;
        return b_factor;
    }
    private Node RotateRR(Node parent)
    {
        Node pivot = parent.right;
        parent.right = pivot.left;
        pivot.left = parent;
        return pivot;
    }
    private Node RotateLL(Node parent)
    {
        Node pivot = parent.left;
        parent.left = pivot.right;
        pivot.right = parent;
        return pivot;
    }
    private Node RotateLR(Node parent)
    {
        Node pivot = parent.left;
        parent.left = RotateRR(pivot);
        return RotateLL(parent);
    }
    private Node RotateRL(Node parent)
    {
        Node pivot = parent.right;
        parent.right = RotateLL(pivot);
        return RotateRR(parent);
    }
}
}