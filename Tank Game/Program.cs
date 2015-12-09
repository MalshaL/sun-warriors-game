using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Tank_Game;
using TankClient;
using System.Windows.Forms;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectClient client = new ConnectClient();
            Application.EnableVisualStyles();
            client.recivePool();
            Application.Run(new GUI());
            Console.WriteLine("-----------------------Client is starting----------------------");
            Console.WriteLine("---------------------------------------------------------------");

            //GameEngine gameEngine = new GameEngine();
            //string initialMsg = "S:P0;1,1;0#?";
            //string mapDetails = "I:P0:1,1;2,3;3,4:2,5;6,8;7,0:3,1;4,2;6,8#?";
            //string m = "G:P0;0,0;0;0;100;0;0#?";
            //gameEngine.handleMessage(initialMsg);
            //gameEngine.handleMessage(mapDetails);
            //gameEngine.handleMessage(m);
            //Console.ReadLine();
        }
    }
}
