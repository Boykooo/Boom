using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project2;

namespace Client.Game
{
    public partial class GameForm : Form
    {
        public bool Connect { get; set; }
        ActGameForm act;
        int ship1, ship2, ship3, ship4;
        bool fixMap = false;
        public GameForm()
        {
            InitializeComponent();
            act = new ActGameForm(pictureBox1.Width, pictureBox1.Height);
            act.Registration();
            act.InitGameForm(this);
            ship1 = ship2 = ship3 = ship4 = 0;
            pictureBox1.Image = act.GetGrid();
            pictureBox2.Image = act.GetGrid();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && fixMap)
            {
                act.AddChanges();

                if (OneShip.Checked)
                {
                    act.AddShip(horizon, 1, tempLoc);
                    ship1++;
                    if (ship1 == 4)
                        OneShip.Enabled = false;
                }
                if (TwoShip.Checked)
                {
                    act.AddShip(horizon, 2, tempLoc);
                    ship2++;
                    if (ship2 == 3)
                        TwoShip.Enabled = false;
                }
                if (ThreeShip.Checked)
                {
                    act.AddShip(horizon, 3, tempLoc);
                    ship3++;
                    if (ship3 == 2)
                        ThreeShip.Enabled = false;
                }
                if (FourShip.Checked)
                {
                    act.AddShip(horizon, 4, tempLoc);
                    ship4++;
                    FourShip.Enabled = false;
                }
                fixMap = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                horizon = !horizon;
            }
        }
        Point tempLoc;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var location = new Point(e.Location.X / StructMap.BlockSize, e.Location.Y / StructMap.BlockSize);
            if (location.X < 10 && location.Y < 10)
            {
                if (OneShip.Checked && ship1 < 4 && act.CheckLocShip(location, horizon, 1))
                {
                    pictureBox1.Image = act.GetShipImage(location, 1, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
                if (TwoShip.Checked && ship2 < 3 && act.CheckLocShip(location, horizon, 2))
                {
                    pictureBox1.Image = act.GetShipImage(location, 2, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
                if (ThreeShip.Checked && ship3 < 2 && act.CheckLocShip(location, horizon, 3))
                {
                    pictureBox1.Image = act.GetShipImage(location, 3, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
                if (FourShip.Checked && ship4 == 0 && act.CheckLocShip(location, horizon, 4))
                {
                    pictureBox1.Image = act.GetShipImage(location, 4, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
            }
        }
        bool horizon = true;
        private void ClearButton_Click(object sender, EventArgs e)
        {
            act.Clear();
            pictureBox1.Image = act.GetGrid();
            ship1 = ship2 = ship3 = ship4 = 0;
            OneShip.Enabled = TwoShip.Enabled = ThreeShip.Enabled = FourShip.Enabled = true;
        }
        private void начатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            act.NewGame();
        }
        public void PaintMaps(List<Ship> one, List<Ship> two)
        {
            pictureBox1.Image = act.GetFullMap(one);
            pictureBox2.Image = act.GetFullMap(two);
        }
    }
}
