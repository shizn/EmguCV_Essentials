using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.Util;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace ContourFinding
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
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img1 = new Image<Bgr, byte>(new Bitmap(pictureBox1.Image));
            //Convert the img1 to grayscale and then filter out the noise
            Image<Gray, Byte> gray1 = img1.Convert<Gray, Byte>().PyrDown().PyrUp();
            //Canny Edge Detector
            Image<Gray, Byte> cannyGray = gray1.Canny(120, 180);
            
            //Lists to store triangles and rectangles
            List<Triangle2DF> triangleList = new List<Triangle2DF>();
            List<MCvBox2D> boxList = new List<MCvBox2D>();
            //New Memstorage
            using (MemStorage storage1 = new MemStorage())
                for (Contour<Point> contours1 = cannyGray.FindContours(); contours1 != null; contours1 = contours1.HNext)
                {
                    //Polygon Approximations
                    Contour<Point> contoursAP = contours1.ApproxPoly(contours1.Perimeter * 0.05, storage1);
                    //Use area to wipe out the unnecessary result
                    if (contours1.Area >= 200)
                    { 
                        //Use vertices to determine the shape
                        if (contoursAP.Total == 3)
                        {
                            //Triangle
                            Point[] points = contoursAP.ToArray();
                            triangleList.Add(new Triangle2DF(
                                points[0],
                                points[1],
                                points[2]
                                ));
                        }
                        else if (contoursAP.Total == 4)
                        { 
                            //Rectangle
                            bool isRectangle = true;
                            Point[] points = contoursAP.ToArray();
                            LineSegment2D[] edges = PointCollection.PolyLine(points, true);
                            //degree within the range of [75, 105] will be detected
                            for (int i = 0; i < edges.Length; i++)
                            {
                                double angle = Math.Abs(
                                   edges[(i + 1) % edges.Length].GetExteriorAngleDegree(edges[i]));
                                if (angle < 75 || angle > 105)
                                {
                                    isRectangle = false;
                                    break;
                                }
                            }
                            if (isRectangle)
                            {
                                boxList.Add(contoursAP.GetMinAreaRect());
                            }
                        }
                    }
                }
            //Draw result
            Image<Bgr, Byte> imageResult = img1.CopyBlank();
            foreach (Triangle2DF triangle in triangleList)
                imageResult.Draw(triangle, new Bgr(Color.LightSteelBlue), 5);
            foreach (MCvBox2D box in boxList)
                imageResult.Draw(box, new Bgr(Color.LimeGreen), 5);
            //Show result
            pictureBox2.Image = imageResult.ToBitmap();
        }
    }
}
