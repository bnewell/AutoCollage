using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCollage
{
    public static class CollagePreferences
    {
        public static int size = 1000;
        public static int count = 5;
        public static int borderWidth = 0;
        public static Color borderColor = Color.Black;
        public static String saveDir = "";

        // Orientation.H = Split.V
        // Orientation.V = Split.H
        public enum Orientation { H, V, B};
        public static Orientation orientation = Orientation.B;
    }
}
