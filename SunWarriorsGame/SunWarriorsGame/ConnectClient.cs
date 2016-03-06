using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
//using ActiveXClientXNA.Entity_Classes;
using System.Collections;
using Tank_Game;

namespace TankClient
{
    class ConnectClient
    {
        GameEngine gameEngine = GameEngine.GetGameEngine();
        private String msg = "";
        Stack message_queue = new Stack();        //stack to keep track of the messages that are received

        public String getMsg()
        {

            String temp = (String)message_queue.Peek();               //Getter for recently received message
            //Console.WriteLine(temp.getMsg());
            return temp;
        }

        public event EventHandler messageRecieved;
        private void notify(String message)
        {
            if (messageRecieved != null)
            {
                Console.WriteLine("hg");
                messageRecieved(this, null);
            }
            else
            {
                Console.WriteLine("hgsd");
            }
        }
        public ConnectClient()
        {
            message_queue.Push("------Test ActiveX Client-------");
            Thread tt = new Thread(receive);                       //Creating a thread to listen for messages
            tt.Start();

        }
        public void SendData(String command)     //sending messages to server
        {
            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
            //Client has Started at this point

            clientSocket.Connect("127.0.0.1", 6000);               //localhost at port 6000         //192.168.1.100

            //Client Socket Program - Server Connected at this point

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(command);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            //message sent at this point

            serverStream.Close();       //closing the stream and socket
            clientSocket.Close();

        }

        public void receive()               //recceieng messages from the server 
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("127.0.0.1");   //localhost            IP.Any()

                while (true)
                {
                    TcpListener myList = new TcpListener(ipAd, 7000);   //initializing the listner at port 7000

                    myList.Start();

                    Socket s = myList.AcceptSocket();

                    byte[] b = new byte[1024];
                    int k = s.Receive(b);
                    for (int i = 0; i < k; i++)
                    {
                        msg = msg + Convert.ToChar(b[i]);            //creating the message by character by character
                    }
                    notify(msg);
                    message_queue.Push(msg);     //creating a new message object andd pushing to the message stack
                    s.Close();
                    myList.Stop();
                    msg = "";                //clearing message
                    gameEngine.handleMessage((String)message_queue.Pop());
                }
            }
            catch (Exception e)
            {
                msg = "Error";
            }
            Console.ReadLine();
        }

    }
}
