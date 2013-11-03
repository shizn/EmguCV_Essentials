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


namespace WorkwithMatrices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Matrix<Double> matrix1 = new Matrix<Double>(5, 7);
            matrixBox1.Matrix = matrix1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Creat a matrix
            Matrix<Double> matrix1 = new Matrix<Double>(5, 7);
            double element = 0;
            //Set the elements
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    matrix1.Data[i, j] = element;
                    element++;
                }
            }
            //Show the result
            matrixBox1.Matrix = matrix1;
        }
    }
}
