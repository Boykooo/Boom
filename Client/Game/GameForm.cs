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
    public partial class GameForm : Form, IForm
    {
        private bool turn;
        public bool Turn
        {
            get { return turn; }
            set 
            {
                Action<Label, string> safe = (x, y) => x.Text = y;
                turn = value;
                string msg = turn ? "Ваш ход" : "Ход противника!";

                label1.Invoke(safe, label1, msg);
            }
        }
        public RadioButton OneShip { get; set; }

        public RadioButton TwoShip { get; set; }

        public RadioButton ThreeShip { get; set; }

        public RadioButton FourShip { get; set; }
        public bool Connect { get; set; }
        SettingGame actSet;
        ActGame actGame;
        public GameForm()
        {
            InitializeComponent();
            RadioInit();
            actSet = new SettingGame(pictureBox1.Width, pictureBox1.Height, this);
            actSet.Registration();
            actSet.InitGameForm(this);
            pictureBox1.Image = actSet.GetGrid();
        }
        void RadioInit()
        {
            // 
            // OneShip
            // 
            OneShip = new RadioButton();
            this.OneShip.AutoSize = true;
            this.OneShip.Location = new System.Drawing.Point(14, 213);
            this.OneShip.Name = "OneShip";
            this.OneShip.Size = new System.Drawing.Size(69, 17);
            this.OneShip.TabIndex = 8;
            this.OneShip.TabStop = true;
            this.OneShip.Text = "1 палуба";
            this.OneShip.UseVisualStyleBackColor = true;
            // 
            // TwoShip
            // 
            TwoShip = new RadioButton();
            this.TwoShip.AutoSize = true;
            this.TwoShip.Location = new System.Drawing.Point(14, 236);
            this.TwoShip.Name = "TwoShip";
            this.TwoShip.Size = new System.Drawing.Size(71, 17);
            this.TwoShip.TabIndex = 9;
            this.TwoShip.TabStop = true;
            this.TwoShip.Text = "2 палубы";
            this.TwoShip.UseVisualStyleBackColor = true;
            // 
            // ThreeShip
            // 
            ThreeShip = new RadioButton();
            this.ThreeShip.AutoSize = true;
            this.ThreeShip.Location = new System.Drawing.Point(14, 259);
            this.ThreeShip.Name = "ThreeShip";
            this.ThreeShip.Size = new System.Drawing.Size(71, 17);
            this.ThreeShip.TabIndex = 10;
            this.ThreeShip.TabStop = true;
            this.ThreeShip.Text = "3 палубы";
            this.ThreeShip.UseVisualStyleBackColor = true;
            // 
            // FourShip
            // 
            FourShip = new RadioButton();
            this.FourShip.AutoSize = true;
            this.FourShip.Location = new System.Drawing.Point(14, 282);
            this.FourShip.Name = "FourShip";
            this.FourShip.Size = new System.Drawing.Size(71, 17);
            this.FourShip.TabIndex = 11;
            this.FourShip.TabStop = true;
            this.FourShip.Text = "4 палубы";
            this.FourShip.UseVisualStyleBackColor = true;

            this.Controls.Add(this.FourShip);
            this.Controls.Add(this.ThreeShip);
            this.Controls.Add(this.TwoShip);
            this.Controls.Add(this.OneShip);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            actSet.MouseClick(sender, e);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            actSet.MouseMove(sender, e);
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            actSet.Clear();
            pictureBox1.Image = actSet.GetGrid();
            OneShip.Enabled = TwoShip.Enabled = ThreeShip.Enabled = FourShip.Enabled = true;
        }
        private void начатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actGame = new ActGame(this, pictureBox2.Width, pictureBox2.Height, actSet.map, actSet.srv);
            pictureBox2.Image = actGame.GetGrid();
            actSet.NewGame();
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Connect)
            {
                actGame.MouseMove(sender, e);
            }
        }
        private void GameForm_Load(object sender, EventArgs e)
        {
        }
        public void InvalidateYou()
        {
            if (!Connect)
                pictureBox1.Image = actSet.GetImageTemp();
            else
                pictureBox1.Image = actGame.GetImageYours();
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (Connect)
            {
                actGame.MouseClick(sender, e);
            }
        }
        public void InvalidateEnemy()
        {
            pictureBox2.Image = actGame.GetImageEnemy();
        }
    }
}
