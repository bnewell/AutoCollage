using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCollage
{
    public class ImageDetails
    {
        public Image image { get; }
        public Size size { get; set; }
        public Point coordinates { get; set; }
        public String path { get; }

        public ImageDetails(Image image, Point coordinates, Size size)
        {
            this.image = image;
            this.coordinates = coordinates;
            this.size = size;
        }
    }
}
