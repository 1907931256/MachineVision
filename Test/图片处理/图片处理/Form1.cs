using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using ZXing.Common;

using AForge;
using AForge.Imaging.Filters;
using ZXing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Math.Geometry;

namespace 图片处理
{
    public partial class Form1 : Form
    {
        Bitmap sourceImage = null;
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            fileDialog.Filter = "图形文件(*.jpg)|*.jpg";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                //this.ImgPathTxt.Text = filePath;
                //this.pictureBox1.Image = Bitmap.FromFile(filePath);
                 filePath = fileDialog.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pictureBox1.Image = Bitmap.FromFile(filePath);

                sourceImage = Bitmap.FromFile(filePath) as Bitmap;



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //// apply filter
            //sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);

            //ApplyFilter(new Threshold());

            //sourceImage.Dispose();
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            ProcessImage(sourceImage);


            sourceImage.Save("123456789.jpg");
            pictureBox2.Image = sourceImage as Bitmap;
            //Test_1();
        }

        private void ApplyFilter(IFilter filter)
        {
            pictureBox2.Image = null;

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            // apply filter
            Bitmap filteredImage = filter.Apply(sourceImage);
            // display filtered image
            pictureBox2.Image = filteredImage;
            pictureBox2.Image.Save(@"e:\BTWork\MachineL\machine_vision\Test\图片处理\12.jpg");
        }

        public void Test_1()
        {
            // 1.设置读取条形码规格
            DecodingOptions decodeOption = new DecodingOptions();
            decodeOption.PossibleFormats = new List<BarcodeFormat>() {
               BarcodeFormat.All_1D,

            };



            // 2.进行读取操作
            ZXing.BarcodeReader br = new BarcodeReader();
            br.Options = decodeOption;
            this.pictureBox2.Image = Bitmap.FromFile(@"e:\BTWork\MachineL\machine_vision\Test\图片处理\123.jpg");
            ZXing.Result rs = br.Decode(this.pictureBox2.Image as Bitmap);
            
            if (rs == null)
            {
                MessageBox.Show("读取失败");
            }
            else
            {
                MessageBox.Show("读取成功，内容：" + rs.Text);
            }
        }

        /// <summary>
        /// 图片处理过程
        /// </summary>
        /// <param name="bitmap"></param>
        private /*Bitmap*/ void ProcessImage(Bitmap bitmap)
        {
            // lock image
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // step 1 - turn background to black
            //ColorFiltering colorFilter = new ColorFiltering();

            //colorFilter.Red = new IntRange(0, 64);
            //colorFilter.Green = new IntRange(0, 64);
            //colorFilter.Blue = new IntRange(0, 64);
            //colorFilter.FillOutsideRange = false;

            //colorFilter.ApplyInPlace(bitmapData);

            // step 2 - locating objects
            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = 5;
            blobCounter.MinWidth = 5;

            blobCounter.ProcessImage(bitmapData);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            bitmap.UnlockBits(bitmapData);

            // step 3 - check objects' type and highlight
            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
            //Bitmap test = bitmap.Save("12345.jpg", ImageFormat.Gif);


            
            Graphics g = Graphics.FromImage(CheckPixPicture(bitmap));
            //Pen yellowPen = new Pen(Color.Yellow, 2); // circles
            Pen redPen = new Pen(Color.Red, 2);       // quadrilateral
            //Pen brownPen = new Pen(Color.Brown, 2);   // quadrilateral with known sub-type
            //Pen greenPen = new Pen(Color.Green, 2);   // known triangle
            //Pen bluePen = new Pen(Color.Blue, 2);     // triangle

            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                AForge.Point center;
                float radius;

                // is circle ?
                //if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                //{
                //    //g.DrawEllipse(yellowPen,
                //    //    (float)(center.X - radius), (float)(center.Y - radius),
                //    //    (float)(radius * 2), (float)(radius * 2));
                //}
                //else
                //{
                    List<IntPoint> corners;

                    // is triangle or quadrilateral
                    if (shapeChecker.IsConvexPolygon(edgePoints, out corners))
                    {
                        // get sub-type
                        PolygonSubType subType = shapeChecker.CheckPolygonSubType(corners);

                        Pen pen;

                        //if (subtype == polygonsubtype.unknown)
                        //{
                        //    pen = (corners.count == 4) ? redpen : bluepen;
                        //}
                        //else
                        //{
                        //    pen = (corners.count == 4) ? brownpen : greenpen;
                        //}

                        g.DrawPolygon(redPen, ToPointsArray(corners));
                    }
                //}
            }

            //yellowPen.Dispose();
            redPen.Dispose();
            //greenPen.Dispose();
            //bluePen.Dispose();
            //brownPen.Dispose();
            g.Dispose();

            // put new image to clipboard
            Clipboard.SetDataObject(bitmap);

            //return bitmap;
        }
        // Conver list of AForge.NET's points to array of .NET points
        private System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        {
            System.Drawing.Point[] array = new System.Drawing.Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
            }

            return array;
        }



        /// <summary>
        /// 用 .net做图片开发时  很可能会遇到 “无法从带有索引像素格式的图像创建graphics对象”这个错误
        /// 调用下面的Method可以解决这个问题
        /// </summary>
        /// <param name="PathString"></param>
        private Bitmap CheckPixPicture(Bitmap bitmap)
        {
            using (System.Drawing.Image img = bitmap as System.Drawing.Image)
            {
                //如果原图片是索引像素格式之列的，则需要转换  
                if (indexedPixelFormats.Contains(img.PixelFormat))
                {
                    Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.DrawImage(img, 0, 0);
                    }
                    return bmp;
                }
                else //否则直接操作  
                {
                    return img as Bitmap;
                }
            }
        }


        /// <summary> 
        /// 带索引的,会产生graphics异常的PixelFormat  
        /// </summary>  
        private static readonly HashSet<PixelFormat> indexedPixelFormats = new HashSet<PixelFormat>
        {
        PixelFormat.Undefined,PixelFormat.DontCare,
        PixelFormat.Format16bppArgb1555,PixelFormat.Format1bppIndexed,
        PixelFormat.Format4bppIndexed,PixelFormat.Format8bppIndexed
        };


    }
}
