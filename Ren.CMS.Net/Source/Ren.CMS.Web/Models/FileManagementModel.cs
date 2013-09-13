namespace Ren.CMS.Models
{
    using System;
    using System.Drawing;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Caching;

    public static class HttpResponseExtension
    {
        #region Methods

        public static void SetDefaultImageHeaders(this HttpResponseBase response)
        {
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetExpires(Cache.NoAbsoluteExpiration);
            response.Cache.SetLastModifiedFromFileDependencies();
        }

        #endregion Methods
    }

    public class WaterMark
    {
        #region Fields

        private string loc = "UPPER_RIGHT";
        private double perc = 0.3;
        private int _margin = 10;
        private float _opacity = 0.9F;

        #endregion Fields

        #region Methods

        public MemoryStream AddWaterMark(Image Img, Image Waterm)
        {
            return this.resizeWaterMark(Img, Waterm);
        }

        public float getOpacity()
        {
            return this._opacity;
        }

        public void SetMargin(int margin = 10)
        {
            this._margin = margin;
        }

        public void SetOpacity(float opacity = 0.9f)
        {
            this._opacity = opacity;
        }

        public void SetWaterMarkLocation(string LOCATION = "UPPER_RIGHT")
        {
            this.loc = LOCATION;
        }

        public void SetWaterMarkPercentageSize(double percentage = 0.3)
        {
            this.perc = percentage;
        }

        private MemoryStream resizeWaterMark(Image Img, Image Waterm)
        {
            Bitmap BMPtemp = new System.Drawing.Bitmap(Img.Width, Img.Height);

            BMPtemp.SetResolution(Img.HorizontalResolution, Img.VerticalResolution);
            Graphics GFX = Graphics.FromImage(BMPtemp);
            //First draw the img:
            GFX.DrawImage(Img,
                 new Rectangle(0, 0, Img.Width, Img.Height),
                 0,
                 0,
                 Img.Width,
                 Img.Height,
                 GraphicsUnit.Pixel);

            ImageAttributes imageAttributes =
                   new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable,
                                 ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {
               new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
               new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
               new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
               new float[] {0.0f,  0.0f,  0.0f,  this._opacity, 0.0f},
               new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
            };

            ColorMatrix wmColorMatrix = new
                        ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix,
                               ColorMatrixFlag.Default,
                                 ColorAdjustType.Bitmap);
            double sumIMG = (Img.Width + Img.Height);
            double sumBITM = (Waterm.Width + Waterm.Height);
            double pc = this.perc;
            double nowW = Waterm.Width;
            double nowH = Waterm.Height;
            double WM2IMG = (sumBITM) / sumIMG;
            if (WM2IMG > pc)
            {

            //Get Perfect Size

            while (WM2IMG > pc)
            {

                nowW = nowW - (nowW * 0.1);
                nowH = nowH - (nowH * 0.1);

                double s = nowH + nowW;

                WM2IMG = s / sumIMG;

            }

            }
            int w = Convert.ToInt32(nowW);
            int h = Convert.ToInt32(nowH);

            //Right Bottom Default
            int x = Img.Width - w - this._margin;

            int y = Img.Height - h - this._margin;
            int middleW = Img.Width / 2;
            int middleH = Img.Height / 2;

            switch (this.loc.ToUpper()) {

                case "LEFT_BOTTOM":
            //Left Bottom
                x = this._margin;
            break;
            //Left Upper
                case "LEFT_UPPER":
            x = this._margin;

            y = this._margin;
            break;
            //Right Upper
             case "RIGHT_UPPER":

            y = this._margin;

            x = Img.Width - w - this._margin;
            break;
            //Upper Center.
            case "UPPER_CENTER":

            y = this._margin;
            x = middleW - (w/2);
            break;
            //Center Center
            case "CENTER":
            x = middleW - (w / 2);

            y = middleH - (h / 2);
            break;

            case "LOWER_CENTER":
            //Lower Center
            y = Img.Height - h - this._margin;
            x = middleW - (w / 2);
            break;
            }

            Point PT = new System.Drawing.Point(x, y);
            int xPosOfWm = ((Img.Width - 200) - 10);
            int yPosOfWm = 10;
            GFX.DrawImage(Waterm,
                new Rectangle(x, y, w,
                                                  h),
                0,
                0,
                 Waterm.Width,
                Waterm.Height,
                GraphicsUnit.Pixel,
                imageAttributes);

            GFX.Save();

            MemoryStream MS = new MemoryStream();

            Img = BMPtemp;

            Img.Save(MS, ImageFormat.Jpeg);

            Waterm.Dispose();
            Img.Dispose();
            BMPtemp.Dispose();
            return MS;
        }

        #endregion Methods
    }

    public class WaterMarkText
    {
        #region Fields

        private bool findperfectsize = true;
        private string FontName = "arial";
        private int fontSize = 10; //Use Default Font Size
        private string loc = "RIGHT_UPPER";
        private string text = "";
        private int _blue = 0;
        private int _green = 0;
        private int _margin = 10;
        private double _opacity = 90;
        private int _red = 0;

        #endregion Fields

        #region Methods

        public MemoryStream AddWaterMarkText(Image SourceImage)
        {
            int imgWidth = SourceImage.Width;
            int imgHeight = SourceImage.Height;

            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            Graphics GFX = Graphics.FromImage(bmp);
            GFX.SmoothingMode = SmoothingMode.AntiAlias;
            GFX.DrawImage(
                 SourceImage,
                 new Rectangle(0, 0, imgWidth, imgHeight),
                 0,
                 0,
                 imgWidth,
                 imgHeight,
                 GraphicsUnit.Pixel);

            int startsize = this.fontSize;
            Font crFont = null;
            SizeF crSize = new SizeF();

            if (this.findperfectsize)
            {

                int[] sizes = new int[] { 16, 14, 12, 10, 8 };

                for (int i = 0; i < sizes.Length; i++)
                {
                    crFont = new Font(this.FontName, sizes[i],
                                                 FontStyle.Bold);
                    crSize = GFX.MeasureString(this.text,
                                                         crFont);

                    if ((ushort)crSize.Width < (ushort)imgWidth * 0.10)
                        break;
                }

            }
            else {

                crFont = new Font(this.FontName, this.fontSize,
                                                     FontStyle.Bold);

            }

              //Font Size found....
            Font F = crFont;

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;
            int op = Convert.ToInt32(Math.Round(255 * (this._opacity / 100),0));

            SolidBrush semiTransBrush2 =
            new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Right Bottom Default
            int x = imgWidth - GFX.MeasureString(this.text,crFont).ToSize().Width - this._margin;

            int y = imgHeight - GFX.MeasureString(this.text, crFont).ToSize().Height - this._margin;
            int middleW = imgWidth / 2;
            int middleH = imgHeight / 2;

            switch (this.loc.ToUpper())
            {

                case "LEFT_BOTTOM":
                    //Left Bottom
                    x = this._margin;
                    StrFormat.Alignment = StringAlignment.Near;
                    break;
                //Left Upper
                case "LEFT_UPPER":
                    x = this._margin;
                    StrFormat.Alignment = StringAlignment.Near;
                    y = this._margin;
                    break;
                //Right Upper
                case "RIGHT_UPPER":

                    y = this._margin;
                    StrFormat.Alignment = StringAlignment.Near;
                    x = imgWidth - GFX.MeasureString(this.text, F).ToSize().Width - this._margin;
                    break;
                //Upper Center.
                case "UPPER_CENTER":
                    StrFormat.Alignment = StringAlignment.Center;
                    y = this._margin;
                    x = middleW - (GFX.MeasureString(this.text, F).ToSize().Width / 2);
                    break;
                //Center Center
                case "CENTER":
                    x = middleW - (GFX.MeasureString(this.text, F).ToSize().Width / 2);
                    StrFormat.Alignment = StringAlignment.Center;
                    y = middleH - (GFX.MeasureString(this.text, F).ToSize().Height / 2);
                    break;

                case "LOWER_CENTER":
                    //Lower Center
                    StrFormat.Alignment = StringAlignment.Center;
                    y = imgHeight - GFX.MeasureString(this.text, F).ToSize().Height - this._margin;
                    x = middleW - (GFX.MeasureString(this.text, F).ToSize().Width / 2);
                    break;
            }
            SolidBrush semiTransBrush = new SolidBrush(
                    Color.FromArgb(op, this._red, this._green, this._blue));
            GFX.DrawString(this.text,
               F,
               semiTransBrush2,
               new Point(x, y),
               StrFormat);
            GFX.DrawString(this.text,
                F,
                semiTransBrush,
                new Point(x, y),
                StrFormat);

            GFX.Save();

            SourceImage = bmp;

            MemoryStream MS = new MemoryStream();
            SourceImage.Save(MS, ImageFormat.Jpeg);

            return MS;
        }

        public void FindPerfectFontSize(bool find = true)
        {
            this.findperfectsize = find;
        }

        public void SetColor(int red, int green, int blue)
        {
            if (red > 255) red = 255;
            if (green > 255) green = 255;
            if (blue > 255) blue = 255;

            if (red <= 0) red = 0;
            if (green <= 0) green = 0;
            if (blue <= 0) blue = 0;

            this._red = red;
            this._green = green;
            this._blue = blue;
        }

        public void SetFontName(string fontname = "arial")
        {
            this.FontName = fontname;
        }

        public void SetFontSize(int FontSize = 10)
        {
            this.fontSize = FontSize;
        }

        public void SetMargin(int margin = 10)
        {
        }

        public void SetOpacity(double opacity = 90)
        {
            this._opacity = opacity;
        }

        public void SetText(string txt)
        {
            this.text = txt;
        }

        public void SetWaterMarkLocation(string LOCATION = "UPPER_RIGHT")
        {
            this.loc = LOCATION;
        }

        #endregion Methods
    }
}