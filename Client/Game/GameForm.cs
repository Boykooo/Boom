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
       

        Action<PictureBox, Bitmap> safeImageSet = (p, i) => { p.Image = i; };
        Action<Control, string> safeControlTextSet = (x, y) => x.Text = y;

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

                    label1.Invoke(safeControlTextSet, label1, value);
                }
                else
                {
                    label1.Text = value;
                }
            }
        }


        public void SetNameButton(ShipCount ship, string name)
        {
            RadioButton button;
            switch (ship)
            {
                case ShipCount.One:
                    button = OneShip;
                    break;
                case ShipCount.Two:
                    button = TwoShip;
                    break;
                case ShipCount.Three:
                    button = ThreeShip;
                    break;
                case ShipCount.Four:
                    button = FourShip;
                    break;
                default:
                    throw new ArgumentException();
            }
            if (button.InvokeRequired)
            {
                button.Invoke(safeControlTextSet, button, name);
            }
            else
            {
                button.Text = name;
            }
        }
        public void SwitchButton(ShipCount ship, bool state)
        {
            RadioButton button;
            switch (ship)
            {
                case ShipCount.One:
                    button = OneShip;
                    break;
                case ShipCount.Two:
                    button = TwoShip;
                    break;
                case ShipCount.Three:
                    button = ThreeShip;
                    break;
                case ShipCount.Four:
                    button = FourShip;
                    break;
                default:
                    throw new ArgumentException();
            }

            button.Enabled = state;
        }




        public bool ClearButtonState
        {
            get
            {
                return ClearButton.Enabled;
            }
            set
            {
                if (ClearButton.InvokeRequired)
                {
                    Action<Control, bool> tmp = (x, y) => x.Enabled = y;
                    ClearButton.Invoke(tmp, ClearButton, value);
                }
                else
                {
                    ClearButton.Enabled = value;
                }
            }
        }
    }
}
