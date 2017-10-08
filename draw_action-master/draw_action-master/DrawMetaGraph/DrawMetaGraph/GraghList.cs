using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DrawMetaGraph
{
    class GraghList
    {

        private Graphics graphics;
        private Color bgColor;
        private List<MetaTypeGraph> metaGraphList;
       
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="graphics">画布</param>
        public GraghList(Graphics graphics)
        {
            this.graphics = graphics;
            this.bgColor = Color.Green;  //默认的填充色
            metaGraphList = new List<MetaTypeGraph>();
           
        }
        /// <summary>
        /// 绘制所有的图形
        /// </summary>
        public void draw()
        {
            foreach (MetaTypeGraph metaGraph in metaGraphList)
            {
                metaGraph.draw();
            }
        }

        void clearSelected()
        {
            foreach (MetaTypeGraph graph in metaGraphList)
            {
                graph.isSelected = false;
            }
        }

        /// <summary>
        /// 鼠标按键处理
        /// </summary>
        /// <param name="e">鼠标参数</param>
        public void mouseDown(MouseEventArgs e)
        {
            clearSelected();
            int maxGraphicsLevel = metaGraphList.Count - 1;
            for (int currentLevel = maxGraphicsLevel; currentLevel >= 0; currentLevel--)
            {
                bool hasChanged = false;
                for (int i = 0; i < metaGraphList.Count; i++)
                {
                    MetaTypeGraph metaGraph = metaGraphList[i];

                    if (metaGraph.graphicLevel == currentLevel)
                    {
                        metaGraph.MouseDown(e);
                        if (metaGraph.isSelected == true)
                        {
                            hasChanged = true;
                            break;
                        }

                    }
                    
                }
                if (hasChanged == true) break;
            }
            draw();
        }

        //清除背景色
        public void clearBgColor() 
        {
            graphics.Clear(bgColor);
        }

        
        public Graphics Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }
        public Color BgColor
        {
            get { return bgColor; }
            set { bgColor = value; }
        }

        public List<MetaTypeGraph> MetaGraphList
        {
            get { return metaGraphList; }
            set { metaGraphList = value; }
        }
    }
}
