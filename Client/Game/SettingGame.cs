using Project2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Game
{
    public class SettingGame : IMauseAction
    {
        public IForm form { get; set; }
        Paint draw;
        public StructMap map;
        public SettingGame(int wh, int ht, IForm gameForm)
        {
            draw = new Paint(wh, ht);
            StructMap.BlockSize = 30;
            map = new StructMap();
            form = gameForm;
            ship1 = ship2 = ship3 = ship4 = 0;
        }

        public Bitmap GetGrid()
        {
            return draw.grid;
        }
        public void Clear()
        {
            map.Map = new int[10, 10];
            ship1 = ship2 = ship3 = ship4 = 0;
        }
        public void NewGame()
        {
            Program.SendGame(new SearchMessage(new GameField(map.ships)));
        }

        int ship1, ship2, ship3, ship4;
        bool horizon = true;
        bool fixMap = false;
        Point tempLoc;
        public void MouseMove(object sender, MouseEventArgs e)
        {
            var location = new Point(e.Location.X / StructMap.BlockSize, e.Location.Y / StructMap.BlockSize);
            if (location.X < 10 && location.Y < 10)
            {
                if (form.OneShip.Checked && ship1 < 4)
                {
                    Move(location, 1);
                }
                if (form.TwoShip.Checked && ship2 < 3)
                {
                    Move(location, 2);
                }
                if (form.ThreeShip.Checked && ship3 < 2)
                {
                    Move(location, 3);
                }
                if (form.FourShip.Checked && ship4 == 0)
                {
                    Move(location, 4);
                }
            }
        }
        void Move(Point location, int size)
        {
            if (map.CheckLocation(location, horizon, size))
            {
                draw.Ship(location, size, horizon, true);
                fixMap = true;
                tempLoc = location;
                form.InvalidateYou();
            }
            else
            {
                if (map.CheckLimit(location, horizon, size))
                {
                    draw.Ship(location, size, horizon, false);
                    fixMap = false;
                    tempLoc = location;
                    form.InvalidateYou();
                }
            }
        }
        public void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && fixMap)
            {
                draw.FixImage();

                if (form.OneShip.Checked)
                {
                    map.FixMap(horizon, 1, tempLoc);
                    ship1++;
                    if (ship1 == 4)
                        form.OneShip.Enabled = false;
                }
                if (form.TwoShip.Checked)
                {
                    map.FixMap(horizon, 2, tempLoc);
                    ship2++;
                    if (ship2 == 3)
                        form.TwoShip.Enabled = false;
                }
                if (form.ThreeShip.Checked)
                {
                    map.FixMap(horizon, 3, tempLoc);
                    ship3++;
                    if (ship3 == 2)
                        form.ThreeShip.Enabled = false;
                }
                if (form.FourShip.Checked)
                {
                    map.FixMap(horizon, 4, tempLoc);
                    ship4++;
                    form.FourShip.Enabled = false;
                }
                fixMap = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                horizon = !horizon;
            }
        }
        public Bitmap GetImageTemp()
        {
            return draw.TempBitmapYours;
        }
    }
}
