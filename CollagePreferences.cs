using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCollage
{
    static class CollagePreferences
    {
        public static int size = 1000;
        public static int count = 5;
        public static int borderWidth = 0;
        public static Color borderColor = Color.Black;
        public static String saveDir = "";

        // Orientation.H = Split.V
        // Orientation.V = Split.H
        public static Orientation orientation = Orientation.Both;
    }
}
