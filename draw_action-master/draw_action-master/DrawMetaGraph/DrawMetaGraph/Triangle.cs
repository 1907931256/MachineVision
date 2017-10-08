using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawMetaGraph
{
    class Triangle : MetaTypeGraph
    {
        private Graphics graphics;
        private Point leftPoint1, rigntPoint2;
        private int height = 50;

     
        //构造方法
        public Triangle(Graphics graphics, Point upLeftCoordinate )
        {
            this.fillColor = Color.Black;      //默认填充色
            this.graphics = graphics;
            this.upLeftCoordinate = upLeftCoordinate;
            initPoints();
            
        }

        //重写的鼠标点击事件响应
        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isSelected = inSidePolygon(new Point(e.X, e.Y));
            }
            else
            {
                isSelected = false;
            }
            draw();

        }

        //判断鼠标是否选中图元
        private bool inSidePolygon(Point p)
        {
            bool isInside1 = false;
            bool isInside2 = false;
            bool isInside3 = false;
            if (upLeftCoordinate.X == leftPoint1.X)
            {
                if ((p.X - upLeftCoordinate.X) * (rigntPoint2.X - upLeftCoordinate.X) >= 0)
                    isInside1 = true;
            }
            else {
                int k1 = (leftPoint1.Y - upLeftCoordinate.Y) / (leftPoint1.X - upLeftCoordinate.X);
                int b = leftPoint1.Y - k1 * leftPoint1.X;
                if ((k1 * p.X + b - p.Y) * (rigntPoint2.X * k1 + b - rigntPoint2.Y) >= 0)
                    isInside1 = true;
            }
            if (upLeftCoordinate.X == rigntPoint2.X)
            {
                if ((p.X - upLeftCoordinate.X) * (leftPoint1.X - upLeftCoordinate.X) >= 0)
                    isInside2 = true;
            }
            else
            {
                int k2 = (rigntPoint2.Y - upLeftCoordinate.Y) / (rigntPoint2.X - upLeftCoordinate.X);
                int b = rigntPoint2.Y - k2 * rigntPoint2.X;
                if ((k2 * p.X + b - p.Y) * (leftPoint1.X * k2 + b - leftPoint1.Y) >= 0)
                    isInside2 = true;
            }
            if (rigntPoint2.X == leftPoint1.X)
            {
                if ((p.X - rigntPoint2.X) * (upLeftCoordinate.X - rigntPoint2.X) >= 0)
                    isInside3 = true;
            }
            else
            {
                int k3 = (leftPoint1.Y - rigntPoint2.Y) / (leftPoint1.X - rigntPoint2.X);
                int b = leftPoint1.Y - k3 * leftPoint1.X;
                if ((k3 * p.X + b - p.Y) * (upLeftCoordinate.X * k3 + b - upLeftCoordinate.Y) >= 0)
                    isInside3 = true;
            }

            return (isInside1 && isInside2 && isInside3);
                
        }

        //重写绘画的方法
        public override void draw()
        {
            initPoints();
            Point[] array = { upLeftCoordinate, leftPoint1, rigntPoint2 };
            if (isSelected == true)
            {
                graphics.FillPolygon(new SolidBrush(choosedColor), array);
            }
            else
            {
                graphics.FillPolygon(new SolidBrush(fillColor), array);
            }
        }

     
       private   void initPoints()
        {
            this.leftPoint1 =  new Point(upLeftCoordinate.X-height/2,upLeftCoordinate.Y+height);
            this.rigntPoint2 =  new Point(upLeftCoordinate.X + height / 2, upLeftCoordinate.Y + height); ;
        
        }

        
    }
}
