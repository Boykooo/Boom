using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            Application.Run(new MainForm());
        }

        public static ClientState state;
    }
}
