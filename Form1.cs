using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Diagnostics;

namespace AutoCollage
{
    public partial class Form1 : Form
    {
        private Dictionary<String, String> currentPics = new Dictionary<String, String>();
        private static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            pictureArea.BackColor = Color.LightGray;
            pictureArea.AllowDrop = true;
            pictureArea.DragEnter += new DragEventHandler(pictureBox_DragEnter);
            pictureArea.DragLeave += new EventHandler(pictureBox_DragLeave);
            pictureArea.DragDrop += new DragEventHandler(pictureBox_DragDrop);
            pictureArea.Paint += new PaintEventHandler(pictureArea_Paint);
        }

        private void pictureArea_Paint(object sender, PaintEventArgs e)
        {
            // Draw picture area border and hint.
            var g = e.Graphics;
            var font = new Font("Calibri", 24, FontStyle.Bold);
            var point = new Point(pictureArea.Left + 195, pictureArea.Top + 100);
            g.DrawString("Drop pictures here", font, Brushes.Gray, point);
            var border = new Pen(Color.Gray, 5);
            border.DashPattern = new float[] { 2.5F, 2 };
            g.DrawRectangle(border, new Rectangle(5, 5, pictureArea.Width - 15, pictureArea.Height - 15));

            if(currentPics.Count > 0)
            {
                // Remove picture area border and hint.
                g.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(0, 0, pictureArea.Width, pictureArea.Height));
            }

