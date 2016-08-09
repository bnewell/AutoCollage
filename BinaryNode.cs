using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCollage
{
    public class BinaryNode
    {

        public BinaryNode parent { get; set; }
        public BinaryNode leftChild { get; set; }
        public BinaryNode rightChild { get; set; }

        public Size size { get; set; }
        public double aspectRatio { get; set; }
        public Split assignedSplit { get; set; }
        public Point coordinates { get; set; }

        public Image image { get; set; }

        public BinaryNode()
        {
            size = new Size(0, 0);
            aspectRatio = 0.0;
            assignedSplit = Split.None;

        }

        /// <summary>
        /// Add a single node to the binary tree
        /// </summary>
        public void addNode()
        {
            var nodeQueue = new Queue<BinaryNode>();
            var currentNode = this;
            while (true)
            {
                if (currentNode.leftChild == null)
                {
                    currentNode.leftChild = new BinaryNode();
                    currentNode.leftChild.parent = currentNode;
                    break;
                }
                nodeQueue.Enqueue(currentNode.leftChild);
                if(currentNode.rightChild == null)
                {
                    currentNode.rightChild = new BinaryNode();
                    currentNode.rightChild.parent = currentNode;
                    break;
                }
                nodeQueue.Enqueue(currentNode.rightChild);
                currentNode = nodeQueue.Dequeue();
            }
        }
    }
}
