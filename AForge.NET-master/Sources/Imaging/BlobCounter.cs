// AForge Image Processing Library
// AForge.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright ?AForge.NET, 2005-2012
// contacts@aforgenet.com
//

namespace AForge.Imaging
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// �ߵ����  �ߵ�����ǰѸ�����״�ӱ�����ɫ�з������
    /// 
    /// </summary>
    /// Blob counter - counts objects in image, which are separated by black background.
    /// �ߵ����  �ߵ�����ǰѸ�����״�ӱ�����ɫ�з������
    /// 
    /// The class counts and extracts stand alone objects in images using connected components labeling algorithm.
    /// ���ǰߵ����������  ��ͨ�������㷨
    /// 
    /// 
    /// ���㷨�ǽ�����С�ڻ���ڱ������صĵ㵱�������������ڱ���ɫ�ĵ�������㡣
    /// The algorithm treats all pixels with values less or equal to as background, but pixels with higher values are treated as objects' pixels.
    /// 
    /// 
    /// For blobs' searching the class supports 8 bpp indexed grayscale images and
    /// 24/32 bpp color images that are at least two pixels wide. Images that are one
    /// pixel wide can be processed if they are rotated first, or they can be processed
    /// with See documentation about  for information about which pixel formats are supported for extraction of blobs.
    ///
    public class BlobCounter : BlobCounterBase
    {
        /// <summary>
        /// ����R��ֵ
        /// </summary>
        private byte backgroundThresholdR = 0;
        /// <summary>
        /// ����G��ֵ
        /// </summary>
        private byte backgroundThresholdG = 0;
        /// <summary>
        /// ����B��ֵ
        /// </summary>
        private byte backgroundThresholdB = 0;

        /// <summary>
        /// ������ֵ
        /// Background threshold's value.
        /// </summary>
        /// 
        /// The property sets threshold value for distinguishing between background pixel and objects' pixels.
        /// ������ֵ��������Ϊ�����ֱ������غͶ�������
        /// 
        /// All pixel with values less or equal to this property are treated as background,
        /// but pixels with higher values are treated as objects' pixels.
        /// ��������С�ڻ���ڱ������صĵ������������������ظ��뱳�����صĵ������������
        /// 
        /// 
        /// In the case of colour images a pixel is treated as objects' pixel if anyof its
        /// RGB values are higher than corresponding values of this threshold.
        /// �ڲ�ɫͼ�������£�������κ����أ�������Ϊ��������أ� 
        /// RGBֵ���ڴ���ֵ����Ӧֵ��
        /// 
        /// For processing grayscale image, set the property with all RGB components eqaul.
        /// ����Ҷ�ͼƬʱ �������е�RGBֵ���
        ///
        /// Default value is set to <b>(0, 0, 0)</b> - black colour.
        /// Ĭ������RGB(0,0,0) ��ɫ����
        public Color BackgroundThreshold
        {
            get { return Color.FromArgb( backgroundThresholdR, backgroundThresholdG, backgroundThresholdB ); }
            set
            {
                backgroundThresholdR = value.R;
                backgroundThresholdG = value.G;
                backgroundThresholdB = value.B;
            }
        }

        /// <summary>
        /// ����һ�����ʵ��
        /// </summary>
        public BlobCounter( ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCounter"/> class.
        /// </summary>
        /// 
        /// <param name="image">Image to look for objects in.</param>
        /// 
        public BlobCounter( Bitmap image ) : base( image ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCounter"/> class.
        /// </summary>
        /// 
        /// <param name="imageData">Image data to look for objects in.</param>
        /// 
        public BlobCounter( BitmapData imageData ) : base( imageData ) { }

        /// <summary>
        /// ����һ�����ʵ�� ��ͼ���Ƿ��йܵ�
        /// </summary>
        public BlobCounter( UnmanagedImage image ) : base( image ) { }

        /// <summary>
        /// ʵ����ͼ����
        /// Actual objects map building.
        /// </summary>
        /// 
        /// Unmanaged image to process.
        /// ���й�ͼ����
        /// 
        /// The method supports 8 bpp indexed grayscale images and 24/32 bpp color images.
        /// �÷���֧��8 bpp�����Ҷ�ͼ���24/32 bpp��ɫͼ�� 
        protected override void BuildObjectsMap( UnmanagedImage image )
        {
            int stride = image.Stride;

            // check pixel format
            if ( ( image.PixelFormat != PixelFormat.Format8bppIndexed ) &&
                 ( image.PixelFormat != PixelFormat.Format24bppRgb ) &&
                 ( image.PixelFormat != PixelFormat.Format32bppRgb ) &&
                 ( image.PixelFormat != PixelFormat.Format32bppArgb ) &&
                 ( image.PixelFormat != PixelFormat.Format32bppPArgb ) )
            {
                throw new UnsupportedImageFormatException( "Unsupported pixel format of the source image." );
            }

            // we don't want one pixel width images
            if ( imageWidth == 1 )
            {
                throw new InvalidImagePropertiesException( "BlobCounter cannot process images that are one pixel wide. Rotate the image or use RecursiveBlobCounter." );
            }

            int imageWidthM1 = imageWidth - 1;

            // allocate labels array
            objectLabels = new int[imageWidth * imageHeight];
            // initial labels count
            int labelsCount = 0;

            // create map
            int maxObjects = ( ( imageWidth / 2 ) + 1 ) * ( ( imageHeight / 2 ) + 1 ) + 1;
            int[] map = new int[maxObjects];

            // initially map all labels to themself
            for ( int i = 0; i < maxObjects; i++ )
            {
                map[i] = i;
            }

            // do the job
            unsafe
            {
                byte* src = (byte*) image.ImageData.ToPointer( );
                int p = 0;

                if ( image.PixelFormat == PixelFormat.Format8bppIndexed )
                {
                    int offset = stride - imageWidth;

                    // 1 - for pixels of the first row
                    if ( *src > backgroundThresholdG )
                    {
                        objectLabels[p] = ++labelsCount;
                    }
                    ++src;
                    ++p;

                    // process the rest of the first row
                    for ( int x = 1; x < imageWidth; x++, src++, p++ )
                    {
                        // check if we need to label current pixel
                        if ( *src > backgroundThresholdG )
                        {
                            // check if the previous pixel already was labeled
                            if ( src[-1] > backgroundThresholdG )
                            {
                                // label current pixel, as the previous
                                objectLabels[p] = objectLabels[p - 1];
                            }
                            else
                            {
                                // create new label
                                objectLabels[p] = ++labelsCount;
                            }
                        }
                    }
                    src += offset;

                    // 2 - for other rows
                    // for each row
                    for ( int y = 1; y < imageHeight; y++ )
                    {
                        // for the first pixel of the row, we need to check
                        // only upper and upper-right pixels
                        if ( *src > backgroundThresholdG )
                        {
                            // check surrounding pixels
                            if ( src[-stride] > backgroundThresholdG )
                            {
                                // label current pixel, as the above
                                objectLabels[p] = objectLabels[p - imageWidth];
                            }
                            else if ( src[1 - stride] > backgroundThresholdG )
                            {
                                // label current pixel, as the above right
                                objectLabels[p] = objectLabels[p + 1 - imageWidth];
                            }
                            else
                            {
                                // create new label
                                objectLabels[p] = ++labelsCount;
                            }
                        }
                        ++src;
                        ++p;

                        // check left pixel and three upper pixels for the rest of pixels
                        for ( int x = 1; x < imageWidthM1; x++, src++, p++ )
                        {
                            if ( *src > backgroundThresholdG )
                            {
                                // check surrounding pixels
                                if ( src[-1] > backgroundThresholdG )
                                {
                                    // label current pixel, as the left
                                    objectLabels[p] = objectLabels[p - 1];
                                }
                                else if ( src[-1 - stride] > backgroundThresholdG )
                                {
                                    // label current pixel, as the above left
                                    objectLabels[p] = objectLabels[p - 1 - imageWidth];
                                }
                                else if ( src[-stride] > backgroundThresholdG )
                                {
                                    // label current pixel, as the above
                                    objectLabels[p] = objectLabels[p - imageWidth];
                                }

                                if ( src[1 - stride] > backgroundThresholdG )
                                {
                                    if ( objectLabels[p] == 0 )
                                    {
                                        // label current pixel, as the above right
                                        objectLabels[p] = objectLabels[p + 1 - imageWidth];
                                    }
                                    else
                                    {
                                        int l1 = objectLabels[p];
                                        int l2 = objectLabels[p + 1 - imageWidth];

                                        if ( ( l1 != l2 ) && ( map[l1] != map[l2] ) )
                                        {
                                            // merge
                                            if ( map[l1] == l1 )
                                            {
                                                // map left value to the right
                                                map[l1] = map[l2];
                                            }
                                            else if ( map[l2] == l2 )
                                            {
                                                // map right value to the left
                                                map[l2] = map[l1];
                                            }
                                            else
                                            {
                                                // both values already mapped
                                                map[map[l1]] = map[l2];
                                                map[l1] = map[l2];
                                            }

                                            // reindex
                                            for ( int i = 1; i <= labelsCount; i++ )
                                            {
                                                if ( map[i] != i )
                                                {
                                                    // reindex
                                                    int j = map[i];
                                                    while ( j != map[j] )
                                                    {
                                                        j = map[j];
                                                    }
                                                    map[i] = j;
                                                }
                                            }
                                        }
                                    }
                                }

                                // label the object if it is not yet
                                if ( objectLabels[p] == 0 )
                                {
                                    // create new label
                                    objectLabels[p] = ++labelsCount;
                                }
                            }
                        }

                        // for the last pixel of the row, we need to check
                        // only upper and upper-left pixels
                        if ( *src > backgroundThresholdG )
                        {
                            // check surrounding pixels
                            if ( src[-1] > backgroundThresholdG )
                            {
                                // label current pixel, as the left
                                objectLabels[p] = objectLabels[p - 1];
                            }
                            else if ( src[-1 - stride] > backgroundThresholdG )
                            {
                                // label current pixel, as the above left
                                objectLabels[p] = objectLabels[p - 1 - imageWidth];
                            }
                            else if ( src[-stride] > backgroundThresholdG )
                            {
                                // label current pixel, as the above
                                objectLabels[p] = objectLabels[p - imageWidth];
                            }
                            else
                            {
                                // create new label
                                objectLabels[p] = ++labelsCount;
                            }
                        }
                        ++src;
                        ++p;

                        src += offset;
                    }
                }
                else
                {
                    // color images
                    int pixelSize = Bitmap.GetPixelFormatSize( image.PixelFormat ) / 8;
                    int offset = stride - imageWidth * pixelSize;

                    int strideM1 = stride - pixelSize;
                    int strideP1 = stride + pixelSize;

                    // 1 - for pixels of the first row
                    if ( ( src[RGB.R] | src[RGB.G] | src[RGB.B] ) != 0 )
                    {
                        objectLabels[p] = ++labelsCount;
                    }
                    src += pixelSize;
                    ++p;

                    // process the rest of the first row
                    for ( int x = 1; x < imageWidth; x++, src += pixelSize, p++ )
                    {
                        // check if we need to label current pixel
                        if ( ( src[RGB.R] > backgroundThresholdR ) ||
                             ( src[RGB.G] > backgroundThresholdG ) ||
                             ( src[RGB.B] > backgroundThresholdB ) )
                        {
                            // check if the previous pixel already was labeled
                            if ( ( src[RGB.R - pixelSize] > backgroundThresholdR ) ||
                                 ( src[RGB.G - pixelSize] > backgroundThresholdG ) ||
                                 ( src[RGB.B - pixelSize] > backgroundThresholdB ) )
                            {
                                // label current pixel, as the previous
                                objectLabels[p] = objectLabels[p - 1];
                            }
                            else
                            {
                                // create new label
                                objectLabels[p] = ++labelsCount;
                            }
                        }
                    }
                    src += offset;

                    // 2 - for other rows
                    // for each row
                    for ( int y = 1; y < imageHeight; y++ )
                    {
                        // for the first pixel of the row, we need to check
                        // only upper and upper-right pixels
                        if ( ( src[RGB.R] > backgroundThresholdR ) ||
                             ( src[RGB.G] > backgroundThresholdG ) ||
                             ( src[RGB.B] > backgroundThresholdB ) )
                        {
                            // check surrounding pixels
                            if ( ( src[RGB.R - stride] > backgroundThresholdR ) ||
                                 ( src[RGB.G - stride] > backgroundThresholdG ) ||
                                 ( src[RGB.B - stride] > backgroundThresholdB ) )
                            {
                                // label current pixel, as the above
                                objectLabels[p] = objectLabels[p - imageWidth];
                            }
                            else if ( ( src[RGB.R - strideM1] > backgroundThresholdR ) ||
                                      ( src[RGB.G - strideM1] > backgroundThresholdG ) ||
                                      ( src[RGB.B - strideM1] > backgroundThresholdB ) )
                            {
                                // label current pixel, as the above right
                                objectLabels[p] = objectLabels[p + 1 - imageWidth];
                            }
                            else
                            {
                                // create new label
                                objectLabels[p] = ++labelsCount;
                            }
                        }
                        src += pixelSize;
                        ++p;

                        // check left pixel and three upper pixels for the rest of pixels
                        for ( int x = 1; x < imageWidth - 1; x++, src += pixelSize, p++ )
                        {
                            if ( ( src[RGB.R] > backgroundThresholdR ) ||
                                 ( src[RGB.G] > backgroundThresholdG ) ||
                                 ( src[RGB.B] > backgroundThresholdB ) )
                            {
                                // check surrounding pixels
                                if ( ( src[RGB.R - pixelSize] > backgroundThresholdR ) ||
                                     ( src[RGB.G - pixelSize] > backgroundThresholdG ) ||
                                     ( src[RGB.B - pixelSize] > backgroundThresholdB ) )
                                {
                                    // label current pixel, as the left
                                    objectLabels[p] = objectLabels[p - 1];
                                }
                                else if ( ( src[RGB.R - strideP1] > backgroundThresholdR ) ||
                                          ( src[RGB.G - strideP1] > backgroundThresholdG ) ||
                                          ( src[RGB.B - strideP1] > backgroundThresholdB ) )
                                {
                                    // label current pixel, as the above left
                                    objectLabels[p] = objectLabels[p - 1 - imageWidth];
                                }
                                else if ( ( src[RGB.R - stride] > backgroundThresholdR ) ||
                                          ( src[RGB.G - stride] > backgroundThresholdG ) ||
                                          ( src[RGB.B - stride] > backgroundThresholdB ) )
                                {
                                    // label current pixel, as the above
                                    objectLabels[p] = objectLabels[p - imageWidth];
                                }

                                if ( ( src[RGB.R - strideM1] > backgroundThresholdR ) ||
                                     ( src[RGB.G - strideM1] > backgroundThresholdG ) ||
                                     ( src[RGB.B - strideM1] > backgroundThresholdB ) )
                                {
                                    if ( objectLabels[p] == 0 )
                                    {
                                        // label current pixel, as the above right
                                        objectLabels[p] = objectLabels[p + 1 - imageWidth];
                                    }
                                    else
                                    {
                                        int l1 = objectLabels[p];
                                        int l2 = objectLabels[p + 1 - imageWidth];

                                        if ( ( l1 != l2 ) && ( map[l1] != map[l2] ) )
                                        {
                                            // merge
                                            if ( map[l1] == l1 )
                                            {
                                                // map left value to the right
                                                map[l1] = map[l2];
                                            }
                                            else if ( map[l2] == l2 )
                                            {
                                                // map right value to the left
                                                map[l2] = map[l1];
                                            }
                                            else
                                            {
                                                // both values already mapped
                                                map[map[l1]] = map[l2];
                                                map[l1] = map[l2];
                                            }

                                            // reindex
                                            for ( int i = 1; i <= labelsCount; i++ )
                                            {
                                                if ( map[i] != i )
                                                {
                                                    // reindex
                                                    int j = map[i];
                                                    while ( j != map[j] )
                                                    {
                                                        j = map[j];
                                                    }
                                                    map[i] = j;
                                                }
                                            }
                                        }
                                    }
                                }

                                // label the object if it is not yet
                                if ( objectLabels[p] == 0 )
                                {
                                    // create new label
                                    objectLabels[p] = ++labelsCount;
                                }
                            }
                        }

                        // for the last pixel of the row, we need to check
                        // only upper and upper-left pixels
                        if ( ( src[RGB.R] > backgroundThresholdR ) ||
                             ( src[RGB.G] > backgroundThresholdG ) ||
                             ( src[RGB.B] > backgroundThresholdB ) )
                        {
                            // check surrounding pixels
                            if ( ( src[RGB.R - pixelSize] > backgroundThresholdR ) ||
                                 ( src[RGB.G - pixelSize] > backgroundThresholdG ) ||
                                 ( src[RGB.B - pixelSize] > backgroundThresholdB ) )
                            {
                                // label current pixel, as the left
                                objectLabels[p] = objectLabels[p - 1];
                            }
                            else if ( ( src[RGB.R - strideP1] > backgroundThresholdR ) ||
                                      ( src[RGB.G - strideP1] > backgroundThresholdG ) ||
                                      ( src[RGB.B - strideP1] > backgroundThresholdB ) )
                            {
                                // label current pixel, as the above left
                                objectLabels[p] = objectLabels[p - 1 - imageWidth];
                            }
                            else if ( ( src[RGB.R - stride] > backgroundThresholdR ) ||
                                      ( src[RGB.G - stride] > backgroundThresholdG ) ||
                                      ( src[RGB.B - stride] > backgroundThresholdB ) )
                            {
                                // label current pixel, as the above
                                objectLabels[p] = objectLabels[p - imageWidth];
                            }
                            else
                            {
                                // create new label
                                objectLabels[p] = ++labelsCount;
                            }
                        }
                        src += pixelSize;
                        ++p;

                        src += offset;
                    }
                }
            }

            // allocate remapping array
            int[] reMap = new int[map.Length];

            // count objects and prepare remapping array
            objectsCount = 0;
            for ( int i = 1; i <= labelsCount; i++ )
            {
                if ( map[i] == i )
                {
                    // increase objects count
                    reMap[i] = ++objectsCount;
                }
            }
            // second pass to complete remapping
            for ( int i = 1; i <= labelsCount; i++ )
            {
                if ( map[i] != i )
                {
                    reMap[i] = reMap[map[i]];
                }
            }

            // repair object labels
            for ( int i = 0, n = objectLabels.Length; i < n; i++ )
            {
                objectLabels[i] = reMap[objectLabels[i]];
            }
        }
    }
}
