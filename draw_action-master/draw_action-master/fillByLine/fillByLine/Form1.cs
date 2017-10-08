using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace fillByLine
{
    public partial class Form1 : Form
    {

        private LineFillTriangle triangle;
        private Point[] points = new Point[3];
        private int clickNumber = 0;
        public Form1()
        {
            InitializeComponent();
           // points[0] = new Point(400,200);
           // points[1] = new Point(600, 400);
           // points[2] = new Point(100, 600);
            this.triangle = new LineFillTriangle(this.CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            triangle.clear();
            MessageBox.Show("在画布上用鼠标左键任意点击三个点之后，\n即可看到直线填充图元的动画。", "使用方法", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                if (clickNumber++ < 3) 
                {
                   
                    points[clickNumber-1].X = e.X;
                    points[clickNumber-1].Y = e.Y;
                    triangle.drawPoint(points[clickNumber - 1]);
                }

                if (clickNumber == 3) 
                {
                    triangle.Points = points;
                    triangle.draw();
                    clickNumber = 0;
                }
                
            }
        }
    }
}
