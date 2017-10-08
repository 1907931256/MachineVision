using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DrawMetaGraph
{
    class SelectBox : MetaTypeGraph 
    {

        //属于盒子的基本要素，包括左上方点以及宽度，高度
        private int width;
        private int height;
        Graphics graphis;

        //构造方法
        public SelectBox(Graphics graphics, Point upLeft)
        {
            this.fillColor = Color.Blue;      //默认填充色
            this.graphis = graphics;        
            upLeftCoordinate = upLeft;
            this.width = 50;                //默认宽度
            this.height = 50;               //默认的高度
        }

        //重写的鼠标点击事件响应
        public override  void MouseDown(MouseEventArgs e)
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
            if (p.X > upLeftCoordinate.X && p.X < upLeftCoordinate.X + width && p.Y > upLeftCoordinate.Y && p.Y < upLeftCoordinate.Y + height)
                return true;
            else
                return false;
        }

        //重写绘画的方法
        public override void draw()
        {
            if (isSelected == true)
            {
                graphis.FillRectangle(new SolidBrush(choosedColor),upLeftCoordinate.X,upLeftCoordinate.Y,width,height);
            }
            else
            {
                graphis.FillRectangle(new SolidBrush(fillColor), upLeftCoordinate.X, upLeftCoordinate.Y, width, height);
            }
        }

        //有关参数的get和set方法
        public int Width
        {
            get { return width; }
            set { width = value; }
        }


        public int Height
        {
            get { return height; }
            set { height = value; }
        }

       
    }
}
