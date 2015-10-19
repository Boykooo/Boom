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

namespace Client
{
    class Server
    {
        Socket socket;
        public void Connect(IPAddress ip, int port, string nick)
        {
            IPEndPoint end = new IPEndPoint(ip, port);
            Socket socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(end);

            Send(new RegistrationMessage(nick));

            Task t = new Task(Listen);
            t.Start();
        }

        public void Send(Message message)
        {
            socket.Send(Serialize(message));
        }

        void Listen()
        {
            byte[] tmp = new byte[100000];

            while(true)
            {
                int i = socket.Receive(tmp);

                Message m = Deserialize(tmp.Take(i).ToArray());

                switch (m.GetType().Name)
                {

                }
            }
        }

        byte[] Serialize(Message m)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(stream, m);

            return stream.GetBuffer();
        }

        Message Deserialize(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BinaryFormatter b = new BinaryFormatter();

            return (Message)b.Deserialize(stream);
        }
    }
}
