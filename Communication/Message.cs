using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    [Serializable]
    public abstract class Message
    {
        public int Id { get; set; }
    }

    [Serializable]
    public class RegistrationMessage : Message
    {
        public RegistrationMessage(string nick)
        {
            this.nick = nick;
        }

        public string nick;

    }
    [Serializable]
    public class RegistrationResultMessage : Message
    {
        public RegistrationResultMessage()
        {
            
        }
    }

    [Serializable]
    public class CreateField : Message
    {

    }

    [Serializable]
    public class SearchMessage : Message
    {
        public SearchMessage()
        {

        }
    }
    [Serializable]
    public class StartGameMessage : Message
    {
        public StartGameMessage()
        {

        }
    }

    [Serializable]
    public class ShootMessage : Message
    {

    }
    [Serializable]
    public class FieldStateMessage : Message
    {
        public FieldStateMessage(GameObject[,] objects)
        {
            this.objects = objects;
        }

        public GameObject[,] objects;
    }
    [Serializable]

    [Serializable]
    public enum GameObject
    {
        None, Hurt, Dead, Point
    }
}
