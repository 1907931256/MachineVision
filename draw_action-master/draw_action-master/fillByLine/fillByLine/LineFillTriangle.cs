using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

namespace fillByLine
{
    class LineFillTriangle
    {

        private Graphics graphics;
        private Point[] points;

        public LineFillTriangle(Graphics graphics) 
        {
            this.graphics = graphics;
        }

         public LineFillTriangle(Graphics graphics,Point[] points) 
        {
            this.graphics = graphics;
             this.points = points;
        }

        //关于图元直线填充的主要实现
        //考虑了特殊情况
        //采用射线法实现
         public void draw() 
         {
             
             sortPoinsByY();
             Pen myPen = new Pen(Color.Red);
            // graphics.DrawPolygon(myPen, points);
            Thread.Sleep(500);
            graphics.DrawLine(myPen, points[0], points[2]);
            Thread.Sleep(500);
            graphics.DrawLine(myPen, points[2], points[1]);
            Thread.Sleep(500);
            graphics.DrawLine(myPen, points[1], points[0]);
                 
             
             if (points[0].X == points[2].X)
                 {
                     Point point1 = new Point(points[2].X, points[1].Y);
                     Point point2 = points[1];
                     double k1 = (points[0].Y - points[1].Y) * 1.0 / (points[0].X - points[1].X);
                     double k2 = (points[2].Y - points[1].Y) * 1.0 / (points[2].X - points[1].X);
                     int k = point2.X;
                     for (int i = points[1].Y; i > points[0].Y; i--)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         point2.Y = i;
                         int dx = (int)(Math.Abs(1 / k1) * Math.Abs(points[1].Y - i));
                         if (points[1].X > points[0].X)
                             point2.X = k + (dx > 0 ? -dx : dx);
                         else
                             point2.X = k + (dx > 0 ? dx : -dx);

                     }
                     point1 = new Point(points[2].X, points[1].Y);
                     point2 = points[1];
                     for (int i = points[1].Y; i < points[2].Y; i++)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         point2.Y = i;
                         int dx = (int)(Math.Abs(1 / k2) * Math.Abs(points[1].Y - i));
                         if (point2.X > points[2].X)
                             point2.X = k + (dx > 0 ? -dx : dx);
                         else
                             point2.X = k + (dx > 0 ? dx : -dx);

                     }
                 }
                 else if (points[0].X == points[1].X)
                 {

                     double k1 = (points[0].Y - points[2].Y) * 1.0 / (points[0].X - points[2].X);
                     double b1 = points[0].Y - k1 * points[0].X;
                     Point point1 = new Point((int)((points[1].Y - b1) / k1), points[1].Y);
                     Point point2 = points[1];
                     double k2 = (points[2].Y - points[1].Y) * 1.0 / (points[2].X - points[1].X);
                     int k = point1.X;
                     for (int i = points[1].Y; i > points[0].Y; i--)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         point2.Y = i;
                         int dx = (int)(Math.Abs(1 / k1) * Math.Abs(points[1].Y - i));
                         if (point1.X > points[0].X)
                             point1.X = k + (dx > 0 ? -dx : dx);
                         else
                             point1.X = k + (dx > 0 ? dx : -dx);

                     }
                     point1 = new Point((int)((points[1].Y - b1) / k1), points[1].Y);
                     point2 = points[1];
                     int x1 = point1.X;
                     int x2 = point2.X;
                     for (int i = points[1].Y; i < points[2].Y; i++)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         int dx1 = (int)(Math.Abs(1 / k1) * Math.Abs(points[1].Y - i));
                         if (point1.X > points[2].X)
                             point1.X = x1 + (dx1 > 0 ? -dx1 : dx1);
                         else
                             point1.X = x1 + (dx1 > 0 ? dx1 : -dx1);
                         point2.Y = i;
                         int dx2 = (int)(Math.Abs(1 / k2) * Math.Abs(points[1].Y - i));
                         if (point2.X > points[2].X)
                             point2.X = x2 + (dx2 > 0 ? -dx2 : dx2);
                         else
                             point2.X = x2 + (dx2 > 0 ? dx2 : -dx2);

                     }

                 }
                 else if (points[2].X == points[1].X)
                 {
                     double k1 = (points[0].Y - points[2].Y) * 1.0 / (points[0].X - points[2].X);
                     double b1 = points[0].Y - k1 * points[0].X;
                     Point point1 = new Point((int)((points[1].Y - b1) / k1), points[1].Y);
                     Point point2 = points[1];
                     double k2 = (points[0].Y - points[1].Y) * 1.0 / (points[0].X - points[1].X);
                     int x1 = point1.X;
                     int x2 = point2.X;
                     for (int i = points[1].Y; i > points[0].Y; i--)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         point2.Y = i;
                         int dx1 = (int)(Math.Abs(1 / k1) * Math.Abs(points[1].Y - i));
                         if (point1.X > points[0].X)
                             point1.X = x1 + (dx1 > 0 ? -dx1 : dx1);
                         else
                             point1.X = x1 + (dx1 > 0 ? dx1 : -dx1);
                         int dx2 = (int)(Math.Abs(1 / k2) * Math.Abs(points[1].Y - i));
                         if (point2.X > points[0].X)
                             point2.X = x2 + (dx2 > 0 ? -dx2 : dx2);
                         else
                             point2.X = x2 + (dx2 > 0 ? dx2 : -dx2);

                     }
                     point1 = new Point((int)((points[1].Y - b1) / k1), points[1].Y);
                     point2 = points[1];
                     x1 = point1.X;
                     for (int i = points[1].Y; i < points[2].Y; i++)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         int dx1 = (int)(Math.Abs(1 / k1) * Math.Abs(points[1].Y - i));
                         if (point1.X > points[2].X)
                             point1.X = x1 + (dx1 > 0 ? -dx1 : dx1);
                         else
                             point1.X = x1 + (dx1 > 0 ? dx1 : -dx1);
                         point2.Y = i;
                     }
                 }
                 else
                 {
                     double k1 = (points[0].Y - points[2].Y) * 1.0 / (points[0].X - points[2].X);
                     double b1 = points[0].Y - k1 * points[0].X;
                     Point point1 = new Point((int)((points[1].Y - b1) / k1), points[1].Y);
                     Point point2 = points[1];
                     double k2 = (points[0].Y - points[1].Y) * 1.0 / (points[0].X - points[1].X);
                     int x1 = point1.X;
                     int x2 = point2.X;
                     for (int i = points[1].Y; i > points[0].Y; i--)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         point2.Y = i;
                         int dx1 = (int)(Math.Abs(1 / k1) * Math.Abs(points[1].Y - i));
                         if (point1.X > points[0].X)
                             point1.X = x1 + (dx1 > 0 ? -dx1 : dx1);
                         else
                             point1.X = x1 + (dx1 > 0 ? dx1 : -dx1);
                         int dx2 = (int)(Math.Abs(1 / k2) * Math.Abs(points[1].Y - i));
                         if (point2.X > points[0].X)
                             point2.X = x2 + (dx2 > 0 ? -dx2 : dx2);
                         else
                             point2.X = x2 + (dx2 > 0 ? dx2 : -dx2);

                     }

                     point1 = new Point((int)((points[1].Y - b1) / k1), points[1].Y);
                     point2 = points[1];
                     double k3 = (points[2].Y - points[1].Y) * 1.0 / (points[2].X - points[1].X);
                     x1 = point1.X;
                     x2 = point2.X;
                     for (int i = points[1].Y; i < points[2].Y; i++)
                     {
                         Thread.Sleep(20);
                         graphics.DrawLine(myPen, point1, point2);
                         point1.Y = i;
                         int dx1 = (int)(Math.Abs(1 / k1) * Math.Abs(points[1].Y - i));
                         if (point1.X > points[2].X)
                             point1.X = x1 + (dx1 > 0 ? -dx1 : dx1);
                         else
                             point1.X = x1 + (dx1 > 0 ? dx1 : -dx1);
                         point2.Y = i;
                         int dx2 = (int)(Math.Abs(1 / k3) * Math.Abs(points[1].Y - i));
                         if (point2.X > points[2].X)
                             point2.X = x2 + (dx2 > 0 ? -dx2 : dx2);
                         else
                             point2.X = x2 + (dx2 > 0 ? dx2 : -dx2);

                     }

                 }

         
         }


         public void clear() 
         {
             Graphics.Clear(Color.Green);
         }


         public void drawPoint(Point p)    
         {
             graphics.FillEllipse(new SolidBrush(Color.Red), p.X, p.Y, 2, 2);
         }
         private void  sortPoinsByY() 
         {
             if (points.Length !=3) 
             {
                 MessageBox.Show("输入的点的个数必须为3个！", "注意提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
             }

             if(!isValidate())
             {
                 MessageBox.Show("输入的3个点无法构成三角形，因为他们在同一直线上！", "注意提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
             }
             Point temp;
             if ((points[0].Y - points[1].Y) * (points[0].Y - points[2].Y) <= 0) 
             {
                 temp = points[1];
                 points[1] = points[0];
                 points[0] = temp;
                
             }else if ((points[2].Y - points[1].Y) * (points[2].Y - points[0].Y) <= 0)
             {
                 temp = points[1];
                 points[1] = points[2];
                 points[2] = temp;
             }

             if (points[0].Y > points[2].Y)
             {
                 temp = points[0];
                 points[0] = points[2];
                 points[2] = temp;

             }
            
                 
         }

         private bool isValidate()
         {
             if (points[0].Y == points[1].Y && points[0].Y == points[2].Y)
                return false;
             else if (points[0].X == points[1].X && points[0].X == points[2].X)
                 return false;
             return true;

         }

        public Point[] Points
        {
            get { return points; }
            set { points = value; }
        }

        public Graphics Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }
    }
}
