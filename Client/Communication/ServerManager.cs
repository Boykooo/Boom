using Client;
using Project2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class ServerManager
{
    Form reg;
    Socket socket;
    public ServerManager(Form regform)
    {
        reg = regform;
    }
    public void Connect(IPAddress ip, int port, string nick)
    {
        IPEndPoint end = new IPEndPoint(ip, port);
        socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(end);

        Send(new RegistrationMessage(nick));

        Task t = new Task(Listen);
        t.Start();
    }

    public void Send(Messages message)
    {
        socket.Send(Serialize(message));
    }

    void Listen()
    {
        byte[] tmp = new byte[100000];

        try
        {
            while (true)
            {
                int i = socket.Receive(tmp);

                Messages m = Deserialize(tmp.Take(i).ToArray());

                switch (m.GetType().Name)
                {
                    case "RegistrationResultMessage":

                        Action a = () => reg.Close();
                        reg.Invoke(a);
                        //MainGameForm game = new MainGameForm();
                        //game.Show();
                        break;
                }
            }
        }
        catch
        {
            Program.state = ClientState.Offline;
        }
    }

    byte[] Serialize(Messages m)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter b = new BinaryFormatter();
        b.Serialize(stream, m);

        return stream.GetBuffer();
    }

    Messages Deserialize(byte[] bytes)
    {
        MemoryStream stream = new MemoryStream(bytes);
        BinaryFormatter b = new BinaryFormatter();

        return (Messages)b.Deserialize(stream);
    }
}
