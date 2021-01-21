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
        private string workingDirectory;

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void Generate_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                //Anti-clockwise
                sprites = SpriteLib.RotateImageAnti_clockwise(pictureBox1.Image, int.Parse(textBox1.Text));
            }
            else if (radioButton2.Checked)
            {
                //Clockwise
                sprites = SpriteLib.RotateImageClockwise(pictureBox1.Image, int.Parse(textBox1.Text));
            }

            spritesViewer1.LoadImages(sprites);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
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

        private void MakeTransparent_Click(object sender, EventArgs e)
        {
            Color c = button3.BackColor;
            if(checkBox1.Checked) c = SpriteLib.GetMostCommon(new Bitmap(image));

            Bitmap bmp = new Bitmap(image);
            bmp.MakeTransparent(c);

            pictureBox1.Image = bmp;
        }

        private void ColourButton_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDialog.Color;
            }
        }

        private void UseBackground_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                button3.BackColor = SpriteLib.GetMostCommon(new Bitmap(image));
            }
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (Image)image.Clone();
        }

        private void selectWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                workingDirectory = folderBrowserDialog.SelectedPath;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while(workingDirectory == null)
            {
                MessageBox.Show("Please select an output directory");
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    workingDirectory = folderBrowserDialog.SelectedPath;
                }
            }

            int i = 0;
            foreach (var sprite in sprites)
            {
                sprite.Save($"{workingDirectory}\\{i}.png", System.Drawing.Imaging.ImageFormat.Png);
                i++;
            }
            MessageBox.Show($"your images are in {workingDirectory}");
        }
    }
}
