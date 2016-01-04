using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace Priority_Queue_Example
{
    public static class SimplePriorityQueueExample
    {
        public static void RunExample()
        {
            //First, we create the priority queue.
            SimplePriorityQueue<string> priorityQueue = new SimplePriorityQueue<string>();

            //Now, let's add them all to the queue (in some arbitrary order)!
            priorityQueue.Enqueue("4 - Joseph", 4);
            priorityQueue.Enqueue("2 - Tyler", 0); //Note: Priority = 0 right now!
            priorityQueue.Enqueue("1 - Jason", 1);
            priorityQueue.Enqueue("4 - Ryan", 4);
            priorityQueue.Enqueue("3 - Valerie", 3);

            //Change one of the string's priority to 2.  Since this string is already in the priority queue, we call UpdatePriority() to do this
            priorityQueue.UpdatePriority("2 - Tyler", 2);

            //Finally, we'll dequeue all the strings and print them out
            while(priorityQueue.Count != 0)
            {
                string nextUser = priorityQueue.Dequeue();
                Console.WriteLine(nextUser);
            }

            //Output:
            //1 - Jason
            //2 - Tyler
            //3 - Valerie
            //4 - Joseph
            //4 - Ryan

            //Notice that when two strings with the same priority were enqueued, they were dequeued in the same order that they were enqueued.
        }
    }
}
