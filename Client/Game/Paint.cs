using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Client
{
    class Paint
    {
        Bitmap mainBitmap;
        Bitmap tempBitmap;
        Graphics g;
        int wh, ht;
        public Paint(int wh, int ht)
        {
            this.wh = wh;
            this.ht = ht;
            tempBitmap = new Bitmap(wh, ht);
            g = Graphics.FromImage(tempBitmap);
        }
        public Bitmap Default()
        {
            mainBitmap = new Bitmap(wh, ht);
            g = Graphics.FromImage(mainBitmap);
            for (int i = 0; i < 300; i += 30)
            {
                for (int j = 0; j < 300; j += 30)
                {
                    g.DrawRectangle(Pens.Black, i, j, 30, 30);
                }
            }
            g.Dispose();
            return mainBitmap;
        }
        public void FixImage()
        {
            mainBitmap = tempBitmap;
        }
        public Bitmap Ship(Point location, int size, bool hor)
        {
            g.Dispose();
            tempBitmap = new Bitmap(mainBitmap);
            g = Graphics.FromImage(tempBitmap);
            switch (size)
            {
                case 1:
                    DrawShip(location, 1, hor);
                    return tempBitmap;
                case 2:
                    DrawShip(location, 2, hor);
                    return tempBitmap;
                case 3:
                    DrawShip(location, 3, hor);
                    return tempBitmap;
                case 4:
                    DrawShip(location, 4, hor);
                    return tempBitmap;
                default:
                    return mainBitmap;
            }
        }
        private void DrawShip(Point location, int size, bool hor)
        {
            if (hor)
            {
                for (int i = 0, j = 0; i < size; i++, j += StructMap.BlockSize)
                {
                    g.FillRectangle(Brushes.Red, location.X * StructMap.BlockSize + 1 + j, location.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                }
            }
            else
            {
                for (int i = 0, j = 0; i < size; i++, j += StructMap.BlockSize)
                {
                    g.FillRectangle(Brushes.Red, location.X * StructMap.BlockSize + 1, location.Y * StructMap.BlockSize + 1 + j, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                }
            }
        }

    }
}
