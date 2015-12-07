using Project2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
   public class Client
    {
        public Client(Socket socket, int id)
        {
            this.socket = socket;
            this.Id = id;
            nick = "";

            Task task = new Task(Listen);
            task.Start();
        }

        public string nick;
        public int Id { get; private set; }
        public Socket socket;
        public GameField gameField { get; set; }
        public bool search;

        public void Send(Messages message)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter b = new BinaryFormatter();
                stream.Position = 0;
                b.Serialize(stream, message);

                socket.Send(stream.GetBuffer());
            }
            catch (Exception e)
            {
                MainController.Instance.logger.Log(e.ToString());
               
            }
        }
        public void Listen()
        {
            try
            {
                byte[] tmp = new byte[1000000];
                while (true)
                {
                    int i = socket.Receive(tmp);
                    Parse(tmp.Take(i).ToArray());
                }
            }
            catch
            {
                MainController.Instance.Disconnect(this);
            }
        }
        void Parse(byte[] bytes)
        {
            Messages m = MainController.Instance.serializer.Deserialize(bytes);
            m.Id = this.Id;
            switch (m.GetType().Name)
            {
              
                case "RegistrationMessage":
                    this.nick = (m as RegistrationMessage).nick;
                    MainController.Instance.logger.Log(nick + " вошел");
                    Send(new RegistrationResultMessage(this.Id));
                    break;
                case "SearchMessage":
                    this.search = true;
                    this.gameField = (m as SearchMessage).field;
                    SearchGame();
                    break;
                case "ShootMessage":
                    Step(m);
                    break;

            }
        }

        public event Action<Messages> Step = (x) => { };
        public event Action SearchGame = () => { };

    }
}
