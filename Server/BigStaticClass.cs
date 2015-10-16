﻿using Project2;
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

        static int id;

        public static void Registration(Socket socket)
        {
            
            Client client = new Client(socket, id++);
            clients.Add(client);

            client.NewRoom += NewRoom;
            client.RequestAllRooms += AllRooms;
            client.ConnectToRoom += StartGame;

            client.Send(new RegistrationResultMessage(client.Id));
            Task task = new Task(client.Listen);
            task.Start();

        }


        static void NewRoom(GameObject[,] objects, string name)
        {
            Game game = new Game(name, objects);

            games.Add(game);
        }

        static void AllRooms(int i)
        {
            List<RoomInfo> rooms = games.Select(x => new RoomInfo(x.Name, x.Count)).ToList();

            clients.First(x => x.Id == i).Send(new ListOfRoomsMessage(rooms));
        }

        static void StartGame(int i, string room)
        {

        }

    }

}
