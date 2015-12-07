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
using Client.Game;

public class ServerManager
{
    Socket socket;

    public ServerManager()
    {

    }
    public void Start(IPAddress ip, int port)
    {
        IPEndPoint end = new IPEndPoint(ip, port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(end);
        Task t = new Task(Listen);
        t.Start();
    }

    public void SendMessage(Messages message)
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

                        Program.Connected();
                        //MainGameForm game = new MainGameForm();
                        //game.Show();
                        break;
                    case "StartGameMessage":
                        Program.StartGame((StartGameMessage)m);
                        break;
                    case "FieldStateMessage":
                        Program.NewGameMessage((FieldStateMessage)m);
                        //actGame.Turn = f.turn;

                        //Action b = () => game.Turn = f.turn;
                        //game.Invoke(b);
                        //actGame.ReDraw(f.you, f.enemy);
                        break;
                }
            }
        }
        catch
        {
            Program.Disconnected();
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
