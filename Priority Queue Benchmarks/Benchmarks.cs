using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Priority_Queue;

namespace Priority_Queue_Benchmarks
{
    public class Benchmarks
    {
        [Params(1, 100, 10000)]
        public int QueueSize;

        public FastPriorityQueueNode[] Nodes;
        public int[] RandomPriorities;
        public int[] RandomUpdatePriorities;

        private FastPriorityQueue<FastPriorityQueueNode> Queue;

        [GlobalSetup]
        public void GlobalSetup()
        {
            Queue = new FastPriorityQueue<FastPriorityQueueNode>(QueueSize);
            Nodes = new FastPriorityQueueNode[QueueSize];
            RandomPriorities = new int[QueueSize];
            RandomUpdatePriorities = new int[QueueSize];
            Random rand = new Random(34829061);
            for(int i = 0; i < QueueSize; i++)
            {
                Nodes[i] = new FastPriorityQueueNode();
                RandomPriorities[i] = rand.Next(16777216); // constrain to range float can hold with no rounding
                RandomUpdatePriorities[i] = rand.Next(16777216); // constrain to range float can hold with no rounding
            }
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            Queue.Clear();
        }

        [Benchmark]
        public void Enqueue()
        {
            Queue.Clear();
            for(int i = 0; i < QueueSize; i++)
            {
                Queue.Enqueue(Nodes[i], i);
            }
        }

        [Benchmark]
        public void EnqueueBackwards()
        {
            Queue.Clear();
            for(int i = QueueSize - 1; i >= 0; i--)
            {
                Queue.Enqueue(Nodes[i], i);
            }
        }

        [Benchmark]
        public void EnqueueRandom()
        {
            Queue.Clear();
            for(int i = 0; i < QueueSize; i++)
            {
                Queue.Enqueue(Nodes[i], RandomPriorities[i]);
            }
        }

        [Benchmark]
        public void EnqueueDequeue()
        {
            Enqueue();

            for(int i = 0; i < QueueSize; i++)
            {
                Queue.Dequeue();
            }
        }

        [Benchmark]
        public void EnqueueBackwardsDequeue()
        {
            EnqueueBackwards();

            for(int i = 0; i < QueueSize; i++)
            {
                Queue.Dequeue();
            }
        }

        [Benchmark]
        public void EnqueueRandomDequeue()
        {
            EnqueueRandom();

            for(int i = 0; i < QueueSize; i++)
            {
                Queue.Dequeue();
            }
        }

        [Benchmark]
        public void EnqueueUpdatePriority()
        {
            Enqueue();

            for(int i = 0; i < QueueSize; i++)
            {
                Queue.UpdatePriority(Nodes[i], QueueSize - i);
            }
        }

        [Benchmark]
        public void EnqueueBackwardsUpdatePriority()
        {
            EnqueueBackwards();

            for(int i = 0; i < QueueSize; i++)
            {
                Queue.UpdatePriority(Nodes[i], QueueSize - i);
            }
        }

        [Benchmark]
        public void EnqueueRandomUpdatePriority()
        {
            EnqueueRandom();

            for(int i = 0; i < QueueSize; i++)
            {
                Queue.UpdatePriority(Nodes[i], RandomUpdatePriorities[i]);
            }
        }

        [Benchmark]
        public bool EnqueueContains()
        {
            Enqueue();
            bool ret = true; // to ensure the compiler doesn't optimize the contains calls out of existence

            for(int i = 0; i < QueueSize; i++)
            {
                ret &= Queue.Contains(Nodes[i]);
            }
            return ret;
        }

        [Benchmark]
        public float EnqueueEnumerator()
        {
            Enqueue();
            float prioritySum = 0;

            foreach(FastPriorityQueueNode node in Queue)
            {
                prioritySum += node.Priority;
            }

            return prioritySum;
        }
    }
}
