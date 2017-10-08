using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawMetaGraph
{
    public partial class Form1 : Form
    {

        private GraghList  graghlist;
        private MetaTypeGraph metaGragh;
        private Color fillColor ;
        private Color bgColor;
        private bool isDrawing;
        private List<MetaTypeGraph> metaList;

        public Form1()
        {
            InitializeComponent();
            graghlist = new GraghList(this.panel2.CreateGraphics());
            isDrawing = false;
            metaList = new List<MetaTypeGraph>();
         
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            graghlist.clearBgColor();
            DialogResult dr = MessageBox.Show("点击开始绘画按钮后，可以绘制不同形状，不同颜色的图元。当点击结束绘画按钮后，可以选中已绘制的图元，被选中的图元将以白色填充。", "图元绘制使用方法", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (isDrawing) {
                getMetaTypeGraph();
                metaGragh.upLeftCoordinate = new Point(e.X, e.Y);
                metaGragh.fillColor = fillColor;
                metaGragh.graphicLevel = metaList.Count;
                metaList.Add(metaGragh);
                metaGragh.draw();
               
            }
            graghlist.MetaGraphList = metaList;
            if (!isDrawing)
                 graghlist.mouseDown(e);
        }



        

        private void comboBox3_textChanged(object sender, EventArgs e)
        {
            getBgcolor();
            graghlist.BgColor = bgColor;
            graghlist.clearBgColor();      
            graghlist.draw();
                

        }

        private void button1_MouseClick(object sender, EventArgs e)
        {
            isDrawing = true;
            getMetaTypeGraph();
            getFillColor();
            getBgcolor();
            
        }

        private void getBgcolor()
        {
            String bc = comboBox3.Text;
            if (bc.Equals("红色"))
                bgColor = Color.Red;
            if (bc.Equals("黄色"))
                bgColor = Color.Yellow;
            if (bc.Equals("蓝色"))
                bgColor = Color.Blue;
            if (bc.Equals("绿色"))
                bgColor = Color.Green;
            if (bc.Equals("灰色"))
                bgColor = Color.Gray;
        }

        private void getFillColor()
        {
            String bc = comboBox2.Text;
            if (bc.Equals("红色"))
                fillColor = Color.Red;
            if (bc.Equals("黄色"))
                fillColor = Color.Yellow;
            if (bc.Equals("蓝色"))
                fillColor = Color.Blue;
            if (bc.Equals("绿色"))
                fillColor = Color.Green;
            if (bc.Equals("灰色"))
                fillColor = Color.Gray;
            if (metaGragh!=null)
                metaGragh.fillColor = fillColor;
        }

        private void getMetaTypeGraph()
        {
            String metaType = comboBox1.Text;
            Point p = new Point(50, 50);
            if (metaType.Equals("正方形"))
            {
                metaGragh = new SelectBox(this.panel2.CreateGraphics(), p);

            }
            if (metaType.Equals("三角形"))
            {
           
                metaGragh = new Triangle(this.panel2.CreateGraphics(), p);

            }
            if (metaType.Equals("圆形"))
            {
                metaGragh = new Circle(this.panel2.CreateGraphics(), p);

            }
        }

        private void comboBox1_textChanged(object sender, EventArgs e)
        {
            getMetaTypeGraph();
            
        }

        private void comboBox2_textChanged(object sender, EventArgs e)
        {
            getFillColor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isDrawing = false;
        }

        private void button3_click(object sender, EventArgs e)
        {
            graghlist.clearBgColor();
            graghlist.MetaGraphList.Clear();

        }

       
    }
}