            border.Dispose();
            font.Dispose();
            g.Dispose();
        }

        void pictureBox_DragEnter(object sender, DragEventArgs e) {
            // Only highlight background if there are no pictures.
            if (currentPics.Count == 0)
            {
                pictureArea.BackColor = Color.LightBlue;
            }
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        void pictureBox_DragLeave(object sender, EventArgs e) {
            pictureArea.BackColor = Color.LightGray;
        }

        void pictureBox_DragDrop(object sender, DragEventArgs e) {
            pictureArea.BackColor = Color.LightGray;
            string[] pics = (string[])e.Data.GetData(DataFormats.FileDrop);
            addToPictureArea(pics);
            pictureArea.Invalidate();

        }

        /// <summary>
        /// Open Browse Folder dialog and call addToPictureArea 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string dir = folderBrowserDialog1.SelectedPath;
                string[] pics = Directory.GetFiles(dir);
                addToPictureArea(pics);
            }
        }

        /// <summary>
        /// Add array of pictures (FQPNs) to picture area
        /// </summary>
        /// <param name="pics"></param>
        private void addToPictureArea(string[] pics) {
            // Size of the picture and container inside the picture area
            Size containerSize = new Size(204, 225);
            Size picSize = new Size(205, 180);

            int offset = currentPics.Count;

            for (int i = 0; i < pics.Length; i++){
                if (isRecognisedImageFile(pics[i])) { 
                    // container 
                    TableLayoutPanel picContainer = new TableLayoutPanel();
                    picContainer.Name = "container" + (i + offset);
                    picContainer.Size = containerSize;
                    picContainer.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
                    picContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    picContainer.Padding = new Padding(5, 5, 5, 5);
                    // pic
                    PictureBox newPic = new PictureBox();
                    // used to create a unique name / no duplicates
                    long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    newPic.Name = "newPic" + milliseconds.ToString().Substring(6);
                    newPic.Size = picSize;
                    newPic.Visible = true;
                    newPic.Image = Image.FromFile(pics[i]);
                    newPic.Tag = pics[i];
                    newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                    newPic.Padding = new Padding(0,0,0,0);
                    newPic.Dock = DockStyle.Fill;
                    // add pic name and path to current pics
                    currentPics.Add(newPic.Name, pics[i]);
                    // remove link
                    LinkLabel removePicLink = new LinkLabel();
                    removePicLink.Name = "removePic" + (i + offset);
                    removePicLink.Text = "Remove";
                    removePicLink.Font = new Font("Arial", 8);
                    removePicLink.LinkColor = Color.Black;
                    removePicLink.LinkBehavior = LinkBehavior.HoverUnderline;
                    removePicLink.TextAlign = ContentAlignment.MiddleCenter;
                    removePicLink.LinkClicked += new LinkLabelLinkClickedEventHandler(remove_LinkClicked);
                    removePicLink.Dock = DockStyle.Fill;
                    // add pic and link to container
                    picContainer.Controls.Add(newPic, 0, 0);
                    picContainer.Controls.Add(removePicLink, 0, 1);
                    // add container to picture area
                    pictureArea.Controls.Add(picContainer);
                    pictureArea.Focus();
                    pictureArea.ScrollControlIntoView(picContainer);
                }
            }
        }

        /// <summary>
        /// Remove images from Picture Area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            String linkName = ((LinkLabel)sender).Name; 
            Control[] links = Controls.Find(linkName, true);
            if (links.Length > 0) {
                Control container = links[0].Parent;
                var pic = (PictureBox)container.GetNextControl(this, true);
                // release picture resource
                pic.Image.Dispose();
                pic.Dispose();
                // remove from picture area
                pictureArea.Controls.Remove(container);
                // remove from current pictures dictionary
                currentPics.Remove(pic.Name);
            }
            // redraw picture area
            pictureArea.Invalidate();
        }

        /// <summary>
        /// Provided by user "Dirk Vollmar" from StackOverflow.com
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        static string getMd5Hash(byte[] buffer)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(buffer);

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// Provided by user "Dirk Vollmar" from StackOverflow.com
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        static byte[] imageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        /*
         * Provided by user "dylmcc" from stackoverflow.com
         */
        public static bool isRecognisedImageFile(string fileName)
        {
            string targetExtension = System.IO.Path.GetExtension(fileName);
            if (String.IsNullOrEmpty(targetExtension))
                return false;
            else
                targetExtension = "*" + targetExtension.ToLowerInvariant();

            List<string> recognisedImageExtensions = new List<string>();

            foreach (System.Drawing.Imaging.ImageCodecInfo imageCodec in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
                recognisedImageExtensions.AddRange(imageCodec.FilenameExtension.ToLowerInvariant().Split(";".ToCharArray()));

            foreach (string extension in recognisedImageExtensions)
            {
                if (extension.Equals(targetExtension))
                {
                    return true;
                }
            }
            return false;
        }

        /*
         * Show Preference Dialog 
         */
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreferencesForm prefForm = new PreferencesForm();
            prefForm.Show();
        }

        /*
         * Exit Application
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
         * Show About Dialog 
         */
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            // do nothing if no images selected
            if (currentPics.Count == 0)
            {
                MessageBox.Show("Please select your pictures before attempting to create a collage", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            createAndSetSaveDirectory();
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // get save directory
            int collageCount = CollagePreferences.count;

            List<String> newCollageHashes = new List<String>();
            int attemptsToCreateUniqueCollage = 0;
            int imageCount = getNumImagesInDir(CollagePreferences.saveDir);

            // create collages
            for (var i = 0; i < collageCount; i++)
            {
                Bitmap newCollage = createCollage();

                // create hash of image and verify collage isn't a duplicate
                byte[] buffer = imageToByteArray(newCollage);
                String imageHash = getMd5Hash(buffer);
                if (newCollageHashes.Contains(imageHash))
                {
                    attemptsToCreateUniqueCollage++;
                    Console.WriteLine("attempt: {0}", attemptsToCreateUniqueCollage);
                    if (attemptsToCreateUniqueCollage == 10)
                    {
                        break;
                    }
                    // try again
                    i--;
                    continue;
                }
                attemptsToCreateUniqueCollage = 0;
                newCollageHashes.Add(imageHash);

                var collageName = "c" + imageCount++ + "-[" + newCollage.Width + "x" +
                    newCollage.Height + "].jpg";

                newCollage.Save(CollagePreferences.saveDir + collageName, ImageFormat.Jpeg);

                // free resource
                newCollage.Dispose();
                backgroundWorker1.ReportProgress(((int)(((double)(i+1) / (double)(collageCount)) * (double)100)));
            }
            // open collage directory and notify user
            openCollageDir();
            System.Media.SystemSounds.Asterisk.Play();

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            int currentCount = ((int)((double)e.ProgressPercentage / (double)100 * (double)CollagePreferences.count));
            collageStatusLabel.Text = currentCount.ToString() + " / " + CollagePreferences.count.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            progressBar1.Value = 0;
            collageStatusLabel.Text = "Done";
        }


        private Bitmap createCollage()
        {
            var root = createTree();
            root.printTree();

            List<Dictionary<String, String>> imagesInfo = root.getImageInfoFromTree();

            var newCollage = new Bitmap(root.Width, root.Height, PixelFormat.Format24bppRgb);
            var canvas = Graphics.FromImage(newCollage);

            // add individual images to newe collage
            for (var i = 0; i < imagesInfo.Count; i++)
            {
                // add collage size to images info to determine border size
                imagesInfo[i].Add("collageWidth", root.Width.ToString());
                imagesInfo[i].Add("collageHeight", root.Height.ToString());

                var imagePath = imagesInfo[i]["path"];
                var image = Image.FromFile(imagePath);
                var width = Convert.ToInt32(imagesInfo[i]["width"]);
                var height = Convert.ToInt32(imagesInfo[i]["height"]);
                var x = Convert.ToInt32(imagesInfo[i]["x"]);
                var y = Convert.ToInt32(imagesInfo[i]["y"]);

                canvas.DrawImage(image, x, y, width, height);

                if (CollagePreferences.borderWidth > 0)
                {
                    // get border size for each image
                    // if a side is external, full "border width"
                    // otherwise half the border, since it's sharing a border with another image
                    Dictionary<String, double> borderWidths = determineBorderWidth(imagesInfo[i]);
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
            }
            return newCollage;
        }

        private BinaryNode createTree()
        {
            var root = new BinaryNode();
            List<String> images = currentPics.Values.ToList();
            images = shuffleList(images);

            // contains path, width, height, and ratio
            var imagesInfo = new List<Dictionary<string, string>>();

            // already created the root node so (count -1)
            for (var i = 0; i < images.Count - 1; i++)
            {
                // we only want full trees so add 2 nodes at a time
                root.addNode();
                root.addNode();
            }

            root.setAllNodeSplits();

            // set user specified orientation (split) if necessary
            if (CollagePreferences.orientation != CollagePreferences.Orientation.B)
            {
                // Orientation.V = Split.H
                // Orientation.H = Split.V
                if(CollagePreferences.orientation == CollagePreferences.Orientation.V)
                {
                    root.Split1 = BinaryNode.Split.H;
                }
                else
                {
                    root.Split1 = BinaryNode.Split.V;
                }
            }

            for (var i = 0; i < images.Count; i++)
            {
                // set image info
                Image image = Image.FromFile(images[i]);
                var width = image.Width;
                var height = image.Height;
                double aspectRatio = (double)width / (double)height;
                var imageInfo = new Dictionary<string, string>();
                imageInfo.Add("path", images[i]);
                imageInfo.Add("width", width.ToString());
                imageInfo.Add("height", height.ToString());
                imageInfo.Add("ratio", aspectRatio.ToString());
                imagesInfo.Add(imageInfo);
            }

            root.setAllImages(imagesInfo);
            root.setAllAspectRatios();

            int collageSize = CollagePreferences.size;
            // aspectRatio = w / h;
            // w = h * aspectRatio;
            // h = w / aspectRatio;
            if (root.AspectRatio > 1)
            {
                // LANDSCAPE
                root.Width = collageSize;
                root.Height = (int)Math.Round(root.Width / root.AspectRatio, 0);
            }
            else
            {
                // PORTRAIT
                root.Height = collageSize;
                root.Width = (int)Math.Round(root.Height * root.AspectRatio, 0);
            }

            root.setAllDimensionsAndPositions();

            return root;
        }

        public static int getNumImagesInDir(String path)
        {
            int count = 0;
            string[] files = Directory.GetFiles(path);
            foreach(string f in files){
                if (isRecognisedImageFile(f))
                {
                    count++;
                }
            }
            return count;
        }

        public static List<String> shuffleList(List<String> list)
        {
            for(var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                var randIndex = random.Next(i, list.Count);
                list[i] = list[randIndex];
                list[randIndex] = temp;
            }
            return list;
        }

        /// <summary>
        /// Open new collage directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewButton_Click(object sender, EventArgs e)
        {
            if(currentPics.Count == 0)
            {
                MessageBox.Show("Please select your pictures before attempting to open collage directory.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            openCollageDir();
        }

        private void openCollageDir()
        {
            var saveDir = createAndSetSaveDirectory();
            Process.Start(@"" + saveDir.FullName);
        }

        private DirectoryInfo createAndSetSaveDirectory()
        {
            if (currentPics.Count == 0)
            {
                return null;
            }
            // store save directory in CollagePreferences
            String firstPic = currentPics.Values.First();
            String dir = Path.GetDirectoryName(firstPic);
            var saveDir = Directory.CreateDirectory(dir + "\\collage\\");
            CollagePreferences.saveDir = saveDir.FullName;
            return saveDir;
        }

        private Dictionary<String, double> determineBorderWidth(Dictionary<String, String> imageInfo)
        {
            var borderSizes = new Dictionary<String, double>();
            int borderWidth = CollagePreferences.borderWidth;
            int collageHeight = Convert.ToInt32(imageInfo["collageHeight"]);
            int collageWidth = Convert.ToInt32(imageInfo["collageWidth"]);
            int x = Convert.ToInt32(imageInfo["x"]);
            int y = Convert.ToInt32(imageInfo["y"]);
            int width = Convert.ToInt32(imageInfo["width"]);
            int height = Convert.ToInt32(imageInfo["height"]);

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
