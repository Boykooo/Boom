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
    public partial class GameForm : Form, IMainGameForm
    {
        bool turn;
        public bool Turn
        {
            get { return turn; }
            set
            {
                Action<Label, string> safe = (x, y) => x.Text = y;
                turn = value;
                string msg = turn ? "Ваш ход" : "Ход противника";

                label1.Invoke(safe, label1, msg);
            }
        }



        public bool Connect { get { return Program.state == ClientState.Gaming; } }
        private SettingGame actSet;
        private ActGame actGame;
        public GameForm()
        {
            InitializeComponent();
            actSet = new SettingGame(pictureBox1.Width, pictureBox1.Height, this);
            //actSet.InitGameForm(this);
            pictureBox1.Image = actSet.GetGrid();
        }

        private void CreateRadio(RadioButton radio, Point location, string name, string text)
        {
            radio.AutoSize = true;
            radio.Location = location;
            radio.Name = name;
            radio.Size = new System.Drawing.Size(69, 17);
            radio.Text = text;
            radio.TabStop = true;
            radio.UseVisualStyleBackColor = true;
            this.Controls.Add(radio);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Program.state == ClientState.Online)
            {
                actSet.MouseClick(sender, e);
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Program.state == ClientState.Online)
            {
                actSet.MouseMove(sender, e);
            }
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            actSet.Clear();
            pictureBox1.Image = actSet.GetGrid();
            OneShip.Enabled = TwoShip.Enabled = ThreeShip.Enabled = FourShip.Enabled = true;
        }
        private void начатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearButton.Enabled = OneShip.Enabled = TwoShip.Enabled = ThreeShip.Enabled = FourShip.Enabled = false;
            actGame = new ActGame(this, pictureBox2.Width, pictureBox2.Height, actSet.map);
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
            {
               // pictureBox1.Image = actGame.GetImageYours();

                pictureBox1.Invoke(safeImageSet, pictureBox1, actGame.GetImageYours());
            }
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
           // pictureBox2.Image = actGame.GetImageEnemy();

            pictureBox2.Invoke(safeImageSet, pictureBox2, actGame.GetImageEnemy());
        }
        public void ReDraw(GameField your, GameField enemy, bool turn)
        {
            actGame.ReDraw(your, enemy);
            Turn = turn;
        }

        Action<PictureBox, Bitmap> safeImageSet = (p, i) => { p.Image = i; };

        public ShipCount Ships
        {
            get { throw new NotImplementedException(); }
        }

        public PictureBox YoursBox
        {
            get
            {
                return YoursBox;
            }
            set
            {
                YoursBox = value;
            }
        }

        public PictureBox EnemyBox
        {
            get
            {
                return EnemyBox;
            }
            set
            {
                EnemyBox = value;
            }
        }

        public event Action<object, MouseEventArgs> yoursBoxClick;
        public event Action<object, MouseEventArgs> EnemyBoxMouseClick;
        public event Action<object, MouseEventArgs> YoursBoxMouseMove;
        public event Action<object, MouseEventArgs> EnemyBoxMouseMove;

        public event Action StartGame;
        public event Action ClearField;


        
    }
}
