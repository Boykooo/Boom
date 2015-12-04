using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Game
{
    public partial class WaitForm : Form
    {
        public WaitForm()
        {
            InitializeComponent();
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {

        }

        private void WaitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
