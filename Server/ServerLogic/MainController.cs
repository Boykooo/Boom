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
    public class MainController
    {

        protected MainController()
        {

        }

        static MainController instance = new MainController();
        public static MainController Instance
        {
            get
            {
                return instance;
            }
        }

        public List<Client> clients = new List<Client>();
        public List<Game> games = new List<Game>();
        public MessageSerializer serializer = new MessageSerializer();
        public ILogger logger = new ConsoleLogger();
        private object lck = new object();

        private int id;

        public void Registration(Socket socket)
        {
            lock (lck)
            {
                Client client = new Client(socket, id++);
                client.SearchGame += TryCreateGame;

                clients.Add(client);

                client.Send(new RegistrationResultMessage(client.Id));
            }

        }
        public void TryCreateGame()
        {
            lock (lck)
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
                        Client[] newGamers = new Client[] { tmp[i], tmp[i + 1] };
                        newGamers[0].search = newGamers[1].search = false;
                        Game game = new Game(newGamers);
                    }
                }
            }
        }     
        public void Disconnect(Client client)
        {
            lock (lck)
            {
                clients.Remove(client);

                logger.Log(client.nick + " вышел");
            }
        }
        public void GameOver(Game game)
        {
            lock (lck)
            {
                games.Remove(game);
            }
        }

    }

}
