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
        Bitmap MainBitmap;
        public Bitmap TempBitmap { get; set; }
        Graphics g;
        int wh, ht;
        public Paint(int wh, int ht)
        {
            this.wh = wh;
            this.ht = ht;
            TempBitmap = new Bitmap(wh, ht);
            g = Graphics.FromImage(TempBitmap);
        }
        public Bitmap DrawGrid()
        {
            MainBitmap = new Bitmap(wh, ht);
            using (g = Graphics.FromImage(MainBitmap))
            {
                for (int i = 0; i < 300; i += 30)
                {
                    for (int j = 0; j < 300; j += 30)
                    {
                        g.DrawRectangle(Pens.Black, i, j, 30, 30);
                    }
                }
            }
            return MainBitmap;
        }
        public void FixImage()
        {
            MainBitmap = TempBitmap;
        }
        public void Ship(Point location, int size, bool hor, bool loc)
        {
            TempBitmap = new Bitmap(MainBitmap);
            using (g = Graphics.FromImage(TempBitmap))
            {
                Brush b;
                if (loc)
                    b = Brushes.Blue;
                else
                    b = Brushes.Red;
                if (hor)
                {
                    for (int i = 0, j = 0; i < size; i++, j += StructMap.BlockSize)
                    {
                        g.FillRectangle(b, location.X * StructMap.BlockSize + 1 + j, location.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                    }
                }
                else
                {
                    for (int i = 0, j = 0; i < size; i++, j += StructMap.BlockSize)
                    {
                        g.FillRectangle(b, location.X * StructMap.BlockSize + 1, location.Y * StructMap.BlockSize + 1 + j, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                    }
                }
            }
        }
        public void DrawMapYou(GameField field, CellType[,] pt)
        {
            TempBitmap = new Bitmap(DrawGrid());
            using (g = Graphics.FromImage(TempBitmap))
            {
                for (int i = 0; i < pt.GetLength(0); i++)
                {
                    for (int j = 0; j < pt.GetLength(1); j++)
                    {
                        if (pt[i, j] == CellType.Point)
                            g.FillEllipse(Brushes.Green, i * StructMap.BlockSize + 10, j * StructMap.BlockSize + 10, 10, 10);
                    }
                }

                for (int i = 0; i < field.ships.Count; i++)
                {
                    for (int j = 0; j < field.ships[i].palub.Count; j++)
                    {
                        if (field.ships[i].palub[j].type == DeckType.Live)
                        {
                            g.FillRectangle(Brushes.Blue, field.ships[i].palub[j].point.X * StructMap.BlockSize + 1, field.ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
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
                MainBitmap = TempBitmap;
            }
        }
        public void DrawMapEnemy(GameField field, CellType[,] pt)
        {
            TempBitmap = new Bitmap(DrawGrid());
            using (g = Graphics.FromImage(TempBitmap))
            {
                for (int i = 0; i < pt.GetLength(0); i++)
                {
                    for (int j = 0; j < pt.GetLength(1); j++)
                    {
                        if (pt[i, j] == CellType.Point)
                            g.FillEllipse(Brushes.Green, i * StructMap.BlockSize + 10, j * StructMap.BlockSize + 10, 10, 10);
                    }
                }
                for (int i = 0; i < field.ships.Count; i++)
                {
                    for (int j = 0; j < field.ships[i].palub.Count; j++)
                    {
                        if (field.ships[i].palub[j].type == DeckType.Hurt)
                        {
                            g.FillRectangle(Brushes.Orange, field.ships[i].palub[j].point.X * StructMap.BlockSize + 1, field.ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                        else if (field.ships[i].palub[j].type == DeckType.Dead)
                        {
                            g.FillRectangle(Brushes.Black, field.ships[i].palub[j].point.X * StructMap.BlockSize + 1, field.ships[i].palub[j].point.Y * StructMap.BlockSize + 1, StructMap.BlockSize - 1, StructMap.BlockSize - 1);
                        }
                    }
                }
                MainBitmap = TempBitmap;
            }
        }
        public void Point(Point location)
        {
            try
            {
                TempBitmap = new Bitmap(MainBitmap);
                using (g = Graphics.FromImage(TempBitmap))
                {
                    g.FillEllipse(Brushes.Green, location.X * StructMap.BlockSize + 10, location.Y * StructMap.BlockSize + 10, 10, 10);
                }
            }
            catch { }
        }
    }
}
