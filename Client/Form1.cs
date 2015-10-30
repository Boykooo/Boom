using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        RegForm reg;
        int ship1, ship2, ship3, ship4;
        StructMap game;
        Graphics g;
        Paint draw;
        bool fixMap = false;
        public MainForm()
        {
            InitializeComponent();
            g = CreateGraphics();
            draw = new Paint(pictureBox1.Width, pictureBox1.Height);
            StructMap.BlockSize = 30;
            game = new StructMap();
            ship1 = ship2 = ship3 = ship4 = 0;
            pictureBox1.Image = draw.Default();
            pictureBox2.Image = draw.Default();
            reg = new RegForm();
            reg.ShowDialog();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && fixMap)
            {
                draw.FixImage();

                if (OneShip.Checked)
                {
                    game.FixMap(horizon, 1, tempLoc);
                    ship1++;
                    if (ship1 == 4)
                        OneShip.Enabled = false;
                }
                if (TwoShip.Checked)
                {
                    game.FixMap(horizon, 2, tempLoc);
                    ship2++;
                    if (ship2 == 3)
                        TwoShip.Enabled = false;
                }
                if (ThreeShip.Checked)
                {
                    game.FixMap(horizon, 3, tempLoc);
                    ship3++;
                    if (ship3 == 2)
                        ThreeShip.Enabled = false;
                }
                if (FourShip.Checked)
                {
                    game.FixMap(horizon, 4, tempLoc);
                    ship4++;
                    FourShip.Enabled = false;
                }
                fixMap = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                Horizon();
            }
        }
        Point tempLoc;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var location = new Point(e.Location.X / StructMap.BlockSize, e.Location.Y / StructMap.BlockSize);
            if (location.X < 10 && location.Y < 10)
            {
                if (OneShip.Checked && ship1 < 4 && game.CheckLocation(location, horizon, 1))
                {
                    pictureBox1.Image = draw.Ship(location, 1, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
                if (TwoShip.Checked && ship2 < 3 && game.CheckLocation(location, horizon, 2))
                {
                    pictureBox1.Image = draw.Ship(location, 2, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
                if (ThreeShip.Checked && ship3 < 2 && game.CheckLocation(location, horizon, 3))
                {
                    pictureBox1.Image = draw.Ship(location, 3, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
                if (FourShip.Checked && ship4 == 0 && game.CheckLocation(location, horizon, 4))
                {
                    pictureBox1.Image = draw.Ship(location, 4, horizon);
                    fixMap = true;
                    tempLoc = location;
                }
            }
        }
        bool horizon = true;
        void Horizon()
        {
            switch (horizon)
            {
                case true:
                    horizon = false;
                    break;
                case false:
                    horizon = true;
                    break;
            }
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            game.Map = new int[10, 10];
            pictureBox1.Image = draw.Default();
            ship1 = ship2 = ship3 = ship4 = 0;
            OneShip.Enabled = TwoShip.Enabled = ThreeShip.Enabled = FourShip.Enabled = true;
        }
        private void начатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
