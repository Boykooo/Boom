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

        struct Gamer
        {
            public bool turn;
            public Client client;

        }

        Gamer[] gamers;
        public Game(Client[] clients)
        {
            gamers = new Gamer[2];
            bool turn = true;
            for (int i = 0; i < gamers.Length; i++)
            {
                gamers[i] = new Gamer();
                gamers[i].turn = turn;
                gamers[i].client = clients[i];

                gamers[i].client.Step += Step;
                turn = !turn;
            }
        }
        void Step(Messages message)
        {
            ShootMessage shoot = message as ShootMessage;

            Gamer first = gamers.First(x => x.client.Id != shoot.Id);
            Gamer second = gamers.First(x => x.client.Id == shoot.Id);
            if (second.turn)
            {

                second.turn = first.client.gameField.Shoot(shoot.x, shoot.y);
                first.turn = !second.turn;
                if (second.turn && first.client.gameField.IsGameOver())
                {
                    for (int i = 0; i < gamers.Length; i++)
                    {
                        gamers[i].client.Send(new EndOfGameMessage(gamers[i].turn));
                    }

                    GameOver();
                }
              

                StateOfRoom();
            }
        }


        void StateOfRoom()
        {
            for (int i = 0; i < gamers.Length; i++)
            {
                FieldStateMessage message = new FieldStateMessage(gamers[i].client.gameField, gamers[(i + 1) % gamers.Length].client.gameField, gamers[i].turn);
                gamers[i].client.Send(message);
            }
        }

        void GameOver()
        {

        }
    }


}
