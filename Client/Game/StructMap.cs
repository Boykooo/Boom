﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Project2;

namespace Client
{
    public class StructMap
    {
        public List<Ship> ships;
        public int[,] Map { get; set; }
        public CellType[,] point;
        public static int BlockSize { get; set; }
        public StructMap()
        {
            Map = new int[10, 10];
            ships = new List<Ship>();
            point = new CellType[10, 10];
        }
        public bool CheckLocation(Point location, bool horizont, int size)
        {
            switch (horizont)
            {
                case true:
                    for (int i = 0; i < size; i++)
                    {
                        if ((location.X + i > 9) || Map[location.X + i, location.Y] != 0 || !CheckRegion(new Point(location.X + i, location.Y)))
                            return false;
                    }
                    return true;
                case false:
                    for (int i = 0; i < size; i++)
                    {
                        if ((location.Y + i > 9) || Map[location.X, location.Y + i] != 0 || !CheckRegion(new Point(location.X, location.Y + i)))
                            return false;
                    }
                    return true;
            }
            return false;
        }
        public bool CheckLimit(Point location, bool horizont, int size)
        {
            switch (horizont)
            {
                case true:
                    for (int i = 0; i < size; i++)
                    {
                        if ((location.X + i > 9))
                            return false;
                    }
                    return true;
                case false:
                    for (int i = 0; i < size; i++)
                    {
                        if ((location.Y + i > 9))
                            return false;
                    }
                    return true;
            }
            return false;
        }
        private bool CheckRegion(Point location)
        {
            bool before = ((location.Y == 0 || location.X == 0) || location.Y - 1 >= 0 && location.X - 1 >= 0 && Map[location.X - 1, location.Y - 1] == 0) && (location.X == 0 || location.X - 1 >= 0 && Map[location.X - 1, location.Y] == 0) && ((location.X == 0 || location.Y == 9) || location.Y + 1 < 10 && location.X - 1 >= 0 && Map[location.X - 1, location.Y + 1] == 0);
            bool middle = (location.Y == 0 || location.Y - 1 >= 0 && Map[location.X, location.Y - 1] == 0) && (location.Y == 9 || location.Y + 1 < 10 && Map[location.X, location.Y + 1] == 0);
            bool after = ((location.Y == 0 || location.X == 9) || location.Y - 1 >= 0 && location.X + 1 < 10 && Map[location.X + 1, location.Y - 1] == 0) && (location.X == 9 || location.X + 1 < 10 && Map[location.X + 1, location.Y] == 0) && ((location.X == 9 || location.Y == 9) || location.Y + 1 < 10 && location.X + 1 < 10 && Map[location.X + 1, location.Y + 1] == 0);
            return before && middle && after;

            //Point[] tmp = new Point[]
            //{
            //    location,
            //    new Point(location.X, location.Y-1),
            //    new Point(location.X+1, location.Y),
            //    new Point(location.X+1, location.Y+1),
            //    new Point(location.X+1, location.Y-1),
            //    new Point(location.X-1, location.Y),
            //    new Point(location.X-1, location.Y+1),
            //    new Point(location.X-1, location.Y-1),
            //    new Point(location.X, location.Y+1),
            //};
            //bool res = true;
            //foreach (var p in tmp)
            //{
            //    try
            //    {
            //        res = res && (Map[p.X, p.Y] == 0);
            //    }
            //    catch { }
            //}
            //return res;
        }
        public void FixMap(bool horizont, int size, Point startLocation)
        {
            Ship s = new Ship(size);
            if (horizont)
            {
                for (int i = 0; i < size; i++)
                {
                    Map[startLocation.X + i, startLocation.Y] = size;
                    s.palub.Add(new Deck(new Point(startLocation.X + i, startLocation.Y)));
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    Map[startLocation.X, startLocation.Y + i] = size;
                    s.palub.Add(new Deck(new Point(startLocation.X, startLocation.Y + i)));
                }
            }
            ships.Add(s);
        }
        public void AddPoint(Point location)
        {
            point[location.X, location.Y] = CellType.Point;
        }
        public void ClearMap()
        {
            Map = new int[10, 10];
            ships.Clear();
            point = new CellType[10, 10];
        }
    }
}
