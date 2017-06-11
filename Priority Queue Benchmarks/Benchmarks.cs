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

        private FastPriorityQueue<FastPriorityQueueNode> Queue;

        [GlobalSetup]
        public void GlobalSetup()
        {
            Queue = new FastPriorityQueue<FastPriorityQueueNode>(QueueSize * 2);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            Queue.Clear();
            for (int i = 0; i < QueueSize; i++)
            {
                Queue.Enqueue(new FastPriorityQueueNode(), i);
            }
        }

        [Benchmark]
        public void Dequeue()
        {
            // Bugfix: https://github.com/dotnet/BenchmarkDotNet/issues/470
            if(Queue.Count != QueueSize)
            {
                return;
            }

            for (int i = 0; i <= QueueSize / 2; i++)
            {
                Queue.Dequeue();
            }
        }

        [Benchmark]
        public void Enqueue()
        {
            if(Queue.Count != QueueSize)
            {
                return;
            }

            for(int i = 0; i <= QueueSize / 2; i++)
            {
                Queue.Enqueue(new FastPriorityQueueNode(), i);
            }
        }

        [Benchmark]
        public bool ContainsFirst()
        {
            if(Queue.Count != QueueSize)
            {
                return true;
            }

            return Queue.Contains(Queue.First);
        }

        [Benchmark]
        public void Clear()
        {
            if(Queue.Count != QueueSize)
            {
                return;
            }

            Queue.Clear();
        }

        [Benchmark]
        public void UpdatePriority()
        {
            if(Queue.Count != QueueSize)
            {
                return;
            }

            for(int i = 0; i <= QueueSize; i++)
            {
                Queue.UpdatePriority(Queue.First, QueueSize-i);
            }
        }
    }
}
