using System;
using System.Collections.Generic;

class Kata
{
  public static List<int> TreeByLevels(Node node)
  {
    //off ya go!
    
    List<int> result = new List<int>();
    Queue<Node> queue = new Queue<Node>();
    if (node != null)
            {
                queue.Enqueue(node);
                result.Add(node.Value);
                while (queue.Count != 0)
                {
                    Node bufferNode = queue.Dequeue();
                    if (bufferNode.Left != null)
                    {
                        queue.Enqueue(bufferNode.Left);
                        result.Add(bufferNode.Left.Value);
                    }
                    if (bufferNode.Right != null)
                    {
                        queue.Enqueue(bufferNode.Right);
                        result.Add(bufferNode.Right.Value);
                    }
                }
            }
    return result;
  }
}
