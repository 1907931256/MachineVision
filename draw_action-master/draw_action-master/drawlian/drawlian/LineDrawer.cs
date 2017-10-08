using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace drawlian
{
    class LineDrawer
    {

        private Graphics graphics;               //用于绘画的graphics
        private double k , b;                   //直线的斜率k和偏移b
        private int start, end, pixelWidth;     //直线的起始x和结束x以及像素大小
        private int graphicsHeight;             //画布高度
        private int graphicsWidth;              //画布宽度
        private bool drawGridFlag = false;      //是否已画过表格
        private bool lineDrawed = false;        //是否已画过直线
        private Color backColor = Color.Green;
        private Color lineColor = Color.Yellow;

        //构造方法
        public LineDrawer(Graphics graphics)
        {
            this.graphics = graphics;
        }

        //绘画直线
        public void drawLine() 
        {
            Point pStart = new Point(start, (int)(start * k + b));
            Point pEnd = new Point(end,(int)(end * k + b));
            drawlineByTwoPoint(pStart, pEnd , pixelWidth);
        
        }

        //根据两个点绘画任意斜率直线实现方法
        private void drawlineByTwoPoint(Point pStart, Point pEnd, int pixelWidth)
        {
            int x1 = pStart.X, y1 = pStart.Y, x2 = pEnd.X, y2 = pEnd.Y;
            int x = x1, y = y1;
	        int a = y1 - y2, c = x2 - x1;
            int cx =1 , cy =1;
            if( c<0 ){
                c = -c;
                cx = -1;
            }
            if( a>0 ){
                a = -a;
                cy = -1;
            }
	        graphics.FillRectangle(new SolidBrush(lineColor), new Rectangle(x, y, (int)(pixelWidth/1.4), (int)(pixelWidth/1.4)));
	        int d, d1, d2;
	        if (-a <= b)		// 斜率绝对值 <= 1
	        {
		        d = 2 * a + c;
		        d1 = 2 * a;
		        d2 = 2 * (a + c);
		        while(x != x2)
		        {
			        if (d < 0)
                    {
                          y += cy; c += d2;
                    } else
				        d += d1;
			        x += cx;
                    graphics.FillRectangle(new SolidBrush(lineColor), new Rectangle(x, y, (int)(pixelWidth / 1.4), (int)(pixelWidth / 1.4)));
		        }
	        }else				// 斜率绝对值 > 1
            {
		        d = 2 * c + a; 
		        d1 = 2 * c;
		        d2 = 2 * (a + c);
		        while(y != y2) 
		        {
                    if (d < 0)
                        d += d1;
                    else {
                        x += cx;
                        d += d2; 
                    }
			        y += cy;
                    graphics.FillRectangle(new SolidBrush(lineColor), new Rectangle(x, y, (int)(pixelWidth / 1.4), (int)(pixelWidth / 1.4)));
		        } 
	        }
        }
       
        //当画布重置时重画所有情况
        public void resize(Graphics graphics)
        {
            this.graphics = graphics;
            clearBackground();
            if (drawGridFlag)
            {
                drawGrid();
            }
            if (lineDrawed)
            {
                drawLine();
            }
            if (lineDrawed && drawGridFlag) 
            { 
                drawGridAndLine();
            }
            

        }

        //画背景图
        public void drawBackground() 
        {
            clearBackground();
        
        }

        //绘制表格
        public void drawGrid() 
        {
            int xgridCount = 1;
            int yGridCount = 1;
            int sx, sy, ex, ey;
            try
            {
                xgridCount = graphicsHeight / pixelWidth;
                yGridCount = graphicsWidth / pixelWidth;
            }
            catch (DivideByZeroException e)
            {
                MessageBox.Show("分辨率不能为0！", "亲~注意提示0~", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            for (int i = 0; i < xgridCount; i++)
            {
                sx = 0;
                sy = i * pixelWidth;
                ex = graphicsWidth;
                ey = i * pixelWidth;
                graphics.DrawLine(new Pen(Color.Black), sx, sy, ex, ey);
            }
            for (int i = 0; i < yGridCount; i++)
            {
                sx = i * pixelWidth;
                sy = 0;
                ex = i * pixelWidth;
                ey = graphicsHeight;
                graphics.DrawLine(new Pen(Color.Black), sx, sy, ex, ey);
            }
        
        }

        //绘制表格和直线
        public void drawGridAndLine()
        {
            drawLine();
            drawGrid();
        }

        //清屏处理
        private void clearBackground() 
        {
            graphics.Clear(backColor);
        
        }

        //私有变量的get和set方法
        public double K
        {
            get { return k; }
            set { k = value; }
        }

        public double B
        {
            get { return b; }
            set { b = value; }
        }

        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        public int End
        {
            get { return end; }
            set { end = value; }
        }

        public int PixelWidth
        {
            get { return pixelWidth; }
            set { pixelWidth = value; }
        }

        public bool DrawGridFlag
        {
            get { return drawGridFlag; }
            set { drawGridFlag = value; }
        }

        public int GraphicsHeight
        {
            get { return graphicsHeight; }
            set { graphicsHeight = value; }
        }

        public int GraphicsWidth
        {
            get { return graphicsWidth; }
            set { graphicsWidth = value; }
        }

        public bool LineDrawed
        {
            get { return lineDrawed; }
            set { lineDrawed = value; }
        }

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }



    }
}
