using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    //Not sure how else to share these tests between StablePriorityQueueTests and SimplePriorityQueueTests.  Using inheritance like the other tests would
    //require multiple inheritance..
    public static class SharedStablePriorityQueueTests
    {
        public static void TestOrderedQueue(Action<Node<int>> enqueue, Func<Node<int>> dequeue)
        {
            Node<int> node1 = new Node<int>(1);
            Node<int> node2 = new Node<int>(1);
            Node<int> node3 = new Node<int>(1);
            Node<int> node4 = new Node<int>(1);
            Node<int> node5 = new Node<int>(1);

            enqueue(node1);
            enqueue(node2);
            enqueue(node3);
            enqueue(node4);
            enqueue(node5);

            Assert.AreEqual(node1, dequeue());
            Assert.AreEqual(node2, dequeue());
            Assert.AreEqual(node3, dequeue());
            Assert.AreEqual(node4, dequeue());
            Assert.AreEqual(node5, dequeue());
        }

        public static void TestMoreComplicatedOrderedQueue(Action<Node<int>> enqueue, Func<Node<int>> dequeue)
        {
            Node<int> node11 = new Node<int>(1);
            Node<int> node12 = new Node<int>(1);
            Node<int> node13 = new Node<int>(1);
            Node<int> node14 = new Node<int>(1);
            Node<int> node15 = new Node<int>(1);
            Node<int> node21 = new Node<int>(2);
            Node<int> node22 = new Node<int>(2);
            Node<int> node23 = new Node<int>(2);
            Node<int> node24 = new Node<int>(2);
            Node<int> node25 = new Node<int>(2);
            Node<int> node31 = new Node<int>(3);
            Node<int> node32 = new Node<int>(3);
            Node<int> node33 = new Node<int>(3);
            Node<int> node34 = new Node<int>(3);
            Node<int> node35 = new Node<int>(3);
            Node<int> node41 = new Node<int>(4);
            Node<int> node42 = new Node<int>(4);
            Node<int> node43 = new Node<int>(4);
            Node<int> node44 = new Node<int>(4);
            Node<int> node45 = new Node<int>(4);
            Node<int> node51 = new Node<int>(5);
            Node<int> node52 = new Node<int>(5);
            Node<int> node53 = new Node<int>(5);
            Node<int> node54 = new Node<int>(5);
            Node<int> node55 = new Node<int>(5);

            enqueue(node31);
            enqueue(node51);
            enqueue(node52);
            enqueue(node11);
            enqueue(node21);
            enqueue(node22);
            enqueue(node53);
            enqueue(node41);
            enqueue(node12);
            enqueue(node32);
            enqueue(node13);
            enqueue(node42);
            enqueue(node43);
            enqueue(node44);
            enqueue(node45);
            enqueue(node54);
            enqueue(node14);
            enqueue(node23);
            enqueue(node24);
            enqueue(node33);
            enqueue(node34);
            enqueue(node55);
            enqueue(node35);
            enqueue(node25);
            enqueue(node15);

            Assert.AreEqual(node11, dequeue());
            Assert.AreEqual(node12, dequeue());
            Assert.AreEqual(node13, dequeue());
            Assert.AreEqual(node14, dequeue());
            Assert.AreEqual(node15, dequeue());
            Assert.AreEqual(node21, dequeue());
            Assert.AreEqual(node22, dequeue());
            Assert.AreEqual(node23, dequeue());
            Assert.AreEqual(node24, dequeue());
            Assert.AreEqual(node25, dequeue());
            Assert.AreEqual(node31, dequeue());
            Assert.AreEqual(node32, dequeue());
            Assert.AreEqual(node33, dequeue());
            Assert.AreEqual(node34, dequeue());
            Assert.AreEqual(node35, dequeue());
            Assert.AreEqual(node41, dequeue());
            Assert.AreEqual(node42, dequeue());
            Assert.AreEqual(node43, dequeue());
            Assert.AreEqual(node44, dequeue());
            Assert.AreEqual(node45, dequeue());
            Assert.AreEqual(node51, dequeue());
            Assert.AreEqual(node52, dequeue());
            Assert.AreEqual(node53, dequeue());
            Assert.AreEqual(node54, dequeue());
            Assert.AreEqual(node55, dequeue());
        }
    }
}
