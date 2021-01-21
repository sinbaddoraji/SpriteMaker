using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteMaker
{
    public partial class SpritesViewer : UserControl
    {
        public int previewWidth = 100;
        public int previewHeight = 100;
        private IEnumerable<Image> images;
        public SpritesViewer()
        {
            InitializeComponent();
        }

        private void SpritesViewer_Load(object sender, EventArgs e)
        {

        }

        public void LoadImages(IEnumerable<Image> images)
        {
            this.images = images;
            flowLayoutPanel1.Controls.Clear();

            foreach (var image in images)
            {
                Panel preview = new Panel
                {
                    Size = new Size(previewWidth, previewHeight)
                };
                preview.BackgroundImage = image.GetThumbnailImage(previewWidth, previewHeight, null, IntPtr.Zero);

                flowLayoutPanel1.Controls.Add(preview);
            }
        }

        public void RefreshPreview()
        {
            LoadImages(images);
        }
    }
}
