using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Client.Game;
using Project2;
using System.Net;
using System.Threading;

namespace Client
{
    enum ClientState
    {
        Offline, Online, Waiting, Gaming
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            state = ClientState.Offline;
            serverManager = new ServerManager();
            regForm = new RegForm();
            gameForm = new GameForm();
            var i = gameForm.Handle;
            Context = new ApplicationContext(regForm);
            Application.Run(Context);

        }
        public static void TryConnect(string ip, string nick)
        {
            int port = 8888;

            IPAddress adress = IPAddress.Parse(ip);

            serverManager.Start(adress, port);
            serverManager.SendMessage(new RegistrationMessage(nick));
        }
        public static void Connected()
        {
            state = ClientState.Online;
            gameForm.Invoke(new Action<Form>(x => x.Show()), gameForm);
            Context.MainForm = gameForm;
            regForm.Invoke(new Action<Form>(x => x.Hide()), regForm);         
        }
        public static void Disconnected()
        {
            state = ClientState.Offline;
            var old = Context.MainForm;
            Context.MainForm = regForm;
            Context.MainForm.Invoke(new Action<Form>(x => x.Show()), regForm);
            old.Invoke(new Action<Form>(x => x.Hide()), old);
        }
        public static void SendGame(SearchMessage message)
        {
            state = ClientState.Waiting;

            serverManager.SendMessage(message);
        }
        public static void StartGame(StartGameMessage message)
        {
            state = ClientState.Gaming;

            gameForm.ReDraw(message.you, message.enemy, message.turn);
        }
        public static void NewGameMessage(FieldStateMessage message)
        {
            gameForm.ReDraw(message.you, message.enemy, message.turn);
        }

        static RegForm regForm;
        static GameForm gameForm;
        public static ClientState state;
        public static ServerManager serverManager { get; private set; }
        public static ApplicationContext Context;
    }

   
    
}
