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

        public GameForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (yoursBoxClick != null)
            {
                yoursBoxClick(sender, e);
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (YoursBoxMouseMove != null)
            {
                YoursBoxMouseMove(sender, e);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearField();
        }
        private void начатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearButton.Enabled = false;
            StartGame();
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (EnemyBoxMouseMove != null)
            {
            EnemyBoxMouseMove(sender, e);
            }
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (EnemyBoxMouseClick != null)
            {
                EnemyBoxMouseClick(sender, e);
            }
        }


        private Action<PictureBox, Bitmap> safeImageSet = (p, i) => { p.Image = i; };

        public ShipCount Ships
        {
            get
            {
                if (OneShip.Checked)
                {
                    return ShipCount.One;
                }
                else if (TwoShip.Checked)
                {
                    return ShipCount.Two;
                }
                else if (ThreeShip.Checked)
                {
                    return ShipCount.Three;
                }
                else
                {
                    return ShipCount.Four;
                }
            }
        }

        public PictureBox YoursBox
        {
            get
            {
                return this.pictureBox1;
            }
            set
            {
                this.pictureBox1 = value;
            }
        }
        public PictureBox EnemyBox
        {
            get
            {
                return this.pictureBox2;
            }
            set
            {
                this.pictureBox1 = value;
            }
        }

        public event Action<object, MouseEventArgs> yoursBoxClick;
        public event Action<object, MouseEventArgs> EnemyBoxMouseClick;
        public event Action<object, MouseEventArgs> YoursBoxMouseMove;
        public event Action<object, MouseEventArgs> EnemyBoxMouseMove;

        public event Action StartGame;
        public event Action ClearField;

        public string MessageString
        {
            get
            {
                return label1.Text;
            }
            set
            {
                if (label1.InvokeRequired)
                {
                    Action<Label, string> tmp = (x, y) => x.Text = y;
                    label1.Invoke(tmp, label1, value);
                }
                else
                {
                    label1.Text = value;
                }
            }
        }
    }
}
