using Project2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
   
    public class Game
    {
        class Gamer
        {
            public Client client;
            public int score;
            public Point position;

            public Gamer(Client client)
            {
                this.client = client;
            }
        }

        List<Gamer> gamers;

        public int Count { get; private set; }
        public string Name { get; private set; }

        GameObject[,] objects;
        public Game(string name, GameObject[,] objects)
        {
            gamers = new List<Gamer>();

            Name = name;
            this.objects = objects;
        }

        void Step(Message message)
        {

        }

        public void AddGamer(Client c)
        {
            Gamer g = new Gamer(c);

            g.position = new Point(10, 10);

            gamers.Add(g);


        }

        void StateOfRoom()
        {
            foreach (var g in gamers)
            {
                g.client.Send( new FieldStateMessage(objects, gamers.Select( x=> new GamerInfo(x.score, x.position, x.client.nick)).ToList()));
            }
        }
    }


}
