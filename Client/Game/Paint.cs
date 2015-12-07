﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Project2;

namespace Client
{
    public class Paint
    {
        public Bitmap grid;
        private Bitmap MainBitmapYours;
        private Bitmap tempBitmapYours;
        public Bitmap TempBitmapYours
        {
            get
            {
                lock (lck)
                {
                    return tempBitmapYours;
                }
            }

        }

        private Bitmap MainBitmapEnemy;
        private Bitmap tempBitmapEnemy;
        public Bitmap TempBitmapEnemy
        {
            get
            {
                lock (lck)
                {
                    return tempBitmapEnemy;
                }
            }
        }
        private object lck = new object();
        // Graphics g;
        private int wh, ht;
        public Paint(int wh, int ht)
        {
            this.wh = wh;
            this.ht = ht;
            tempBitmapYours = new Bitmap(wh, ht);
            MainBitmapYours = new Bitmap(wh, ht);

            MainBitmapEnemy = new Bitmap(wh, ht);
            tempBitmapEnemy = new Bitmap(wh, ht);

            grid = new Bitmap(wh, ht);
            // g = Graphics.FromImage(TempBitmap);

            DrawGrid();
        }
        public void DrawGrid()
        {

            using (Graphics g = Graphics.FromImage(grid))
            {
                //for (int i = 0; i < 300; i += 30)
                //{
                //    for (int j = 0; j < 300; j += 30)
                //    {
                //        g.DrawRectangle(Pens.Black, i, j, 30, 30);
                //    }
                //}

                for (int i = 0; i < 11; i++)
                {
                    g.DrawLine(Pens.Black, i * StructMap.BlockSize, 0, i * StructMap.BlockSize, StructMap.BlockSize * 10);
                    g.DrawLine(Pens.Black, 0, i * StructMap.BlockSize, StructMap.BlockSize * 10, i * StructMap.BlockSize);
                }
            }
            //g.Dispose();
        }
        public void FixImage()
        {
            lock (lck)
            {
                using (Graphics g = Graphics.FromImage(MainBitmapYours))
                {
                    g.Clear(Color.White);
                    g.DrawImage(tempBitmapYours, 0, 0);
                }
            }
        }
        public void Ship(Point location, int size, bool hor, bool loc)
        {
            DrawShip(location, size, hor, loc);
        }
        private void DrawShip(Point location, int size, bool hor, bool loc)
        {
            lock (lck)
            {
                using (Graphics g = Graphics.FromImage(tempBitmapYours))
                {
                    Color c = loc ? Color.Red : Color.Blue;
                    Brush b = new SolidBrush(c);
                    g.Clear(Color.White);

                    g.DrawImage(grid, 0, 0);
                    g.DrawImage(MainBitmapYours, 0, 0);

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
        }
        public void DrawField(GameField field, CellType[,] pt, bool isYours)
        {
            lock (lck)
            {
                Bitmap newBitmap = isYours ? tempBitmapYours : tempBitmapEnemy;

                Graphics g = Graphics.FromImage(newBitmap);
                using (g)
                {
                    g.Clear(Color.White);
                    g.DrawImage(grid, 0, 0);

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

                    Graphics tmp = isYours ? Graphics.FromImage(MainBitmapYours) : Graphics.FromImage(MainBitmapEnemy);

                    using (tmp)
                    {
                        tmp.Clear(Color.White);
                        tmp.DrawImage(newBitmap, 0, 0);
                    }

                }

                

            }
        }
        public void Point(Point location)
        {
            lock (lck)
            {

                //TempBitmap = new Bitmap(MainBitmap);
                using (Graphics g = Graphics.FromImage(tempBitmapEnemy))
                {

                    g.Clear(Color.White);
                    g.DrawImage(MainBitmapEnemy, 0, 0);

                    g.FillEllipse(Brushes.Green, location.X * StructMap.BlockSize + 10, location.Y * StructMap.BlockSize + 10, 10, 10);
                }


            }
        }
        public void DrawCell(Point location)
        {
            lock (lck)
            {
                using (Graphics g = Graphics.FromImage(tempBitmapYours))
                {

                    g.FillEllipse
                        (
                        Brushes.Plum,
                        location.X * StructMap.BlockSize + StructMap.BlockSize / 4,
                        location.Y * StructMap.BlockSize + StructMap.BlockSize / 4,
                        StructMap.BlockSize / 2, StructMap.BlockSize / 2
                        );
                }
            }
        }
        public void Clear()
        {
            lock (lck)
            {
                using (Graphics g = Graphics.FromImage(MainBitmapYours))
                {
                    g.Clear(Color.White);
                    g.DrawImage(grid, new Point(0, 0));

                }
            }
        }
    }
}
