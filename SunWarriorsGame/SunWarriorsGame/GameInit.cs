using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client;
using Tank_Game;
using TankClient;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SunWarriorsGame
{
    class GameInit
    {
        public static void StartGame()
        {
            Thread XNA = new Thread(new ThreadStart(StartXNA));
            XNA.Start();
            Thread gui = new Thread(new ThreadStart(StartGUI));
            gui.Start();
        }

        static void StartXNA()
        {
            Game1 game = new Game1();
            game.Run();
        }

        static void StartGUI()
        {
            ConnectClient client = ConnectClient.GetClient();
            Application.EnableVisualStyles();
            client.recivePool();
            Application.Run(new GUI());
        }
    }
}
