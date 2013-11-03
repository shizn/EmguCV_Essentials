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
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace WorkwithImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Creating an image
        private void button1_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(320, 240, new Bgr(255, 0, 0));
            pictureBox1.Image = img1.ToBitmap();
        }
        //Loading image from file
        private void button2_Click(object sender, EventArgs e)
        {
            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(ofd.FileName);
                pictureBox1.Image = img1.ToBitmap();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Creating an image
            Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(320, 240, new Bgr(255, 0, 0));
            //yellow(0,255,255)
            Byte b1 = 255;
            Bgr yellow = new Bgr(0, 255, 255);
            //Change the color by iterating Data
            for (int i = 20; i < 60; i++)
            {
                for (int j = 20; j < 60; j++)
                {
                    img1.Data[i, j, 0] = 0;
                    img1.Data[i, j, 1] = b1;
                    img1.Data[i, j, 2] = b1;
                }
            }
            //Change the color by setting an Bgr color
            for (int i = 120; i < 160; i++)
            {
                for (int j = 20; j < 60; j++)
                {
                    img1[i, j] = yellow;
                }
            }
            //The best practice to reduce performance penalties
            byte[, ,] data = img1.Data;
            for (int i = 20; i < 60; i++)
            {
                for (int j = 100; j < 140; j++)
                {
                    //Avoid using c# property inside a loop can have a huge performance boost
                    data[i, j, 0] = 0;
                    data[i, j, 1] = b1;
                    data[i, j, 2] = b1;
                }
            }
            //Show the result
            pictureBox1.Image = img1.ToBitmap();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgBlue = new Image<Bgr, Byte>(320, 240, new Bgr(255, 0, 0));
            Image<Bgr, Byte> imgGreen = new Image<Bgr, Byte>(320, 240, new Bgr(0, 255, 0));
            Image<Bgr, Byte> imgRed = new Image<Bgr, Byte>(320, 240, new Bgr(0, 0, 255));
            //Blue + Green + Red = White
            //Operators Overload
            Image<Bgr, Byte> img1 = imgBlue + imgGreen + imgRed;
            pictureBox1.Image = img1.ToBitmap();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Gray, Byte> imgGray = new Image<Gray, Byte>(ofd.FileName);
                Image<Gray, Single> img1 = imgGray.Convert<Single>(delegate(Byte b) { return (Single)Math.Sin(b * b / 255.0); });
                pictureBox1.Image = img1.ToBitmap();
                
            }
             
            //Image<Gray, Byte> imgGray = new Image<Gray, Byte>(320, 240, new Gray(127));
            //Image<Gray, Single> img1 = imgGray.Convert<Single>(delegate(Byte b) { return (Single)Math.Sin(b * b / 255.0); });
            //pictureBox1.Image = img1.ToBitmap();

        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(ofd.FileName);
                pictureBox1.Image = img1.ToBitmap();
                //Convert an Image to XmlDocument
                StringBuilder sb1 = new StringBuilder();
                (new XmlSerializer(typeof(Image<Bgr, Byte>))).Serialize(new StringWriter(sb1),img1);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(sb1.ToString());
                //Save the XML file
                xmlDoc.Save("image.xml");
            }
        }
    }
}
