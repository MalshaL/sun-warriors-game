using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tank_Game;

namespace TankClient
{
    class ConnectClient
    {
        GameEngine gameEngine = new GameEngine();
        //Inisialize client to communicate with server
        private TcpClient client;

        // TcpListner to listen to incomming clients      
        private TcpListener listener;


        //Network stream to send & recive data
        private NetworkStream clientStream;
        private NetworkStream serverStream;

        // BinaryWriter to client
        private BinaryWriter writer;

        //private string clientmsg = null;
        public string reply = "";
        private Thread threadsend = null;
        private Thread threadrecive = null;
        // String clientmag = Console.ReadLine();
        Move command = new Move();


        public void ReceiveData()
        {

            bool errorOcurred = false;

            //Socket for listning
            Socket clientAccept = null;

            try
            {


                //create new client socket
                this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);

                //Starts listening
                this.listener.Start();

                //Establishing connection
                while (true)
                {
                    //connection is connected socket
                    clientAccept = listener.AcceptSocket();

                    //check for connection
                    if (clientAccept.Connected)
                    {
                        //Console.WriteLine("Client connected sussesfully");

                        //To read from socket create NetworkStream object associated with socket
                        this.serverStream = new NetworkStream(clientAccept);


                        SocketAddress ipAd = clientAccept.RemoteEndPoint.Serialize();
                        int port = 100;

                        string address = clientAccept.RemoteEndPoint.ToString();
                        List<Byte> clientInput = new List<byte>();

                        int asw = 0;
                        while (asw != -1)
                        {
                            asw = this.serverStream.ReadByte();
                            clientInput.Add((Byte)asw);
                            //Console.WriteLine("STILL RUNNING");
                        }

                        //  Console.WriteLine("end");
                        reply = Encoding.UTF8.GetString(clientInput.ToArray());
                        string r = reply.Split(';')[0];
                        // Console.WriteLine("______________________"+r);
                        command.catchSend(r);
                        this.serverStream.Close();
                        // Console.WriteLine("CONNECTION CLOSE");
                        Thread.Sleep(10);
                        string ip = address.Substring(0, address.IndexOf(":"));


                        try
                        {
                            string ss = reply.Substring(0, reply.IndexOf(";"));
                            port = Convert.ToInt32(ss);
                        }

                        catch (Exception)
                        {
                            port = 100;
                        }
                        Console.WriteLine(ip + ": " + reply);
                        gameEngine.handleMessage(reply);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (RECEIVING) Failed! \n " + e.Message);
                errorOcurred = true;
            }
            finally
            {
                if (clientAccept != null)
                    if (clientAccept.Connected)
                        clientAccept.Close();
                if (errorOcurred)
                    this.ReceiveData();
            }
        }



        public void SendData(string msg)
        {

            //Opening the connection
            this.client = new TcpClient();


            try
            {

                //Console.WriteLine("Client starting.....");
                this.client.Connect(IPAddress.Parse("127.0.0.1"), 6000);

                if (this.client.Connected)
                {
                    //To write to the socket
                    this.clientStream = client.GetStream();

                    //Create objects for writing across stream
                    this.writer = new BinaryWriter(clientStream);
                    //Console.WriteLine("Write JOIN# to start game");
                    //String clientmag = Console.ReadLine();
                    //clientmag = "I want to send";
                    Byte[] tempStr = Encoding.ASCII.GetBytes(msg);

                    //writing to the port                
                    this.writer.Write(tempStr);
                    //Console.WriteLine("\t Data: " + msg + " is written to " + IPAddress.Parse("127.0.0.1") + " on " + 6000);
                    this.writer.Close();
                    this.clientStream.Close();
                    // Console.WriteLine("send is close");
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Communication (WRITING) to " + IPAddress.Parse("127.0.0.1") + " on " + 6000 + "Failed! \n " + e.Message);
            }
            finally
            {
                this.client.Close();
            }
        }




        public void recivePool()
        {

            threadrecive = new Thread(new ThreadStart(ReceiveData));
            threadrecive.Start();
        }
        public void sendPool()
        {

            //threadsend = new Thread(new ThreadStart(SendData());
            threadsend.Start();
            //Console.WriteLine("Send pool is still running");
        }















    }


}

