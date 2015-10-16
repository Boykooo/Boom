using Project2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    public class MessageSerializer
    {
        public byte[] Serialize(Message m)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(stream, m);

            return stream.GetBuffer();
        }

        public Message Deserialize(byte[] bytes)
        {
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);

            return (Message)f.Deserialize(stream);
        }
    }
}
