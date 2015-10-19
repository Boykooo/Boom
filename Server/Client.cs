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


        }

        public string nick;
        public int Id { get; private set; }
        public Socket socket;
        public GameField gameField { get; set; }
        public bool search;

        public void Send(Message message)
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
                Console.WriteLine(e.ToString());
            }
        }
        public void Listen()
        {
            byte[] tmp = new byte[1000000];
            while (true)
            {
                int i = socket.Receive(tmp);

                Parse(tmp.Take(i).ToArray());


            }
        }

        void Parse(byte[] bytes)
        {
            Message m = BigStaticClass.serializer.Deserialize(bytes);
            m.Id = this.Id;
            switch (m.GetType().Name)
            {
              
                case "RegistrationMessage":
                    this.nick = (m as RegistrationMessage).nick;
                    Send(new RegistrationResultMessage(this.Id));
                    break;
                case "SearchMessage":
                    this.search = true;
                    BigStaticClass.TryCreateGame();
                    break;
                case "ShootMessage":
                    if (Step != null)
                        Step(m);
                    break;


            }
        }

        public event Action<Message> Step;
       
       

    }
}
