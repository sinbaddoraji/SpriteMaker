using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteMaker
{
    public partial class Form1 : Form
    {
        private Image image;
        private IEnumerable<Image> sprites;

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.Image = (Image)image.Clone();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox1_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                //Anti-clockwise
                sprites = SpriteLib.RotateImageAnti_clockwise(image, int.Parse(textBox1.Text));
            }
            else if (radioButton2.Checked)
            {
                //Clockwise
                sprites = SpriteLib.RotateImageClockwise(image, int.Parse(textBox1.Text));
            }

            spritesViewer1.LoadImages(sprites);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int.Parse(textBox1.Text);
            }
            catch (Exception)
            {
                textBox1.Text = "0";
            }
        }


        Color mostCommon(Bitmap bmp)
        {
            List<Color> colors = new List<Color>();

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    colors.Add(bmp.GetPixel(x, y));
                }
            }

            return colors.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Color c = button3.BackColor;
            if(checkBox1.Checked)
            {
                c = mostCommon(new Bitmap(image));
            }
            byte r = c.R; //For Red colour

            Bitmap bmp = new Bitmap(image);
            bmp.MakeTransparent(c);

            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDialog.Color;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                button3.BackColor = mostCommon(new Bitmap(image));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (Image)image.Clone();
        }
    }
}
