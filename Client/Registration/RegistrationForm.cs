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
        public ServerManager Srv { get; set; }
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
                    int port = 8888;
                    IPHostEntry host = Dns.GetHostEntry( IPAddress.Parse(maskedTextBox1.Text));
                    IPAddress adress = host.AddressList[0];
                    Srv = new ServerManager(this, adress, port);
                    Srv.SendMessage(new RegistrationMessage(NickName.Text));
                    Program.state = ClientState.Online;
                }
                catch(Exception exc)
                {

                }
            }
        }
    }
}
