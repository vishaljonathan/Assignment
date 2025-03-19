import java.util.*;
class Linearsearch
{
    static int LinearSearch(int[] arr, int n, int element)
    {
        for(int i=0;i<n;i++)
        {
            if(arr[i] == element)
            {
                return i;
            }
        }
        return -1;
    }
    public static void main(String[] args)
    {
        int n = 5, element = 2;
        int[] arr = {1,3,5,7,8};
        int position = LinearSearch(arr,n,element);
        if(position < 0)
        {
            System.out.println("Element not found or Element not present!");
        }
        else
        {
            System.out.println("Element found!");
        }
    }
}
