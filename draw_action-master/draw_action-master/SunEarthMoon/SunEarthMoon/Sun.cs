using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SunEarthMoon
{
    class Sun :Start
    {

        public Sun()
        {
        
        }

        public Sun(Point center, Point movingCenter, int radius, int movingRadius ,Graphics graphics,Color bgColor) 
        {
            this.center = center;
            this.movingCenter = movingCenter;
            this.radius = radius;
            this.movingRadius = movingRadius;
            this.graphics = graphics;
            this.bgcolor = bgColor;
            this.leftPoint = new Point((int)(center.X + 0.5 * radius), center.Y);
            this.length = 15;
            
        }

        public override void draw() 
        {
            graphics.FillPie(new SolidBrush(bgcolor), center.X-radius, center.Y-radius, 2 * radius, 2 * radius, 0, 360);
            graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(leftPoint.X, leftPoint.Y, length, length));
        }
    }
}
