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

        private FastPriorityQueue<FastPriorityQueueNode> Queue;

        [GlobalSetup]
        public void GlobalSetup()
        {
            Queue = new FastPriorityQueue<FastPriorityQueueNode>(QueueSize);
            Nodes = new FastPriorityQueueNode[QueueSize];
            for(int i = 0; i < QueueSize; i++)
            {
                Nodes[i] = new FastPriorityQueueNode();
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
        public void EnqueueDequeue()
        {
            Enqueue();

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
                Queue.UpdatePriority(Queue.First, QueueSize-i);
            }
        }

        [Benchmark]
        public void EnqueueContains()
        {
            Enqueue();

            for(int i = 0; i < QueueSize; i++)
            {
                Queue.Contains(Nodes[i]);
            }
        }
    }
}
