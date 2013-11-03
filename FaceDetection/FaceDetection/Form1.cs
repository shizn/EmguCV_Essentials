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
using Emgu.CV.Util;

namespace FaceDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Load the image
            Image<Bgr, Byte> image = new Image<Bgr, Byte>(openFileDialog1.FileName);
            //Use List to store faces and eyes
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            //Pre-trained cascade
            CascadeClassifier face = new CascadeClassifier("haarcascade_frontalface_default.xml");
            CascadeClassifier eye = new CascadeClassifier("haarcascade_eye.xml");
            //The input image of Cascadeclassifier must be grayscale
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>();
            //Face detection
            Rectangle[] facesDetected = face.DetectMultiScale(
                gray,               //image
                1.1,                //scaleFactor
                10,                 //minNeighbors
                new Size(20, 20),   //minSize
                Size.Empty);        //maxSize
            faces.AddRange(facesDetected);
            //Eyes detection
            foreach (Rectangle f in facesDetected)
            {
                gray.ROI = f;
                Rectangle[] eyesDetected = eye.DetectMultiScale(
                    gray,
                    1.1,
                    10,
                    new Size(20, 20),
                    Size.Empty);
                gray.ROI = Rectangle.Empty;
                foreach (Rectangle ey in eyesDetected)
                {
                    Rectangle eyeRect = ey;
                    eyeRect.Offset(f.X, f.Y);
                    eyes.Add(eyeRect);
                }
            }
            //Draw detected area
            foreach (Rectangle face1 in faces)
                image.Draw(face1, new Bgr(Color.Red), 2);
            foreach (Rectangle eye1 in eyes)
                image.Draw(eye1, new Bgr(Color.Blue), 2);
            //Show image
            pictureBox1.Image = image.Bitmap;

        }
    }
}
