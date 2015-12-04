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
        public StructMap map;
        private Paint draw;
        private int ship1, ship2, ship3, ship4;
        private bool horizon = true;
        private bool fixMap = false;
        private Point tempLoc;
        private IMainGameForm form;
        public SettingGame()
        {
            map = new StructMap();
            ship1 = ship2 = ship3 = ship4 = 0;
        }
        public void Attach(IMainGameForm form)
        {
            this.form = form;

            draw = new Paint(form.YoursBox.Width, form.YoursBox.Height);
            map = new StructMap();
            ship1 = ship2 = ship3 = ship4 = 0;

            form.YoursBoxMouseMove += MouseMove;
            form.yoursBoxClick += MouseClick;
            form.StartGame += NewGame;
            form.ClearField += ClearField;
        }
        public void Detach()
        {
            form.YoursBoxMouseMove -= MouseMove;
            form.yoursBoxClick -= MouseClick;
            form.StartGame -= NewGame;
            form.ClearField -= ClearField;

            this.form = null;
        }
        public Bitmap GetGrid()
        {
            return draw.grid;
        }
        public void ClearField()
        {
            map.ClearMap();
            draw.Clear();
            ship1 = ship2 = ship3 = ship4 = 0;

            form.YoursBox.Image = draw.TempBitmapYours;
        }
        public void NewGame()
        {
            Program.SendGame(new SearchMessage(new GameField(map.ships)));
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {
            var location = new Point(e.Location.X / StructMap.BlockSize, e.Location.Y / StructMap.BlockSize);
            if (location.X < 10 && location.Y < 10)
            {
                if (form.Ships == ShipCount.One && ship1 < 4)
                {
                    Move(location, 1);
                }
                if (form.Ships == ShipCount.Two && ship2 < 3)
                {
                    Move(location, 2);
                }
                if (form.Ships == ShipCount.Three && ship3 < 2)
                {
                    Move(location, 3);
                }
                if (form.Ships == ShipCount.Four && ship4 == 0)
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
                form.YoursBox.Image = GetImageTemp();
            }
            else
            {
                if (map.CheckLimit(location, horizon, size))
                {
                    draw.Ship(location, size, horizon, false);
                    fixMap = false;
                    tempLoc = location;
                    form.YoursBox.Image = GetImageTemp();
                }
            }
        }
        public void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && fixMap)
            {
                draw.FixImage();

                if (form.Ships == ShipCount.One && ship1 < 4)
                {
                    map.FixMap(horizon, 1, tempLoc);
                    ship1++;
                }
                if (form.Ships == ShipCount.Two && ship2 < 3)
                {
                    map.FixMap(horizon, 2, tempLoc);
                    ship2++;
                }
                if (form.Ships == ShipCount.Three && ship3 < 2)
                {
                    map.FixMap(horizon, 3, tempLoc);
                    ship3++;
                }
                if (form.Ships == ShipCount.Four && ship4 < 1)
                {
                    map.FixMap(horizon, 4, tempLoc);
                    ship4++;
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
