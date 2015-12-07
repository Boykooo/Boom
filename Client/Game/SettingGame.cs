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

            form.EnemyBox.Image = draw.TempBitmapEnemy;
            form.YoursBox.Image = draw.TempBitmapYours;

            form.ClearButtonState = true;
            ResetButtons();
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
            ResetButtons();

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
        private void Move(Point location, int size)
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

                UpdateButtons();
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


        void ResetButtons()
        {
            form.SetNameButton(ShipCount.One, "Однопалубник (0/4)");
            form.SetNameButton(ShipCount.Two, "Двухпалубник (0/3)");
            form.SetNameButton(ShipCount.Three, "Трехпалубник (0/2)");
            form.SetNameButton(ShipCount.Four, "Четырехпалубник (0/1)");

            form.SwitchButton(ShipCount.One, true);
            form.SwitchButton(ShipCount.Two, true);
            form.SwitchButton(ShipCount.Three, true);
            form.SwitchButton(ShipCount.Four, true);
        }
        void UpdateButtons()
        {
            form.SetNameButton(ShipCount.One, "Однопалубник (" + ship1.ToString() + "/4)");
            form.SetNameButton(ShipCount.Two, "Двухпалубник (" + ship2.ToString() + "/3)");
            form.SetNameButton(ShipCount.Three, "Трехпалубник (" + ship3.ToString() + "/2)");
            form.SetNameButton(ShipCount.Four, "Четырехпалубник (" + ship4.ToString() + "/1)");

            form.SwitchButton(ShipCount.One, ship1 < 4);
            form.SwitchButton(ShipCount.Two, ship2 < 3);
            form.SwitchButton(ShipCount.Three, ship3 < 2);
            form.SwitchButton(ShipCount.Four, ship4 < 1);
        }
    }
}
