import java.util.*;
class Parentheses
{
    public static boolean isValid(String s) 
    {
        Stack<Character> st = new Stack<Character>();
        for (char it : s.toCharArray()) 
        {
            if (it == '(' || it == '[' || it == '{')
                st.push(it);
            else 
            {
                if(st.isEmpty()) return false;
                char ch = st.pop();
                if((it == ')' && ch == '(') ||  (it == ']' && ch == '[') || (it == '}' && ch == '{')) continue;
                else return false;
            }
        }
        return st.isEmpty();
    }
    public static void main (String[] args) 
    {
        String s="()[{}()]";
        if(isValid(s)==true)
        System.out.println("String contains balanced parentheses");
        else
        System.out.println("String does not contain balanced parentheses");
    }
}