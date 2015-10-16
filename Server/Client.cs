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

            switch (m.GetType().Name)
            {
                case "MoveMessage":
                    Step((m as MoveMessage).move, Id);
                    break;
                case "RequestAllRoomsMessage":
                    RequestAllRooms(Id);
                    break;
                case "CreateNewRoom":
                    NewRoom((m as CreateNewRoom).objects, (m as CreateNewRoom).name);
                    break;
                case "RegistrationMessage":
                    if (nick == "")
                    {
                        nick = (m as RegistrationMessage).nick;
                    }
                    break;
                case "ConnectToRoomMessage":
                    ConnectToRoom(Id, (m as ConnectToRoomMessage).roomName);
                    break;

            }
        }

        public event Action<Move, int> Step;
        public event Action<GameObject[,], string> NewRoom;
        public event Action<Move, int> Piy;
        public event Action<int> RequestAllRooms;
        public event Action<int, string> ConnectToRoom;
       

    }
}
