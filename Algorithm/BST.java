import java.util.ArrayList;
import java.util.*;
class Node 
{
    int data;
    Node left, right;
    
    Node(int data) 
    {
        this.data = data;
        this.left = null;
        this.right = null;
    }
}

class BST 
{
    static Node sortedArrayToBSTRecur(int[] arr, int start, int end) 
    {
        if (start > end)
        {
            return null;
        } 
        int mid = start + (end - start) / 2;
        Node root = new Node(arr[mid]);
        root.left = sortedArrayToBSTRecur(arr, start, mid - 1);
        root.right = sortedArrayToBSTRecur(arr, mid + 1, end);
        return root;
    }
    
    static Node sortedArrayToBST(int[] arr) 
    {
        return sortedArrayToBSTRecur(arr, 0, arr.length - 1);
    }

    static void preOrder(Node root) 
    {
        if (root == null)
        {
            return;
        }
        System.out.print(root.data + " ");
        preOrder(root.left);
        preOrder(root.right);
    }

    public static void main(String[] args) 
    {
        int[] arr = {1,2,3,4,5,6};
        Node root = sortedArrayToBST(arr);
        preOrder(root);
    }
}