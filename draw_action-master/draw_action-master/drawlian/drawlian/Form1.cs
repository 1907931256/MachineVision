using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace drawlian
{
    public partial class Form1 : Form
    {
        private LineDrawer lineDrawer;
       
        public Form1()
        {
            InitializeComponent();
            lineDrawer = new LineDrawer(this.panel2.CreateGraphics()); 
             
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            lineDrawer.drawBackground();
        }


        private void start_Click(object sender, EventArgs e)
        {
            getData();
            lineDrawer.LineDrawed = true;
            drawSomething();
            
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
              lineDrawer.resize(this.panel2.CreateGraphics());
            
        }

        private void getData() 
        {
            try
            {
                lineDrawer.K = Double.Parse(textBox1.Text);
                lineDrawer.B = Double.Parse(textBox2.Text);
                lineDrawer.Start = int.Parse(textBox3.Text);
                lineDrawer.End = int.Parse(textBox4.Text);
                lineDrawer.PixelWidth = int.Parse(textBox5.Text);

            }
            catch (FormatException)
            {
                DialogResult dr = MessageBox.Show("输入参数不符合要求，请检查是否为空或含有字母和符号！", "亲~注意提示0~", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void gridDraw_Click(object sender, EventArgs e)
        {
            setSize();
            lineDrawer.PixelWidth = int.Parse(textBox5.Text);
            lineDrawer.drawBackground();
            lineDrawer.drawGrid();
            lineDrawer.DrawGridFlag = true;
            if (lineDrawer.LineDrawed)
                lineDrawer.drawGridAndLine();
           
        }

        private void setSize() 
        {
            lineDrawer.GraphicsHeight = panel2.Height;
            lineDrawer.GraphicsWidth = panel2.Width;
        }

        public void cancelGrid(object sender, EventArgs e) 
        {
            DialogResult dr = MessageBox.Show("确认删除表格？", "亲~注意提示0~", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                lineDrawer.DrawGridFlag = false;
                lineDrawer.drawBackground();
                if (lineDrawer.LineDrawed)
                {
                    lineDrawer.drawLine();
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "绿色") 
            {
                lineDrawer.BackColor = Color.Green;
            }
            else if (comboBox1.Text == "黄色") 
            {
                lineDrawer.BackColor = Color.Yellow;
            }
            else if (comboBox1.Text == "蓝色") 
            {
                lineDrawer.BackColor = Color.Blue;
            }
            else if (comboBox1.Text == "灰色")
            {
                lineDrawer.BackColor = Color.Gray;
            }
            else if (comboBox1.Text == "红色")
            {
                lineDrawer.BackColor = Color.Red;
            }

            drawSomething();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "绿色")
            {
                lineDrawer.LineColor = Color.Green;
            }
            else if (comboBox2.Text == "黄色")
            {
                lineDrawer.LineColor = Color.Yellow;
            }
            else if (comboBox2.Text == "蓝色")
            {
                lineDrawer.LineColor = Color.Blue;
            }
            else if (comboBox2.Text == "灰色")
            {
                lineDrawer.LineColor = Color.Gray;
            }
            else if (comboBox2.Text == "红色")
            {
                lineDrawer.LineColor = Color.Red;
            }

            drawSomething();
        }

        private void drawSomething() {

            lineDrawer.drawBackground();
            if (lineDrawer.DrawGridFlag)
            {
                lineDrawer.drawGrid();
            }
            if (lineDrawer.LineDrawed)
            {
                lineDrawer.drawLine();
            }
            if (lineDrawer.DrawGridFlag && lineDrawer.LineDrawed && canDrawGrid(textBox5.Text))
            {
                lineDrawer.drawGridAndLine();
            }
        
        }

        private void textBox5_changed(object sender, EventArgs e)
        {
            if (canDrawGrid(textBox5.Text))
            {
                getData();
                drawSomething();
            }
        }

        private bool canDrawGrid(String text) 
        {
            if (text != null && !text.Equals("") && int.Parse(text)!=0)
                return true;
            else
            return false;
        
        }

    }
}
