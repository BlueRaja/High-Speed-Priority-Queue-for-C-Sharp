using NUnit.Framework;
using Priority_Queue;

namespace Priority_Queue_Tests
{
    [TestFixture]
    public class HeapPriorityQueueTests
    {
        private class Node : PriorityQueueNode
        {
            public Node(int priority)
            {
                Priority = priority;
            }
        }

        [Test]
        public void TestSanity()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);

            Assert.AreEqual(node1, node1);
            Assert.AreEqual(node2, node2);
            Assert.AreNotEqual(node1, node2);
        }

        [Test]
        public void TestSimpleQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(5);
            Enqueue(queue, node2);
            Enqueue(queue, node5);
            Enqueue(queue, node1);
            Enqueue(queue, node3);
            Enqueue(queue, node4);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node2, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
            Assert.AreEqual(node5, Dequeue(queue));
        }

        [Test]
        public void TestForwardOrder()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(5);
            Enqueue(queue, node1);
            Enqueue(queue, node2);
            Enqueue(queue, node3);
            Enqueue(queue, node4);
            Enqueue(queue, node5);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node2, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
            Assert.AreEqual(node5, Dequeue(queue));
        }

        [Test]
        public void TestBackwardOrder()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(5);
            Enqueue(queue, node5);
            Enqueue(queue, node4);
            Enqueue(queue, node3);
            Enqueue(queue, node2);
            Enqueue(queue, node1);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node2, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
            Assert.AreEqual(node5, Dequeue(queue));
        }

        [Test]
        public void TestAddingSameNodesLater()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(5);
            Enqueue(queue, node2);
            Enqueue(queue, node5);
            Enqueue(queue, node1);
            Enqueue(queue, node3);
            Enqueue(queue, node4);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node2, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
            Assert.AreEqual(node5, Dequeue(queue));

            Enqueue(queue, node5);
            Enqueue(queue, node3);
            Enqueue(queue, node1);
            Enqueue(queue, node2);
            Enqueue(queue, node4);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node2, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
            Assert.AreEqual(node5, Dequeue(queue));
        }

        [Test]
        public void TestAddingDifferentNodesLater()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(5);
            Enqueue(queue, node2);
            Enqueue(queue, node5);
            Enqueue(queue, node1);
            Enqueue(queue, node3);
            Enqueue(queue, node4);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node2, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
            Assert.AreEqual(node5, Dequeue(queue));

            Node node6 = new Node(6);
            Node node7 = new Node(7);
            Node node8 = new Node(8);
            Node node9 = new Node(9);
            Node node10 = new Node(10);

            Enqueue(queue, node6);
            Enqueue(queue, node7);
            Enqueue(queue, node8);
            Enqueue(queue, node10);
            Enqueue(queue, node9);

            Assert.AreEqual(node6, Dequeue(queue));
            Assert.AreEqual(node7, Dequeue(queue));
            Assert.AreEqual(node8, Dequeue(queue));
            Assert.AreEqual(node9, Dequeue(queue));
            Assert.AreEqual(node10, Dequeue(queue));
        }

        [Test]
        public void TestClear()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(5);
            Enqueue(queue, node2);
            Enqueue(queue, node5);
            queue.Clear();
            Enqueue(queue, node1);
            Enqueue(queue, node3);
            Enqueue(queue, node4);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
        }

        [Test]
        public void TestOrderedQueue()
        {
            Node node1 = new Node(1);
            Node node2 = new Node(1);
            Node node3 = new Node(1);
            Node node4 = new Node(1);
            Node node5 = new Node(1);

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(5);
            Enqueue(queue, node1);
            Enqueue(queue, node2);
            Enqueue(queue, node3);
            Enqueue(queue, node4);
            Enqueue(queue, node5);

            Assert.AreEqual(node1, Dequeue(queue));
            Assert.AreEqual(node2, Dequeue(queue));
            Assert.AreEqual(node3, Dequeue(queue));
            Assert.AreEqual(node4, Dequeue(queue));
            Assert.AreEqual(node5, Dequeue(queue));
        }

        [Test]
        public void TestMoreComplicatedQueue()
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

            HeapPriorityQueue<Node> queue = new HeapPriorityQueue<Node>(25);
            Enqueue(queue, node31);
            Enqueue(queue, node51);
            Enqueue(queue, node52);
            Enqueue(queue, node11);
            Enqueue(queue, node21);
            Enqueue(queue, node22);
            Enqueue(queue, node53);
            Enqueue(queue, node41);
            Enqueue(queue, node12);
            Enqueue(queue, node32);
            Enqueue(queue, node13);
            Enqueue(queue, node42);
            Enqueue(queue, node43);
            Enqueue(queue, node44);
            Enqueue(queue, node45);
            Enqueue(queue, node54);
            Enqueue(queue, node14);
            Enqueue(queue, node23);
            Enqueue(queue, node24);
            Enqueue(queue, node33);
            Enqueue(queue, node34);
            Enqueue(queue, node55);
            Enqueue(queue, node35);
            Enqueue(queue, node25);
            Enqueue(queue, node15);

            Assert.AreEqual(node11, Dequeue(queue));
            Assert.AreEqual(node12, Dequeue(queue));
            Assert.AreEqual(node13, Dequeue(queue));
            Assert.AreEqual(node14, Dequeue(queue));
            Assert.AreEqual(node15, Dequeue(queue));
            Assert.AreEqual(node21, Dequeue(queue));
            Assert.AreEqual(node22, Dequeue(queue));
            Assert.AreEqual(node23, Dequeue(queue));
            Assert.AreEqual(node24, Dequeue(queue));
            Assert.AreEqual(node25, Dequeue(queue));
            Assert.AreEqual(node31, Dequeue(queue));
            Assert.AreEqual(node32, Dequeue(queue));
            Assert.AreEqual(node33, Dequeue(queue));
            Assert.AreEqual(node34, Dequeue(queue));
            Assert.AreEqual(node35, Dequeue(queue));
            Assert.AreEqual(node41, Dequeue(queue));
            Assert.AreEqual(node42, Dequeue(queue));
            Assert.AreEqual(node43, Dequeue(queue));
            Assert.AreEqual(node44, Dequeue(queue));
            Assert.AreEqual(node45, Dequeue(queue));
            Assert.AreEqual(node51, Dequeue(queue));
            Assert.AreEqual(node52, Dequeue(queue));
            Assert.AreEqual(node53, Dequeue(queue));
            Assert.AreEqual(node54, Dequeue(queue));
            Assert.AreEqual(node55, Dequeue(queue));
        }

        private void Enqueue(HeapPriorityQueue<Node> queue, Node node)
        {
            queue.Enqueue(node, node.Priority);
            Assert.IsTrue(queue.IsValidQueue());
        }

        private Node Dequeue(HeapPriorityQueue<Node> queue)
        {
            Node returnMe = queue.Dequeue();
            Assert.IsTrue(queue.IsValidQueue());
            return returnMe;
        }
    }
}
