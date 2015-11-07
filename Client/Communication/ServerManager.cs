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
    Form reg;
    GameForm game;
    Socket socket;
    ActGame actGame;
    public ServerManager(Form regform, IPAddress ip, int port)
    {
        reg = regform;
        IPEndPoint end = new IPEndPoint(ip, port);
        socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(end);
    }
    public void InitializeGameForm(GameForm game)
    {
        this.game = game;
    }
    public void InitializeActGame(ActGame actGame)
    {
        this.actGame = actGame; 
    }
    public void SendMessage(Messages message)
    {
        socket.Send(Serialize(message));
        Task t = new Task(Listen);
        t.Start();
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
                    case "StartGameMessage":
                        StartGameMessage q = (StartGameMessage) m;
                        game.Connect = true;
                        actGame.Turn = q.turn;
                        break;
                    case "FieldStateMessage":
                        FieldStateMessage f = (FieldStateMessage) m;
                        actGame.Turn = f.turn;
                        actGame.ReDraw(f.you, f.enemy);
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
