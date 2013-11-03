using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Stitching;


namespace ImageStitching
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Clear data
                dataGridView1.Rows.Clear();
                //Store input images
                Image<Bgr, Byte>[] images = new Image<Bgr, Byte>[ofd.FileNames.Length];
                for (int i = 0; i < images.Length; i++)
                {
                    images[i] = new Image<Bgr, Byte>(ofd.FileNames[i]);
                    using (Image<Bgr, Byte> thumbnail = images[i].Resize(150, 150, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC, true))
                    {
                        DataGridViewRow row = dataGridView1.Rows[dataGridView1.Rows.Add()];
                        row.Cells["Image"].Value = thumbnail.ToBitmap();
                        row.Height = 150;
                    }
                }
                //Try Image Stitching
                try
                {
                    //Core Part
                    using (Stitcher stitcher = new Stitcher(
                        // GPU boost enable or disable
                        // Must specify false because it will cause error if true
                        // The bug is from OpenCV
                        false))
                    {
                        Image<Bgr, Byte> result = stitcher.Stitch(images);
                        imageBox1.Image = result;
                    }
                }
                finally
                {
                    foreach (Image<Bgr, Byte> image in images)
                    {
                        ((IDisposable)image).Dispose(); 
                    }
                }
            }
        }
    }
}
