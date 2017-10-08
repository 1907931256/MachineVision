using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 集合运算
{
    public partial class Form1 : Form
    {
        private string curFileName;
        private System.Drawing.Bitmap curBitmap;
        public Form1()
        {
            InitializeComponent();
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "所有图像文件| *.bmp;*.pcx; *.png; *.jpg; *.gif;" +
                "*.tif; *.ico; *.dxf;*.cgm;*.cdr;*.wmf;*.eps;*.emf|" +
                "位图（*.bmp; *.jpg;*.png;...）|*.bmp;*.pcx;*.png;*.jpg;";

            opnDlg.Title = "打开图像文件";
            opnDlg.ShowHelp = true;
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                curFileName = opnDlg.FileName;
                curBitmap = (Bitmap)Image.FromFile(curFileName);

            }
            Invalidate();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void translation_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                translation traForm = new translation();

                if (traForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    int x = Convert.ToInt32(traForm.GetXOffset);
                    int y = Convert.ToInt32(traForm.GetYOffset);

                    byte[] tempArray = new byte[bytes];
                    for (int i = 0; i < curBitmap.Height; i++)
                    {
                        if((i + y) < curBitmap.Height && (i + y) > 0) 
                        {
                            for(int j = 0; j < curBitmap.Width; j++)
                        {
                            if((j + x) < curBitmap.Width && (j + x) > 0)
                            {
                                tempArray[(j + x) + (i + y) * curBitmap.Width] =
                                    grayValues[j + i * curBitmap.Width];
;

                            }
                        }
                        }
                    }
                    grayValues = (byte[])tempArray.Clone()
;
                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }
                Invalidate();
            }
          
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (curBitmap != null)
            {
                g.DrawImage(curBitmap, 160, 20, 600, 500);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mirror_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                mirror mirForm = new mirror();

                if (mirForm.ShowDialog() == DialogResult.OK)
                {
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                    IntPtr ptr = bmpData.Scan0;
                    int bytes = curBitmap.Width * curBitmap.Height;
                    byte[] grayValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    int halfWidth = curBitmap.Width / 2;
                    int halfHeight = curBitmap.Height / 2;
                    byte temp;

                    if (mirForm.GetMirror)
                    {

                        for (int i = 0; i < curBitmap.Height; i++)
                        {
                            for (int j = 0; j < halfWidth; j++)
                            {
                                temp = grayValues[i * curBitmap.Width + j];
                                grayValues[i * curBitmap.Width + j] =
                                    grayValues[(i + 1) * curBitmap.Width - 1 - j];
                                grayValues[(i + 1) * curBitmap.Width - 1 - j] = temp;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < curBitmap.Width; i++)
                        {
                            for (int j = 0; j < halfHeight; j++)
                            {
                                temp = grayValues[j * curBitmap.Width + i];
                                grayValues[j * curBitmap.Width + i] =
                                    grayValues[(curBitmap.Height - j - 1) * curBitmap.Width + i];
                                grayValues[(curBitmap.Height - j - 1) * curBitmap.Width + i] = temp;
                            }
                        }
                    }


                    System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                    curBitmap.UnlockBits(bmpData);
                }
                Invalidate();

            }
        }


    }
}
