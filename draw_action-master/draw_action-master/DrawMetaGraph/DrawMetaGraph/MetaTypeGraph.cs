using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawMetaGraph
{
    class MetaTypeGraph
    {
        public Color fillColor;
        public bool isSelected = false;
        public int graphicLevel ;
        public Point upLeftCoordinate;
        public Color choosedColor = Color.White;

        public virtual void draw()
        {
        }
        public virtual void MouseDown(MouseEventArgs e)
        {
        }
    }
}
