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
            waitForm = new WaitForm();

            gameController = new GameController();
            fieldController = new SettingGame();

            var i = gameForm.Handle;
            i = waitForm.Handle;
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
            if (state == ClientState.Offline)
            {
                state = ClientState.Online;
                gameForm.Invoke(new Action<Form>(x => x.Show()), gameForm);
                Context.MainForm = gameForm;
                regForm.Invoke(new Action<Form>(x => x.Hide()), regForm);

                fieldController.Attach(gameForm);
            }
        }
        public static void Disconnected()
        {
            if (state != ClientState.Offline)
            {
                state = ClientState.Offline;
                var old = Context.MainForm;
                Context.MainForm = regForm;
                Context.MainForm.Invoke(new Action<Form>(x => x.Show()), regForm);
                old.Invoke(new Action<Form>(x => x.Hide()), old);

            }

        }
        public static void SendGame(SearchMessage message)
        {
            if (state == ClientState.Online)
            {
                state = ClientState.Waiting;

                serverManager.SendMessage(message);
                waitForm.Invoke(new Action<Form>(x => x.ShowDialog()), waitForm);
            }
        }
        public static void StartGame(StartGameMessage message)
        {
            if (state == ClientState.Waiting)
            {
                state = ClientState.Gaming;

                //gameForm.ReDraw(message.you, message.enemy, message.turn);

                fieldController.Detach();
                gameController.Attach(gameForm);
                gameController.NewField(message.you, message.enemy, message.turn);

                waitForm.Invoke(new Action<Form>(x => x.Hide()), waitForm);
            }
        }
        public static void NewGameMessage(FieldStateMessage message)
        {
            //gameForm.ReDraw(message.you, message.enemy, message.turn);

            gameController.NewField(message.you, message.enemy, message.turn);
        }
        public static void EndOfGame(EndOfGameMessage message)
        {
            string mes = (message.win) ? "Вы выиграли" : "Вы проиграли";
            MessageBox.Show(mes);
        }

        private static RegForm regForm;
        private static GameForm gameForm;
        private WaitForm waitForm;
        private static GameController gameController;
        private static SettingGame fieldController;
        public static ClientState state;
        public static ServerManager serverManager { get; private set; }
        public static ApplicationContext Context;
    }

   
    
}
