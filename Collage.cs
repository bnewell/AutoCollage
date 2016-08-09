using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCollage
{
    class Collage
    {
        private BinaryNode root = new BinaryNode();

        private List<Image> images;
        private List<ImageDetails> imageInformation = new List<ImageDetails>();

        private Size finalCollageSize;

        private int collageLength;
        private Orientation collageOrientation;

        private int borderWidth;
        private Color borderColor;

        private Random random;


        public Collage(List<String> imagePaths, int collageLength, Orientation collageOrientation, int borderWidth, Color borderColor)
        {
            this.images = convertPathsToImages(imagePaths);
            this.collageLength = collageLength;
            this.collageOrientation = collageOrientation;
            this.borderWidth = borderWidth;
            this.borderColor = borderColor;
            random = new Random();
            SetCollageSplit();
        }


        private List<Image> convertPathsToImages(List<String> pathsToImages)
        {
            List<Image> listOfImages = new List<Image>();
            foreach (String image in pathsToImages)
            {
                Image theImage = null;
                try
                {
                    theImage = Image.FromFile(image);
                }
                catch (Exception e)
                {

                }
                if (theImage != null)
                {
                    listOfImages.Add(Image.FromFile(image));
                }
            }
            return listOfImages;
        }


        private void SetCollageSplit()
        {
            // set collage orientation
            if (collageOrientation != Orientation.Both)
            {
                // Orientation.V = Split.H
                // Orientation.H = Split.V
                root.assignedSplit = collageOrientation == Orientation.Vertical ? Split.Horizontal : Split.Vertical;
            } else
            {
                root.assignedSplit = GetRandomSplit();
            }
        }


        private Split GetRandomSplit()
        {
            return random.Next(2) == 0 ? Split.Horizontal : Split.Vertical;
        }


        public Bitmap CreateCollage()
        {
            CreateCollageTree();

            var newCollage = new Bitmap(finalCollageSize.Width, finalCollageSize.Height, PixelFormat.Format32bppRgb);
            var canvas = Graphics.FromImage(newCollage);

            // add individual images to new collage
            for (int i = 0; i < imageInformation.Count; i++)
            {
                ImageDetails image = imageInformation[i];
                int x = image.coordinates.X;
                int y = image.coordinates.Y;
                int width = image.size.Width;
                int height = image.size.Height;

                canvas.DrawImage(image.image, x, y, width, height);
                canvas = drawBorderOnCanvas(canvas, image, x, y, width, height);
            }
            return newCollage;
        }


        private Graphics drawBorderOnCanvas(Graphics canvas, ImageDetails image, int x, int y, int width, int height)
        {
            // get border size for each image
            // if a side is external, full "border width"
            // otherwise half the border, since it's sharing a border with another image
            Dictionary<String, double> borderWidths = determineBorderWidth(image);
            var color = CollagePreferences.borderColor;

            // top border
            Pen borderLine = new Pen(color, (float)borderWidths["top"]);
            canvas.DrawLine(borderLine, x, y, x + width, y);

            // bottom border
            borderLine = new Pen(color, (float)borderWidths["bottom"]);
            canvas.DrawLine(borderLine, x, y + height, x + width, y + height);

            // left border
            borderLine = new Pen(color, (float)borderWidths["left"]);
            canvas.DrawLine(borderLine, x, y, x, y + height);

            // right border
            borderLine = new Pen(color, (float)borderWidths["right"]);
            canvas.DrawLine(borderLine, x + width, y, x + width, y + height);

            return canvas;
        }


        private void CreateCollageTree()
        {
            InitializeFullBinaryTree();
            SetNodeSplits();
            SetImagesToLeafNodes();
            SetAspectRatios();
            SetFinalCollageSize();
            SetNewImageSizes();
            SetImageCoordinates();
            GetImageDetailsFromTree();
        }


        private void InitializeFullBinaryTree()
        {
            // Create collage tree. It needs to be a full binary tree
            // so add 2 nodes for every one image. Also images.Count -1
            // because the root node has already been created;
            for (int i = 0; i < images.Count - 1; i++)
            {
                root.addNode();
                root.addNode();
            }
        }


        /// <summary>
        /// Assign inner nodes a 'Vertical' or 'Horizontal' split at random (50/50 chance)
        /// </summary>
        private void SetNodeSplits()
        {
            var currentNode = root;
            var nodeQueue = new Queue<BinaryNode>();

            while (currentNode != null)
            {
                if (currentNode.leftChild != null)
                {
                    // inner node
                    nodeQueue.Enqueue(currentNode.leftChild);
                    nodeQueue.Enqueue(currentNode.rightChild);

                    // only set split if it doesn't have one
                    if (currentNode.assignedSplit == Split.None)
                    {
                        currentNode.assignedSplit = GetRandomSplit();
                    }
                }

                currentNode = nodeQueue.Count > 0 ? nodeQueue.Dequeue() : null;
            }
        }


        /// <summary>
        /// Assign images and aspect ratios to all leaf nodes
        /// </summary>
        /// <param name="imagesInfo">A list of dictionaries that contain the path, 
        /// width, height, and apsect ratio of all images</param>
        private void SetImagesToLeafNodes()
        {
            var currentNode = root;
            var nodeQueue = new Queue<BinaryNode>();
            var imageIndex = 0;

            while (currentNode != null)
            {
                if (currentNode.leftChild != null)
                {
                    nodeQueue.Enqueue(currentNode.leftChild);
                    nodeQueue.Enqueue(currentNode.rightChild);
                }
                else
                {
                    // It's a leaf node, so assign an image to it.
                    if (imageIndex < images.Count)
                    {
                        Image image = images[imageIndex];
                        currentNode.image = image;
                        currentNode.aspectRatio = (float)image.Width / (float)image.Height;
                        imageIndex++;
                    }
                }

                currentNode = nodeQueue.Count > 0 ? nodeQueue.Dequeue() : null;
            }
        }


        /// <summary>
        /// Set aspect ratios of all nodes in the tree
        /// </summary>
        private void SetAspectRatios()
        {
            var currentNode = root;
            var nodeQueue = new Queue<BinaryNode>();

            while (currentNode != null)
            {
                if (currentNode.leftChild != null)
                {
                    nodeQueue.Enqueue(currentNode.leftChild);
                    nodeQueue.Enqueue(currentNode.rightChild);
                }

                currentNode.aspectRatio = CalculateAspectRatio(currentNode);
                currentNode = nodeQueue.Count > 0 ? nodeQueue.Dequeue() : null;
            }

        }


        /// <summary>
        /// Calculate and return aspect ratio of current node.
        /// </summary>
        /// <returns>Aspect ratio of current node.</returns>
        private double CalculateAspectRatio(BinaryNode node)
        {
            var currentNode = node;
            var aspectRatio = 0.0;

            if (currentNode.leftChild != null)
            {
                // inner node
                var aspectRatioLeft = CalculateAspectRatio(currentNode.leftChild);
                var aspectRatioRight = CalculateAspectRatio(currentNode.rightChild);

                if (currentNode.assignedSplit == Split.Vertical)
                {
                    aspectRatio = aspectRatioLeft + aspectRatioRight;
                }
                else
                {
                    // aspect ratio for H split: (1/ar) = (1/ar_l) + (1/ar_r)
                    aspectRatio = (aspectRatioLeft * aspectRatioRight) / (aspectRatioLeft + aspectRatioRight);
                }
            }
            else
            {
                // leaf node, return actual aspect ratio
                aspectRatio = currentNode.aspectRatio;
            }

            return aspectRatio;
        }


        private void SetFinalCollageSize()
        {
            // Depending on the orientation, the initial "size" of the collage
            // will specify either the vertical or horizontal pixels. We must calculate
            // the other dimension using the aspect ratio simple algebra expressions.

            // aspectRatio = w / h;
            // w = h * aspectRatio;
            // h = w / aspectRatio;
            var width = 0;
            var height = 0;
            if (root.aspectRatio > 1)
            {
                // LANDSCAPE
                width = collageLength;
                height = (int)Math.Round(width / root.aspectRatio, 0);
            }
            else
            {
                // PORTRAIT
                height = collageLength;
                width = (int)Math.Round(height * root.aspectRatio, 0);
            }

            finalCollageSize = new Size(width, height);
        }


        /// <summary>
        /// Set image sizes for all nodes in the tre
        /// </summary>
        private void SetNewImageSizes()
        {
            var currentNode = root;
            var nodeQueue = new Queue<BinaryNode>();

            while (currentNode != null)
            {
                if (currentNode.leftChild != null)
                {
                    nodeQueue.Enqueue(currentNode.leftChild);
                    nodeQueue.Enqueue(currentNode.rightChild);
                }

                currentNode.size = CalculateNewImageSize(currentNode);
                currentNode = nodeQueue.Count > 0 ? nodeQueue.Dequeue() : null;
            }
        }


        /// <summary>
        /// Calculate a node's size. Works Top Down
        /// so all of the current node's parent's sizes 
        /// need to be calculated first.
        /// </summary>
        private Size CalculateNewImageSize(BinaryNode node)
        {
            var current = node;
            var parent = current.parent;

            var width = 0;
            var height = 0;

            if (parent == null)
            {
                // root size is collage size
                return new Size(finalCollageSize.Width, finalCollageSize.Height);
            }

            if (parent.assignedSplit == Split.Vertical)
            {
                height = parent.size.Height;
                width = (int)Math.Round(height * current.aspectRatio);
            }
            else
            {
                // Horizontal split
                width = parent.size.Width;
                height = (int)Math.Round(width / current.aspectRatio);
            }

            return new Size(width, height);
        }


        /// <summary>
        /// Set coordinates for all nodes in the tree.
        /// </summary>
        private void SetImageCoordinates()
        {
            var currentNode = root;
            var nodeQueue = new Queue<BinaryNode>();

            // breadth-first
            while (currentNode != null)
            {
                if (currentNode.leftChild != null)
                {
                    nodeQueue.Enqueue(currentNode.leftChild);
                    nodeQueue.Enqueue(currentNode.rightChild);
                }

                currentNode.coordinates = CalculateImageCoordinates(currentNode);
                currentNode = nodeQueue.Count > 0 ? nodeQueue.Dequeue() : null;
            }
        }


        /// <summary>
        /// Calculate a node's coordinates. Works Top Down
        /// so all of the current node's parent's coordinates 
        /// need to be calculated first.
        /// </summary>
        private Point CalculateImageCoordinates(BinaryNode node)
        {
            var current = node;
            var parent = current.parent;

            int x = 0;
            int y = 0;

            if (parent == null)
            {
                // root coordinates are always 0,0
                return new Point(x, y);
            }

            // calculate coordinates
            if (current == parent.leftChild)
            {
                // left child always goes first and gets parent's coordinates
                x = parent.coordinates.X;
                y = parent.coordinates.Y;
            }
            else
            {
                // right child
                if (parent.assignedSplit == Split.Vertical)
                {
                    x = parent.coordinates.X + parent.size.Width - current.size.Width;
                    y = parent.coordinates.Y;
                }
                else
                {
                    y = parent.coordinates.Y + parent.size.Height - current.size.Height;
                    x = parent.coordinates.X;
                }
            }

            return new Point(x, y);
        }


        private void GetImageDetailsFromTree()
        {
            var currentNode = root;
            var nodeQueue = new Queue<BinaryNode>();

            while (currentNode != null)
            {
                if (currentNode.leftChild != null)
                {
                    nodeQueue.Enqueue(currentNode.leftChild);
                    nodeQueue.Enqueue(currentNode.rightChild);
                }
                else
                {
                    imageInformation.Add(new ImageDetails(currentNode.image, currentNode.coordinates, currentNode.size));
                }

                currentNode = nodeQueue.Count > 0 ? nodeQueue.Dequeue() : null;
            }
        }


        private Dictionary<String, double> determineBorderWidth(ImageDetails image)
        {
            var borderSizes = new Dictionary<String, double>();
            int borderWidth = CollagePreferences.borderWidth;
            int collageHeight = Convert.ToInt32(finalCollageSize.Height);
            int collageWidth = Convert.ToInt32(finalCollageSize.Width);
            int x = image.coordinates.X;
            int y = image.coordinates.Y;
            int width = image.size.Width;
            int height = image.size.Height;

            // if a side is external, full border width
            // otherwise, half the border width since it's sharing a border with another image
            double top = isTopExternal(y) ? borderWidth : (double)borderWidth / (double)2;
            double bottom = isBottomExternal(collageHeight, y, height) ? borderWidth : (double)borderWidth / (double)2;
            double left = isLeftExternal(collageWidth, x) ? borderWidth : (double)borderWidth / (double)2;
            double right = isRightExternal(collageWidth, x, width) ? borderWidth : (double)borderWidth / (double)2;

            borderSizes.Add("top", top);
            borderSizes.Add("bottom", bottom);
            borderSizes.Add("left", left);
            borderSizes.Add("right", right);
            return borderSizes;
        }


        private bool isTopExternal(int y)
        {
            return y == 0;
        }


        private bool isBottomExternal(int collageHeight, int y, int height)
        {
            return (y + height) == collageHeight;
        }


        private bool isLeftExternal(int collageWidth, int x)
        {
            return x == 0;
        }


        private bool isRightExternal(int collageWidth, int x, int width)
        {
            return (x + width) == collageWidth;
        }

    }
}
