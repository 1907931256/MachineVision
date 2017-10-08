using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SunEarthMoon
{
    public partial class Form1 : Form
    {
        private Space space;
        private bool isMoving = false;
        private double i = 1;
        public Form1()
        {
            InitializeComponent();
            space = new Space(this.panel2.CreateGraphics(),new Point(this.panel2.Width/2,this.panel2.Height/2));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            space.drawBg();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isMoving )
                isMoving = true;
            space.draw(isMoving);

            label1.Text = "速度：" + i + "*X";

        }

        private void button2_click(object sender, EventArgs e)
        {
            
            space.IsMoving = false;
            isMoving = false;
            label1.Text = "暂停中...";
        }

        private void button3_click(object sender, EventArgs e)
        {
            if (isMoving)
            {
                i = i * 2;
                space.D_angle = 2.0 * space.D_angle;
                space.C_angle = 2.0 * space.C_angle;
                label1.Text = "速度：" + i + "*X";
            }
            else {
                label1.Text = "暂停中...";
            }
            
        }

        private void button4_click(object sender, EventArgs e)
        {
            if (isMoving)
            {
                i = i / 2.0;
                space.D_angle = space.D_angle / 2.0;
                label1.Text = "速度：" + i + "*X";
            }
            else {
                label1.Text = "暂停中...";
            }
            

        }

    }
}
