using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SunEarthMoon
{
    class  Start
    {
        public  Point center;          // 星球的球星
        public  Point movingCenter;    //星球公转轨迹的中心
        public  int radius;            //星球的半径
        public  int movingRadius;      //星球公转的半径
        public  Graphics graphics;      //绘制的画布
        public  Color bgcolor;          //星球的背景色
        public Point leftPoint;
        public int length;

        public virtual void draw() {

        }

    }
}
