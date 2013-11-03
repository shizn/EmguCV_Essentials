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

namespace CameraCapture
{
    public partial class Form1 : Form
    {
        //Frame
        Image<Bgr, byte> current;
        //Webcam
        Capture webcam = new Capture();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //run until close
            Application.Idle += new EventHandler(processCamera);
        }
        private void processCamera(object sender, EventArgs e)
        {
            current = webcam.QueryFrame();
            // Flip because forecam is MIRRORED
            current = current.Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
            pictureBox1.Image = current.Bitmap;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Get the current frame
            pictureBox2.Image = pictureBox1.Image;
        }
    }
}

