using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCollage
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();

            // set previous preferences if necessary
            //
            // SIZE
            int s = CollagePreferences.size;
            if(s == 500)
            {
                smallRadio.Checked = true;
            } else if(s == 1000)
            {
                mediumRadio.Checked = true;
            } else if(s == 2000)
            {
                largeRadio.Checked = true;
            } else if(s == 5000)
            {
                extraLargeRadio.Checked = true;
            } else
            {
                // custom
                customSizeRadio.Checked = true;
                customSizeBox.Value = s;
            }
            //
            // COUNT
            int c = CollagePreferences.count;
            if(c == 1)
            {
                oneRadio.Checked = true;
            } else if(c == 5)
            {
                fiveRadio.Checked = true;
            } else if(c == 10)
            {
                tenRadio.Checked = true;
            } else if(c == 20)
            {
                twentyRadio.Checked = true;
            } else if(c == 50)
            {
                fiftyRadio.Checked = true;
            }
            else
            {
                // custom
                customCountRadio.Checked = true;
                customCountBox.Value = c;
            }
            //
            // BORDER
            if(CollagePreferences.borderWidth != 0)
            {
                borderCheckBox.Checked = true;
                customBorderWidthBox.Value = CollagePreferences.borderWidth;
                borderColorLabel.BackColor = CollagePreferences.borderColor;
            }
            else
            {
                customBorderWidthBox.Value = 0;
                borderColorLabel.BackColor = Color.Black;
                customBorderWidthBox.Enabled = false;
                borderColorButton.Enabled = false;
                borderPx.Enabled = false;
            }
            //
            // ORIENTATION
            if(CollagePreferences.orientation == Orientation.Vertical)
            {
                orientationVRadio.Checked = true;
            } else if(CollagePreferences.orientation == Orientation.Horizontal)
            {
                orientationHRadio.Checked = true;
            }
            else
            {
                orientationBothRadio.Checked = true;
            }


        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.size = 5000;
            pixelSize.Text = "(5000px)";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.count = 50;
        }

        // SAVE BUTTON CLICK
        private void button2_Click(object sender, EventArgs e)
        {
            // before exiting set custom collage values
            if (customSizeRadio.Checked)
            {
                if (customSizeBox.Value != 0)
                {
                    CollagePreferences.size = Convert.ToInt32(customSizeBox.Value);
                }
            }

            if (customCountRadio.Checked)
            {
                if (customCountBox.Value != 0)
                {
                    CollagePreferences.count = Convert.ToInt32(customCountBox.Value);
                }
            }

            if (borderCheckBox.Checked)
            {
                if (customBorderWidthBox.Value != 0)
                {
                    CollagePreferences.borderWidth = Convert.ToInt32(customBorderWidthBox.Value);
                    CollagePreferences.borderColor = borderColorLabel.BackColor;
                }
                else
                {
                    // border value is 0, uncheck the checkbox
                    borderCheckBox.Checked = false;
                    CollagePreferences.borderWidth = 0;
                    CollagePreferences.borderColor = Color.Black;
                }
            } else
            {
                CollagePreferences.borderWidth = 0;
                CollagePreferences.borderColor = Color.Black;
            }

            this.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (customSizeRadio.Checked)
            {
                customSizeBox.Enabled = true;
                sizePx.Enabled = true;
            }
            else
            {
                customSizeBox.Enabled = false;
                sizePx.Enabled = false;

            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (customCountRadio.Checked)
            {
                customCountBox.Enabled = true;
            }else
            {
                customCountBox.Enabled = false;
            }
        }

        private void borderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (borderCheckBox.Checked == true)
            {
                customBorderWidthBox.Enabled = true;
                borderColorButton.Enabled = true;
                borderPx.Enabled = true;
            } else
            {
                customBorderWidthBox.Enabled = false;
                borderColorButton.Enabled = false;
                borderPx.Enabled = false;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.count = 20;
        }

        private void borderColorButton_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                borderColorLabel.BackColor = colorDialog1.Color;
            }
        }

        private void smallRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.size = 500;
            pixelSize.Text = "(500px)";
        }

        private void mediumRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.size = 1000;
            pixelSize.Text = "(1000px)";
        }

        private void largeRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.size = 2000;
            pixelSize.Text = "(2000px)";
        }

        private void oneRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.count = 1;
        }

        private void fiveRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.count = 5;
        }

        private void tenRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.count = 10;
        }

        private void orientationBothRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.orientation = Orientation.Both;
        }

        private void orientationHRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.orientation = Orientation.Horizontal;
        }

        private void orientationVRadio_CheckedChanged(object sender, EventArgs e)
        {
            CollagePreferences.orientation = Orientation.Vertical;
        }

        private void customSizeBox_ValueChanged(object sender, EventArgs e)
        {
            pixelSize.Text = "(" + customSizeBox.Value.ToString() + "px)";
        }
    }
}
