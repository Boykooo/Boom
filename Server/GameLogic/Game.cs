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
            BigStaticClass.logger.Log(gamers[0].client.nick + " и " + gamers[1].client.nick + " вошли в игру");
            Start();
        }
        void Step(Messages message)
        {
            ShootMessage shoot = message as ShootMessage;

            Gamer first = gamers.First(x => x.client.Id != shoot.Id);
            Gamer second = gamers.First(x => x.client.Id == shoot.Id);

            BigStaticClass.logger.Log("Игрок " + second.client.nick + " сделал ход по координатам " + shoot.x.ToString() + " " + shoot.y.ToString());
            if (second.turn)
            {

                second.turn = first.client.gameField.Shoot(shoot.x, shoot.y);
                first.turn = !second.turn;
                if (second.turn && first.client.gameField.IsGameOver())
                {
                    GameOver();
                }


                StateOfRoom();
            }
        }


        void StateOfRoom()
        {
            for (int i = 0; i < gamers.Length; i++)
            {
                FieldStateMessage message
                    = new FieldStateMessage
                        (
                        gamers[i].client.gameField,
                        gamers[(i + 1) % gamers.Length].client.gameField.GetForEnemy(),
                        gamers[i].turn
                        );

                gamers[i].client.Send(message);
            }
        }

        void Start()
        {
            for (int i = 0; i < gamers.Length; i++)
            {
                StartGameMessage message
                    = new StartGameMessage
                        (
                        gamers[i].client.gameField,
                        gamers[(i + 1) % gamers.Length].client.gameField.GetForEnemy(),
                        gamers[i].turn
                        );

                gamers[i].client.Send(message);
            }
        }
        void GameOver()
        {
            
            for (int i = 0; i < gamers.Length; i++)
            {
                gamers[i].client.Send(new EndOfGameMessage(gamers[i].turn));
            }

            BigStaticClass.logger.Log(
                "Игрок " + gamers.First(x=> x.turn).client.nick 
              + " одержал победу над игроком " + gamers.First( x=> !x.turn).client.nick
              );

            BigStaticClass.GameOver(this);
        }
    }


}
