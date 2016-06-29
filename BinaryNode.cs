using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCollage
{
    class BinaryNode
    {
        public enum Split { H, V, N };

        // set split to None (N)
        private Split split = Split.N;
        private int width = 0;
        private int height = 0;
        private int x = 0;
        private int y = 0;
        private bool hasChild = false;
        private bool isLeftChild = false;
        private BinaryNode leftChild = null;
        private BinaryNode rightChild = null;
        private BinaryNode parent = null;
        private String imagePath;
        private double aspectRatio = 0.0;

        private Random random;

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public double AspectRatio
        {
            get
            {
                return aspectRatio;
            }

            set
            {
                aspectRatio = value;
            }
        }

        internal Split Split1
        {
            get
            {
                return split;
            }

            set
            {
                split = value;
            }
        }

        public BinaryNode()
        {
            random = new Random();

        }

        /// <summary>
        /// Add a single node to the binary tree
        /// </summary>
        public void addNode()
        {
            var temp = new Queue<BinaryNode>();
            var current = this;
            // Breadth-first insertion
            while (true)
            {
                if (current.leftChild == null)
                {
                    current.leftChild = new BinaryNode();
                    current.hasChild = true;
                    current.leftChild.parent = current;
                    current.leftChild.isLeftChild = true;
                    break;
                }
                temp.Enqueue(current.leftChild);
                if(current.rightChild == null)
                {
                    current.rightChild = new BinaryNode();
                    current.rightChild.parent = current;
                    current.rightChild.isLeftChild = false;
                    break;
                }
                temp.Enqueue(current.rightChild);
                current = temp.Dequeue();
            }
        }

        /// <summary>
        /// Parse binary tree and assign inner nodes
        /// a 'V' or 'H' split (50/50)
        /// </summary>
        public void setAllNodeSplits()
        {
            var current = this;
            var temp = new Queue<BinaryNode>();
            // breadth-first parse
            while (current != null)
            {
                if (current.leftChild != null)
                {
                    temp.Enqueue(current.leftChild);
                }
                if (current.rightChild != null)
                {
                    temp.Enqueue(current.rightChild);
                }
                if (current.hasChild)
                {
                    // only set split if it doesn't have one
                    if (current.Split1 == Split.N) {
                        if (random.Next(100) >= 50)
                        {
                            current.Split1 = Split.V;
                        }
                        else
                        {
                            current.Split1 = Split.H;
                        }
                    }
                }
                if (!(temp.Count == 0))
                {
                    current = temp.Dequeue();
                }
                else
                {
                    current = null;
                }
            }
        }

        /// <summary>
        /// Calculate and return aspect ratio of current node.
        /// </summary>
        /// <returns>Aspect ratio of current node.</returns>
        public double calculateAspectRatio()
        {
            var current = this;
            var aspectRatio = 0.0;

            if (current.hasChild)
            {
                // inner node
                var aspectRatioLeft = current.leftChild.calculateAspectRatio();
                var aspectRatioRight = current.rightChild.calculateAspectRatio();

                if(current.Split1 == Split.V)
                {
                    aspectRatio = aspectRatioLeft + aspectRatioRight;
                }
                else
                {
                    // aspect ratio for H split: (1/ar) = (1/ar_r) + (1/ar_l)
                    aspectRatio = (aspectRatioLeft * aspectRatioRight) / (aspectRatioLeft + aspectRatioRight);
                }
            }
            else
            {
                // leaf node, return actual aspect ratio
                aspectRatio = current.AspectRatio;
            }

            return aspectRatio;
        }

        /// <summary>
        /// Set aspect ratios of all nodes in the tree
        /// </summary>
        public void setAllAspectRatios()
        {
            var current = this;
            var temp = new Queue<BinaryNode>();

            // breadth-first parse
            while(current != null)
            {
                if (current.leftChild != null)
                {
                    temp.Enqueue(current.leftChild);
                }
                if (current.rightChild != null)
                {
                    temp.Enqueue(current.rightChild);
                }
                current.AspectRatio = current.calculateAspectRatio();
                if (!(temp.Count == 0))
                {
                    current = temp.Dequeue();
                }
                else
                {
                    current = null;
                }
            }

        }

        /// <summary>
        /// Calculate a node's dimensions and coordinates. Works Top Down
        /// so all of the current node's parent's dimensions and positions 
        /// need to be calculated first.
        /// </summary>
        public void calculateDimensionsAndPosition()
        {
            var current = this;
            if(current.parent == null)
            {
                // root already has dimensions
                return;
            }
            var parent = current.parent;
            // calculate dimensions
            if(parent.Split1 == Split.V)
            {
                current.Height = parent.Height;
                current.Width = (int)Math.Round(current.Height * current.AspectRatio);
            }
            else
            {
                // H split
                current.Width = parent.Width;
                current.Height = (int)Math.Round(current.Width / current.AspectRatio);
            }
            // calculate coordinates
            if (current.isLeftChild)
            {
                // left child always goes first and gets parent's coordinates
                current.x = parent.x;
                current.y = parent.y;
            }else
            {
                if(parent.Split1 == Split.V)
                {
                    current.x = parent.x + parent.Width - current.Width;
                    current.y = parent.y;
                }
                else
                {
                    current.y = parent.y + parent.Height - current.Height;
                    current.x = parent.x;
                }
            }
        }

        /// <summary>
        /// Set dimensions and positions of all nodes in the tree.
        /// </summary>
        public void setAllDimensionsAndPositions()
        {
            var current = this;
            var temp = new Queue<BinaryNode>();

            // breadth-first parse
            while (current != null)
            {
                if (current.leftChild != null)
                {
                    temp.Enqueue(current.leftChild);
                }
                if (current.rightChild != null)
                {
                    temp.Enqueue(current.rightChild);
                }
                current.calculateDimensionsAndPosition();
                if (!(temp.Count == 0))
                {
                    current = temp.Dequeue();
                }
                else
                {
                    current = null;
                }
            }
        }

        /// <summary>
        /// Assign images and aspect ratios to all leaf nodes
        /// </summary>
        /// <param name="imagesInfo">A list of dictionaries that contain the path, 
        /// width, height, and apsect ratio of all images</param>
        public void setAllImages(List<Dictionary<string, string>> imagesInfo)
        {
            var current = this;
            var temp = new Queue<BinaryNode>();
            var i = 0;
            // breadth-first parse
            while (current != null)
            {
                if (current.leftChild != null)
                {
                    temp.Enqueue(current.leftChild);
                }
                if (current.rightChild != null)
                {
                    temp.Enqueue(current.rightChild);
                }

                if (!current.hasChild)
                {
                    // leaf node
                    if(i < imagesInfo.Count)
                    {
                        current.imagePath = imagesInfo[i]["path"];
                        current.width = Convert.ToInt32(imagesInfo[i]["width"]);
                        current.height = Convert.ToInt32(imagesInfo[i]["height"]);
                        current.aspectRatio = Double.Parse(imagesInfo[i]["ratio"]);
                        i++;
                    }
                }

                if (!(temp.Count == 0))
                {
                    current = temp.Dequeue();
                }
                else
                {
                    current = null;
                }
            }
        }

        public List<Dictionary<string, string>> getImageInfoFromTree()
        {
            var current = this;
            var temp = new Queue<BinaryNode>();

            var imagesInfo = new List<Dictionary<string, string>>();
            // breadth-first parse
            while (current != null)
            {
                if (current.leftChild != null)
                {
                    temp.Enqueue(current.leftChild);
                }
                if (current.rightChild != null)
                {
                    temp.Enqueue(current.rightChild);
                }

                if (!current.hasChild)
                {
                    var imageInfo = new Dictionary<String, String>();
                    imageInfo.Add("path", current.imagePath);
                    imageInfo.Add("width", current.width.ToString());
                    imageInfo.Add("height", current.height.ToString());
                    imageInfo.Add("x", current.x.ToString());
                    imageInfo.Add("y", current.y.ToString());
                    imagesInfo.Add(imageInfo);
                }

                if (!(temp.Count == 0))
                {
                    current = temp.Dequeue();
                }
                else
                {
                    current = null;
                }
            }
            return imagesInfo;
        }


        public void printTree()
        {
            var current = this;
            var temp = new Queue<BinaryNode>();

            int p = 1;
            int c = 1;

            while(current != null)
            {
                Console.WriteLine("node: {0} split: {1} ar: {2} x: {3} y: {4} w: {5} h: {6} p: {7}", 
                    p, current.Split1, Math.Round(current.AspectRatio, 2), current.x, current.y, current.Width,
                    current.Height, current.imagePath);
                p++;
                if(current.leftChild != null)
                {
                    temp.Enqueue(current.leftChild);
                    c++;
                    Console.WriteLine("leftChild={0}", c);
                }
                if(current.rightChild != null)
                {
                    temp.Enqueue(current.rightChild);
                    c++;
                    Console.WriteLine("rightChild={0}", c);
                }
                Console.WriteLine("---");

                if(!(temp.Count == 0))
                {
                    current = temp.Dequeue();
                }
                else
                {
                    current = null;
                }

            }

            
        }


    }
}
