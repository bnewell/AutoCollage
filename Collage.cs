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

        private Random random = new Random();
        private int numImagesAssignedToNodes = 0;

        public Collage(List<String> imagePaths, int collageLength, Orientation collageOrientation, int borderWidth, Color borderColor)
        {
            this.images = convertPathsToImages(imagePaths);
            this.collageLength = collageLength;
            this.collageOrientation = collageOrientation;
            this.borderWidth = borderWidth;
            this.borderColor = borderColor;
        }


        private static List<Image> convertPathsToImages(List<String> pathsToImages)
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
                    Console.Write("[-] Unable to convert path \"" + image + "\" to image.\nException: "  + e);
                }
                if (theImage != null)
                {
                    listOfImages.Add(Image.FromFile(image));
                }
            }
            return listOfImages;
        }


        public Bitmap CreateCollage()
        {
            CreateCollageTree();

            var newCollage = new Bitmap(finalCollageSize.Width, finalCollageSize.Height, PixelFormat.Format32bppRgb);
            var canvas = Graphics.FromImage(newCollage);

            // add individual images to new collage
            foreach(ImageDetails image in imageInformation)
            {
                int x = image.coordinates.X;
                int y = image.coordinates.Y;
                int width = image.size.Width;
                int height = image.size.Height;

                canvas.DrawImage(image.image, x, y, width, height);
                canvas = drawBorderOnCanvas(canvas, image, x, y, width, height);
            }
            return newCollage;
        }


        /// <summary>
        /// Get border size for each image.
        //  If a side is external, use the full "border width".
        //  Otherwise, half the border since it's sharing a border with another image.
        /// </summary>
        private Graphics drawBorderOnCanvas(Graphics canvas, ImageDetails image, int x, int y, int width, int height)
        {
            if (CollagePreferences.borderWidth > 0)
            {
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
            }
            return canvas;
        }


        private void CreateCollageTree()
        {

            SetCollageSplit();
            InitializeFullBinaryTree();

            VisitTree(node =>
            {
                SetNodeSplit(node);
                SetImageToLeafNode(node);
            });

            VisitTree(node =>
            {
                SetAspectRatio(node);
            });

            SetFinalCollageSize();

            VisitTree(node =>
            {
                SetNewImageSize(node);
            });

            VisitTree(node =>
            {
                SetImageCoordinate(node);
            });

            VisitTree(node =>
            {
                GetImageDetailsFromTree(node);
            });

        }


        private void SetCollageSplit()
        {
            if (collageOrientation != Orientation.Both)
            {
                // Orientation.Vertical = Split.Horizontal
                // Orientation.Horizontal = Split.Vertical
                root.assignedSplit = collageOrientation == Orientation.Vertical ? Split.Horizontal : Split.Vertical;
            }
            else
            {
                root.assignedSplit = GetRandomSplit();
            }
        }


        private Split GetRandomSplit()
        {
            return random.Next(2) == 0 ? Split.Horizontal : Split.Vertical;
        }


        /// <summary>
        /// Create collage tree. It needs to be a full binary tree,
        /// so add 2 nodes for every one image. Also subtract one from
        /// image count because the root node has already been created.
        /// </summary>
        private void InitializeFullBinaryTree()
        {
            for (int i = 0; i < images.Count - 1; i++)
            {
                root.addNode();
                root.addNode();
            }
        }


        public void VisitTree(Action<BinaryNode> processNode)
        {
            if (processNode == null)
            {
                throw new ArgumentNullException("revivor");
            }

            var currentNode = root;
            var nodeQueue = new Queue<BinaryNode>();

            while (currentNode != null)
            {
                if (!currentNode.IsLeaf())
                {
                    nodeQueue.Enqueue(currentNode.leftChild);
                    nodeQueue.Enqueue(currentNode.rightChild);
                }
                processNode(currentNode);
                currentNode = nodeQueue.Any() ? nodeQueue.Dequeue() : null;
            }
        }


        /// <summary>
        /// Assign inner nodes a 'Vertical' or 'Horizontal' split at random (50/50 chance)
        /// </summary>
        private void SetNodeSplit(BinaryNode node)
        {
            if (!node.IsLeaf())
            {
                if (node.assignedSplit == Split.None)
                {
                    node.assignedSplit = GetRandomSplit();
                }
            }
        }


        /// <summary>
        /// Assign images and aspect ratios to all leaf nodes
        /// </summary>
        private void SetImageToLeafNode(BinaryNode node)
        {
            if (node.IsLeaf())
            {
                if (numImagesAssignedToNodes < images.Count)
                {
                    Image image = images[numImagesAssignedToNodes++];
                    node.image = image;
                    node.aspectRatio = (float)image.Width / (float)image.Height;
                }
            }
        }


        /// <summary>
        /// Set aspect ratios of all nodes in the tree
        /// </summary>
        private void SetAspectRatio(BinaryNode node)
        {
            node.aspectRatio = CalculateAspectRatio(node);
        }


        /// <summary>
        /// Calculate and return aspect ratio of current node.
        /// </summary>
        /// <returns>Aspect ratio of current node.</returns>
        private double CalculateAspectRatio(BinaryNode node)
        {
            var currentNode = node;
            var aspectRatio = 0.0;

            if (!currentNode.IsLeaf())
            {
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

        /// <summary>
        /// Set image sizes for all nodes in the tre
        /// </summary>
        private void SetNewImageSize(BinaryNode node)
        {
            node.size = CalculateNewImageSize(node);
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
        private void SetImageCoordinate(BinaryNode node)
        {
            node.coordinates = CalculateImageCoordinates(node);
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


        private void SetFinalCollageSize()
        {
            // Depending on the orientation, the initial "size" of the collage
            // will specify either the vertical or horizontal pixels. We must calculate
            // the other dimension using the aspect ratio and simple algebra equations.
            //
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


        private void GetImageDetailsFromTree(BinaryNode node)
        {
            if (node.IsLeaf())
            {
                imageInformation.Add(new ImageDetails(node.image, node.coordinates, node.size));
            }
        }


        private Dictionary<String, double> determineBorderWidth(ImageDetails image)
        {
            var borderSizes = new Dictionary<String, double>();
            int collageHeight = finalCollageSize.Height;
            int collageWidth = finalCollageSize.Width;
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
