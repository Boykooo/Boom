using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Project2;

namespace Client
{
    public partial class RegForm : Form
    {
        public string Nick { get; set; }
        public RegForm()
        {
            InitializeComponent();
        }
        private void RegistrationForm_Load(object sender, EventArgs e)
        {
        }
        private void EndRegistration_Click(object sender, EventArgs e)
        {

            if (NickName.Text != "" && NickName.Text.Length < 10)
            {
                try
                {
                    Program.TryConnect(maskedTextBox1.Text, NickName.Text);
                }
                catch(Exception exc)
                {

                }
            }
        }

        private void RegForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.state == ClientState.Offline)
            {
                Application.Exit();
            }
        }
    }
}
