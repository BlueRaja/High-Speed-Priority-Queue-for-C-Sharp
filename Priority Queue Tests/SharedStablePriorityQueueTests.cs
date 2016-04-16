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
        public static void TestOrderedQueue(Action<Node> enqueue, Func<Node> dequeue)
        {
            Node node1 = new Node(1);
            Node node2 = new Node(1);
            Node node3 = new Node(1);
            Node node4 = new Node(1);
            Node node5 = new Node(1);

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

        public static void TestMoreComplicatedOrderedQueue(Action<Node> enqueue, Func<Node> dequeue)
        {
            Node node11 = new Node(1);
            Node node12 = new Node(1);
            Node node13 = new Node(1);
            Node node14 = new Node(1);
            Node node15 = new Node(1);
            Node node21 = new Node(2);
            Node node22 = new Node(2);
            Node node23 = new Node(2);
            Node node24 = new Node(2);
            Node node25 = new Node(2);
            Node node31 = new Node(3);
            Node node32 = new Node(3);
            Node node33 = new Node(3);
            Node node34 = new Node(3);
            Node node35 = new Node(3);
            Node node41 = new Node(4);
            Node node42 = new Node(4);
            Node node43 = new Node(4);
            Node node44 = new Node(4);
            Node node45 = new Node(4);
            Node node51 = new Node(5);
            Node node52 = new Node(5);
            Node node53 = new Node(5);
            Node node54 = new Node(5);
            Node node55 = new Node(5);

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
