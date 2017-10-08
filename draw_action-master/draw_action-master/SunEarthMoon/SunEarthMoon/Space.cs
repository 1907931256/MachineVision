using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace SunEarthMoon
{
    class Space
    {

        private Graphics graphics;
        private Start sun;
        private Start earth;
        private Start moon;
        private Point screenCenter;
        private bool isMoving = false;
        private double d_angle = 2 * Math.PI / 360;
        private double c_angle = 4 * Math.PI / 360;
        private  double angle ;
        private double cr_angle;

        public Space(Graphics graphics, Point screenCenter)
        {
            this.graphics = graphics;
            this.screenCenter = screenCenter;
            this.sun = new Sun(screenCenter,screenCenter,50,50,graphics,Color.Yellow);
            this.earth = new Earth(new Point(screenCenter.X + 200, screenCenter.Y), screenCenter, 25, 200, graphics, Color.Blue);
            this.moon = new Moon(new Point(earth.center.X+50,earth.center.Y),earth.center,15,50,graphics,Color.White);
            this.angle = d_angle;
            this.cr_angle = c_angle;
        }

        public void draw(bool isMoving) 
        {
           this.IsMoving = isMoving;
            ThreadStart threadStart = new ThreadStart(threadDraw);
            Thread thread = new Thread(threadStart);
            thread.Start();
        
        }

        public void drawBg() 
        {
            graphics.Clear(Color.Black);
        }


        private void threadDraw() 
        {
                int dx_e = 200;
                int dx_m = 50;
                while (true)
                {
                    sun.draw();
                    earth.draw();
                    moon.draw();
                    earth.center.X = screenCenter.X + (int)(dx_e * Math.Cos(angle));
                    earth.center.Y = screenCenter.Y + (int)(dx_e * Math.Sin(angle));
                    moon.center.X = earth.center.X + (int)(dx_m * Math.Cos(-angle * 12));
                    moon.center.Y = earth.center.Y + (int)(dx_m * Math.Sin(-angle * 12));
                    moon.movingCenter = earth.center;
                    angle += d_angle;
                    sun.leftPoint.X = sun.center.X + (int)(sun.radius * 0.6 * Math.Cos(-cr_angle));
                    sun.leftPoint.Y = sun.center.Y + (int)(sun.radius * 0.6 * Math.Sin(-cr_angle));
                    earth.leftPoint.X = earth.center.X + (int)(earth.radius * 0.6 * Math.Cos(cr_angle * 29));
                    earth.leftPoint.Y = earth.center.Y + (int)(earth.radius * 0.6 * Math.Sin(cr_angle * 29));
                    moon.leftPoint.X = moon.center.X + (int)(moon.radius * 0.6 * Math.Cos(-cr_angle));
                    moon.leftPoint.Y = moon.center.Y + (int)(moon.radius * 0.6 * Math.Sin(-cr_angle));
                    cr_angle += C_angle;
                    Thread.Sleep(400);
                    if (!IsMoving)
                        break;
                    drawBg();   

                }
        }

        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        public double D_angle
        {
            get { return d_angle; }
            set { d_angle = value; }
        }

        public double C_angle
        {
            get { return c_angle; }
            set { c_angle = value; }
        }
    }
}
