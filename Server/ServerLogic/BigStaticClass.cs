using Project2;
using Server.ServerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class BigStaticClass
    {
        public static List<Client> clients = new List<Client>();
        public static List<Game> games = new List<Game>();
        public static MessageSerializer serializer = new MessageSerializer();
        public static ILogger logger = new ConsoleLogger();
        static int id;

        public static void Registration(Socket socket)
        {
            
            Client client = new Client(socket, id++);
            clients.Add(client);

            client.Send(new RegistrationResultMessage(client.Id));
            Task task = new Task(client.Listen);
            task.Start();

        }

        public static void TryCreateGame()
        {
            List<Client> tmp = clients.Where(x => x.search).ToList();
            if (tmp.Count > 1)
            {
                if (tmp.Count % 2 == 1)
                {
                    tmp.RemoveAt(tmp.Count - 1);
                }
                for (int i = 0; i < tmp.Count; i += 2)
                {
                    Client[] newGamers = new Client[]{tmp[i], tmp[i+1]};
                    Game game = new Game(newGamers);
                }
            }
        }

        static void StartGame(int i)
        {

        }
        public static void Disconnect(Client client)
        {
            clients.Remove(client);
        }

    }

}
