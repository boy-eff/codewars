using System;
using System.Collections.Generic;
using System.Text;
class Permutations
{
  static public void Proc(List<int> currArray, char[] array, List<string> result)
        {
            if (currArray.Count == array.Length)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < currArray.Count; i++)
                {
                    sb.Append(array[currArray[i]]);
                    
                }
                string buffer = sb.ToString();
                if (!result.Contains(buffer))
                {
                    result.Add(sb.ToString());
                }
                
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    bool isExist = false;
                    for (int j = 0; j < currArray.Count; j++)
                    {
                        if (currArray[j] == i)
                        {
                            isExist = true;
                        }
                    }
                    if (!isExist)
                    {
                        currArray.Add(i);
                        Proc(currArray, array, result);
                        currArray.RemoveAt(currArray.Count - 1);
                    }
                }
            }
        }
  
   public static List<string> SinglePermutations(string s)
   {
       // Your code here!
      List<string> result = new List<string>();
            Proc(new List<int>(), s.ToCharArray(), result);
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
            }
       return result;
   }
}
