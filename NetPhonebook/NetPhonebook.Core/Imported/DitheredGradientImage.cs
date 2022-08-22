using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace NetPhonebook.Core.Imported
{
    /// <summary>
    /// A class that draws a pixel-perfect, dithered gradient image
    /// </summary>
    public class DitheredGradientImage : FrameworkElement
    {
        public static readonly DependencyProperty TopLeftColorProperty =
            DependencyProperty.Register(nameof(TopLeftColor), typeof(Color), typeof(DitheredGradientImage),
        new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender, OnColorsChanged));

        public static readonly DependencyProperty TopRightColorProperty =
            DependencyProperty.Register(nameof(TopRightColor), typeof(Color), typeof(DitheredGradientImage),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender, OnColorsChanged));

        public static readonly DependencyProperty BottomLeftColorProperty =
            DependencyProperty.Register(nameof(BottomLeftColor), typeof(Color), typeof(DitheredGradientImage),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender, OnColorsChanged));

        public static readonly DependencyProperty BottomRightColorProperty =
            DependencyProperty.Register(nameof(BottomRightColor), typeof(Color), typeof(DitheredGradientImage),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender, OnColorsChanged));

        public Color TopLeftColor
        {
            get { return (Color)this.GetValue(TopLeftColorProperty); }
            set { this.SetValue(TopLeftColorProperty, value); }
        }

        public Color TopRightColor
        {
            get { return (Color)this.GetValue(TopRightColorProperty); }
            set { this.SetValue(TopRightColorProperty, value); }
        }

        public Color BottomLeftColor
        {
            get { return (Color)this.GetValue(BottomLeftColorProperty); }
            set { this.SetValue(BottomLeftColorProperty, value); }
        }

        public Color BottomRightColor
        {
            get { return (Color)this.GetValue(BottomRightColorProperty); }
            set { this.SetValue(BottomRightColorProperty, value); }
        }

        public Color TopColor
        {
            get { return (TopLeftColor == TopRightColor) ? TopLeftColor : default(Color); }
            set { TopLeftColor = value; TopRightColor = value; }
        }

        public Color BottomColor
        {
            get { return (BottomLeftColor == BottomRightColor) ? BottomLeftColor : default(Color); }
            set { BottomLeftColor = value; BottomRightColor = value; }
        }

        public Color LeftColor
        {
            get { return (TopLeftColor == BottomLeftColor) ? TopLeftColor : default(Color); }
            set { TopLeftColor = value; BottomLeftColor = value; }
        }

        public Color RightColor
        {
            get { return (TopRightColor == BottomRightColor) ? TopRightColor : default(Color); }
            set { TopRightColor = value; BottomRightColor = value; }
        }

        public double DpiScaleX => VisualTreeHelper.GetDpi(this).DpiScaleX;
        public double DpiScaleY => VisualTreeHelper.GetDpi(this).DpiScaleY;

        public double DpiX => VisualTreeHelper.GetDpi(this).PixelsPerInchX;
        public double DpiY => VisualTreeHelper.GetDpi(this).PixelsPerInchX;

        private WriteableBitmap bitmap = null;
        private int lastPixelWidth = 0;
        private int lastPixelHeight = 0;

        protected static void OnColorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Force redraw by resetting width and height
            if (d is DitheredGradientImage di)
            {
                di.lastPixelWidth = 0;
                di.lastPixelHeight = 0;
            }
        }

        protected void Update(int pixelWidth, int pixelHeight)
        {
            // Check if bitmap needs recreation, because it it is either
            // not initialized, or too small to fit the current element size
            if ((bitmap == null) ||
                (bitmap.PixelWidth < pixelWidth) ||
                (bitmap.PixelHeight < pixelHeight))
            {
                // Oversize, so that continuous resizing doesn't lead to constant bitmap recreation
                bitmap = new WriteableBitmap(pixelWidth + 256, pixelHeight + 256, DpiX, DpiY, PixelFormats.Bgra32, null);

                // As the old bitmap is gone, reset values so it gets redrawn anyway
                lastPixelWidth = 0;
                lastPixelHeight = 0;
            }

            // The gradient needs to be drawn to the exact size required
            if ((lastPixelWidth != pixelWidth) || (lastPixelHeight != pixelHeight))
            {
                // Draw a line-based test pattern to verify proper pixel scaling (or the lack of)
                //DrawTestPattern(pixelWidth, pixelHeight);

                // Draw the actual gradient
                DrawGradient(pixelWidth, pixelHeight);

                // Store values to avoid redrawing when nothing has changed
                lastPixelWidth = pixelWidth;
                lastPixelHeight = pixelHeight;
            }
        }

        protected void DrawTestPattern(int pixelWidth, int pixelHeight)
        {
            try
            {
                // Reserve the back buffer for updates.
                bitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = bitmap.BackBuffer;

                    for (int y = 0; y < pixelHeight; y++)
                    {
                        for (int x = 0; x < pixelWidth; x++)
                        {
                            // Draw alternate black and white lines
                            uint colorData = (y % 2 == 0) ? 0xFF000000 : 0xFFFFFFFF;
                            *(uint*)(pBackBuffer + (y * bitmap.BackBufferStride) + (x * 4)) = colorData;
                        }
                    }
                }

                // Specify the area of the bitmap that changed.
                bitmap.AddDirtyRect(new Int32Rect(0, 0, pixelWidth, pixelHeight));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                bitmap.Unlock();
            }
        }

        private const byte matrixSize = 4;

        private static readonly float[,] scaledMatrix = CalculateMatrix(matrixSize);

        private static float[,] CalculateMatrix(byte n)
        {
            uint[,] uintMatrix = new uint[n, n];
            uint divisor = uint.MaxValue;
            for (ushort i = 0; i < n; i++)
            {
                for (ushort j = 0; j < n; j++)
                {
                    uintMatrix[i, j] = BitReverse(8, BitInterleave((ushort)(i ^ j), i));
                    if ((uintMatrix[i, j] != 0) && (divisor > uintMatrix[i, j]))
                        divisor = uintMatrix[i, j];
                }
            }

            float[,] floatMatrix = new float[n, n];
            float coefficient = 1.0f / (uintMatrix.Length * divisor);
            for (ushort i = 0; i < n; i++)
            {
                for (ushort j = 0; j < n; j++)
                {
                    floatMatrix[i, j] = uintMatrix[i, j] * coefficient;
                }
            }

            return floatMatrix;
        }

        /// <summary>
        /// Reverse all bits
        /// </summary>
        private static uint BitReverse(byte bits, uint value)
        {
            uint left = (uint)1 << (bits - 1);
            uint right = 1;
            uint result = 0;

            for (int i = (bits - 1); i >= 1; i -= 2)
            {
                result |= (value & left) >> i;
                result |= (value & right) << i;
                left >>= 1;
                right <<= 1;
            }
            return result;
        }

        /// <summary>
        /// Bitwise interleave of two 16-bit unsigned integers
        /// </summary>
        private static uint BitInterleave(ushort x, ushort y)
        {
            uint _x = x;
            _x = (_x | (_x << 8)) & 0x00FF00FF;
            _x = (_x | (_x << 4)) & 0x0F0F0F0F;
            _x = (_x | (_x << 2)) & 0x33333333;
            _x = (_x | (_x << 1)) & 0x55555555;

            uint _y = y;
            _y = (_y | (_y << 8)) & 0x00FF00FF;
            _y = (_y | (_y << 4)) & 0x0F0F0F0F;
            _y = (_y | (_y << 2)) & 0x33333333;
            _y = (_y | (_y << 1)) & 0x55555555;

            return (uint)(_x | (_y << 1));
        }

        /// <summary>
        /// Draws the gradient
        /// </summary>
        protected void DrawGradient(int pixelWidth, int pixelHeight)
        {
            // Caching the color values is mandatory, JIT compiler doesn't seem to cache access
            byte topLeftR = TopLeftColor.R, topLeftG = TopLeftColor.G, topLeftB = TopLeftColor.B, topLeftA = TopLeftColor.A;
            byte topRightR = TopRightColor.R, topRightG = TopRightColor.G, topRightB = TopRightColor.B, topRightA = TopRightColor.A;
            byte bottomLeftR = BottomLeftColor.R, bottomLeftG = BottomLeftColor.G, bottomLeftB = BottomLeftColor.B, bottomLeftA = BottomLeftColor.A;
            byte bottomRightR = BottomRightColor.R, bottomRightG = BottomRightColor.G, bottomRightB = BottomRightColor.B, bottomRightA = BottomRightColor.A;

            try
            {
                // Reserve the back buffer for updates.
                bitmap.Lock();

                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = bitmap.BackBuffer;
                    int backBufferStride = bitmap.BackBufferStride;

                    for (int y = 0; y < pixelHeight; y++)
                    {
                        float incYScale = y / (float)pixelHeight;
                        float decYScale = 1 - (y / (float)pixelHeight);

                        for (int x = 0; x < pixelWidth; x++)
                        {
                            float incXScale = x / (float)pixelWidth;
                            float decXScale = 1 - (x / (float)pixelWidth);

                            float r = (((topLeftR * decXScale) + (topRightR * incXScale)) * decYScale) +
                                        (((bottomLeftR * decXScale) + (bottomRightR * incXScale)) * incYScale);
                            float g = (((topLeftG * decXScale) + (topRightG * incXScale)) * decYScale) +
                                        (((bottomLeftG * decXScale) + (bottomRightG * incXScale)) * incYScale);
                            float b = (((topLeftB * decXScale) + (topRightB * incXScale)) * decYScale) +
                                        (((bottomLeftB * decXScale) + (bottomRightB * incXScale)) * incYScale);
                            float a = (((topLeftA * decXScale) + (topRightA * incXScale)) * decYScale) +
                                        (((bottomLeftA * decXScale) + (bottomRightA * incXScale)) * incYScale);

                            byte* p = (byte*)(pBackBuffer + (y * backBufferStride) + (x * 4));

                            p[0] = (byte)(b + scaledMatrix[x % matrixSize, y % matrixSize]);
                            p[1] = (byte)(g + scaledMatrix[x % matrixSize, y % matrixSize]);
                            p[2] = (byte)(r + scaledMatrix[x % matrixSize, y % matrixSize]);
                            p[3] = (byte)a;
                        }
                    }

                    // Specify the area of the bitmap that changed.
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, pixelWidth, pixelHeight));
                }
            }
            finally
            {
                bitmap.Unlock();
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            // Math.Round seems to be used in WPF, so it's important to use the 
            // same function, otherwise you sometimes don't get pixel-perfect drawing
            int pixelWidth = (int)Math.Round(RenderSize.Width * DpiScaleX);
            int pixelHeight = (int)Math.Round(RenderSize.Height * DpiScaleY);

            // Exit if there is nothing to draw
            if ((pixelWidth == 0) || (pixelHeight == 0)) return;

            // Refresh the bitmap
            Update(pixelWidth, pixelHeight);

            if (bitmap != null)
            {
                // The bitmap will be oversized, and DrawingContext.DrawImage doesn't provide
                // any other way to specify a source image rect
                CroppedBitmap croppedBitmap = new CroppedBitmap(bitmap, new Int32Rect(0, 0, pixelWidth, pixelHeight));
                dc.DrawImage(croppedBitmap, new Rect(default(Point), RenderSize));
            }
        }
    }
}