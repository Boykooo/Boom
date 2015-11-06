using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Project2;

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
        public Bitmap DrawGrid()
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
        public Bitmap GetFullMap(List<Ship> ships)
        {
            Bitmap map = new Bitmap(DrawGrid());
            using(g = Graphics.FromImage(map))
            {
                for (int i = 0; i < ships.Count; i++)
                {
                    for (int j = 0; j < ships[i].palub.Count; j++)
                    {
                        if (ships[i].palub[j].type == DeckType.Live)
                        {
                            g.FillRectangle(Brushes.Red, ships[i].palub[j].point.X * StructMap.BlockSize + 1, ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                        else if (ships[i].palub[j].type == DeckType.Hurt)
                        {
                            g.FillRectangle(Brushes.Orange, ships[i].palub[j].point.X * StructMap.BlockSize + 1, ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                        else if (ships[i].palub[j].type == DeckType.Dead)
                        {
                            g.FillRectangle(Brushes.Black, ships[i].palub[j].point.X * StructMap.BlockSize + 1, ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                    }
                }
            }
            return map; 
        }
        public Bitmap GetFullMap(GameField field)
        {
            Bitmap map = new Bitmap(DrawGrid());
            using (g = Graphics.FromImage(map))
            {
                for (int i = 0; i < field.ships.Count; i++)
                {
                    for (int j = 0; j < field.ships[i].palub.Count; j++)
                    {
                        if (field.ships[i].palub[j].type == DeckType.Live)
                        {
                            g.FillRectangle(Brushes.Red, field.ships[i].palub[j].point.X * StructMap.BlockSize + 1, field.ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                        else if (field.ships[i].palub[j].type == DeckType.Hurt)
                        {
                            g.FillRectangle(Brushes.Orange, field.ships[i].palub[j].point.X * StructMap.BlockSize + 1, field.ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                        else if (field.ships[i].palub[j].type == DeckType.Dead)
                        {
                            g.FillRectangle(Brushes.Black, field.ships[i].palub[j].point.X * StructMap.BlockSize + 1, field.ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                    }
                }
                for (int i = 0; i < field.field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.field.GetLength(1); j++)
                    {
                        if (field.field[i, j] == CellType.Point)
                            g.FillEllipse(Brushes.Blue, i * StructMap.BlockSize + 10, j * StructMap.BlockSize + 10, 15, 15);
                    }
                }
            }
            return map;
        }
    }
}
