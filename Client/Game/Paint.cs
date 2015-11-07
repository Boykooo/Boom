﻿using System;
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
            g = Graphics.FromImage(MainBitmap);
            for (int i = 0; i < 300; i += 30)
            {
                for (int j = 0; j < 300; j += 30)
                {
                    g.DrawRectangle(Pens.Black, i, j, 30, 30);
                }
            }
            g.Dispose();
            return MainBitmap;
        }
        public void FixImage()
        {
            MainBitmap = TempBitmap;
        }
        public void Ship(Point location, int size, bool hor)
        {
            g.Dispose();
            TempBitmap = new Bitmap(MainBitmap);
            g = Graphics.FromImage(TempBitmap);
            switch (size)
            {
                case 1:
                    DrawShip(location, 1, hor);
                    break;
                case 2:
                    DrawShip(location, 2, hor);
                    break;
                case 3:
                    DrawShip(location, 3, hor);
                    break;
                case 4:
                    DrawShip(location, 4, hor);
                    break;
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
        public void DrawMapYou(GameField field)
        {
            MainBitmap = new Bitmap(DrawGrid());
            using (g = Graphics.FromImage(MainBitmap))
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
            }
        }
        public void DrawMapEnemy(GameField field, CellType[,] pt)
        {
            MainBitmap = new Bitmap(DrawGrid());
            using (g = Graphics.FromImage(MainBitmap))
            {
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
                for (int i = 0; i < pt.GetLength(0); i++)
                {
                    for (int j = 0; j < pt.GetLength(1); j++)
                    {
                        if (pt[i,j] == CellType.Point)
                            g.FillEllipse(Brushes.Green, i * StructMap.BlockSize + 15, j * StructMap.BlockSize + 15, 10, 10);
                    }
                }
            }
        }
        public void Point(Point location)
        {
            g.Dispose();
            TempBitmap = new Bitmap(MainBitmap);
            g = Graphics.FromImage(TempBitmap);
            g.FillEllipse(Brushes.Green, location.X * StructMap.BlockSize + 15, location.Y * StructMap.BlockSize + 15, 10, 10);
        }
    }
}
