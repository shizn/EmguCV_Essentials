using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace CannyEdgeDetector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(ofd.FileName);
                pictureBox1.Image = img1.ToBitmap();
                //Convert the img1 to grayscale and then filter out the noise
                Image<Gray, Byte> gray1 = img1.Convert<Gray, Byte>().PyrDown().PyrUp();
                //Canny Edge Detector
                Image<Gray, Byte> cannyGray = gray1.Canny(120, 180);
                pictureBox2.Image = cannyGray.ToBitmap();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Load the image that has been pre-processed
            Image<Gray, Byte> cannyGray = new Image<Gray, byte>(new Bitmap(pictureBox2.Image));
            //Call HoughLinesBinary method
            LineSegment2D[] lines = cannyGray.HoughLinesBinary(1, Math.PI / 45.0, 20, 30, 10)[0];
            //Draw lines
            Image<Bgr, Byte> imageLines = new Image<Bgr, byte>(cannyGray.Width, cannyGray.Height);
            foreach (LineSegment2D line in lines)
            {
                imageLines.Draw(line, new Bgr(Color.DeepSkyBlue), 5);
            }
            //Show result
            pictureBox2.Image = imageLines.ToBitmap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(ofd.FileName);
                pictureBox1.Image = img1.ToBitmap();
                //Convert the img1 to grayscale and then filter out the noise
                Image<Gray, Byte> gray1 = img1.Convert<Gray, Byte>().PyrDown().PyrUp();
                //Call HoughCircles (Canny included)
                CircleF[] circles = gray1.HoughCircles(
                    new Gray(180),  //cannyThreshold
                    new Gray(120),  //accumulatorThreshold
                    2.0,            //dp
                    15.0,           //minDist
                    5,              //minRadius
                    0               //maxRadius
                    )[0];
                //Draw circles
                Image<Bgr, Byte> imageCircles = img1.CopyBlank();
                foreach (CircleF circle in circles)
                {
                    imageCircles.Draw(circle, new Bgr(Color.Yellow), 5);
                }
                //Show result
                pictureBox2.Image = imageCircles.ToBitmap();
            }
        }
    }
}
