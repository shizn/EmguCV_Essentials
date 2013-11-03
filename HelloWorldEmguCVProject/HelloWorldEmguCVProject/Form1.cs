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

namespace HelloWorldEmguCVProject
{
    
    public partial class Form1 : Form
    {
        //Set the name of pop-up window
        String winname = "First Window";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create a window with the specific name
            CvInvoke.cvNamedWindow(winname);

            //Create an image of 480x180 with color yellow
            using (Image<Bgr, Byte> img1 = new Image<Bgr, byte>(480, 200, new Bgr(0, 255, 255)))
            {
                //Create a font
                MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);
                //Draw "Hello, world" on the yellow image;Start point is (25, 100) with color blue
                img1.Draw("Hello, world", ref font, new Point(25, 100), new Bgr(255, 0, 0));

                //Show the image in the window
                CvInvoke.cvShowImage(winname, img1.Ptr);
                //A key pressing event
                CvInvoke.cvWaitKey(0);
                //Destory the window
                CvInvoke.cvDestroyWindow(winname);
            }
        }
    }
}

