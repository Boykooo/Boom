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
using Server;
using System.Net.Sockets;
using Project2;

namespace Client6
{
    public partial class RegistrationForm : Form
    {
        public string Nick { get; set; }
        public RegistrationForm()
        {
            InitializeComponent();
        }
        private void RegistrationForm_Load(object sender, EventArgs e)
        {
        }
        private void EndRegistration_Click(object sender, EventArgs e)
        {
            if (Check.CheckNickName(NickName.Text))
            {
                int port = 8888;
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress adress = host.AddressList[0];
                IPEndPoint endPoint = new IPEndPoint(adress, port);
                Socket socket = new Socket(adress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                socket.Send(MessageSerializer.Serialize(new RegistrationMessage(NickName.Text)));
                byte[] temp = new byte[1000000];
                socket.Receive(temp);
                RegistrationResultMessage m = (RegistrationResultMessage)MessageSerializer.Deserialize(temp);
                if (m.ok)
                {
                    this.Close();
                }
            }
        }
    }
}
