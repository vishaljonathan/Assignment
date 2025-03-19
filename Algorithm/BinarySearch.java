import java.util.*;
class Binarysearch
{
    public static void main(String[] args)
    {
        int arr[] = {10,20,30,40,50};
        int key = 20;
        int n = 5;
        binarysearch(arr,n,key);
    }
    public static void binarysearch(int arr[],int len,int key)
    {
        int mid,last,first=0;
        last = len-1;
        mid = (first+last)/2;
        while(first<=last)
        {
            if(arr[mid]<key)
            {
                first = mid+1;
            }
            else if(arr[mid]==key)
            {
                System.out.println("Element is found at the the index: "+mid);
                break;
            }
            else
            {
                last = mid - 1;
            }
            mid = (first+last)/2;
        }
        if(first>last)
        {
            System.out.println("Element is not found or Element not present!");
        }
    }
}