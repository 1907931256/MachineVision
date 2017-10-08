using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DrawMetaGraph
{
    class Circle : MetaTypeGraph
    {

        private Graphics graphics;
        private int radius;

        //构造方法
        public Circle(Graphics graphics, Point upLeftCoordinate)
        {
            this.fillColor = Color.Red;      //默认填充色
            this.radius = 25;
            this.graphics = graphics;
            this.upLeftCoordinate = upLeftCoordinate;
            
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
            int dx = p.X - (upLeftCoordinate.X + radius);
            int dy = p.Y - (upLeftCoordinate.Y + radius);
            if ((dx*dx + dy*dy) < radius*radius )
                return true;
            else
                return false;
        }

        //重写绘画的方法
        public override void draw()
        {
            if (isSelected == true)
            {
                graphics.FillPie(new SolidBrush(choosedColor), upLeftCoordinate.X, upLeftCoordinate.Y, 2*radius, 2*radius, 0, 360);
            }
            else
            {
                graphics.FillPie(new SolidBrush(fillColor), upLeftCoordinate.X , upLeftCoordinate.Y , 2*radius, 2*radius, 0, 360);
            }
        }


        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        
    }
}
