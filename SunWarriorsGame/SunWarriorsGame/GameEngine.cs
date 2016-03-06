using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SunWarriorsGame;
using System.Timers;

namespace Tank_Game
{
    public class GameEngine
    {
        #region Variables

        private static GameEngine gameEngine;
        private static object syncRoot = new object();
        //private static Game1 game;
        private Player me;
        private string playerName;      //player name (client)
        private int playerNum;          //player number
        private Point startLoc;         //start location of player
        private int startDir;            //start direction of player
        private GridEntity[,] grid;     //the grid
        private int mapSize;            //no of rows and columns in the grid
        private List<Player> playerList;
        private List<String> playerNames;
        private List<Point> brickLocations;
        //private List<GridEntity> stoneLocations;
        //private List<GridEntity> waterLocations;
        //private List<GridEntity> coinLocations;
        private List<char> msgTypes;
        bool isFirstDecode = true;
        private String otherMessage = "";
        private int players = 0;

        #endregion

        public GameEngine()             
        {
            msgTypes = new List<char>();
            msgTypes.Add('S');
            msgTypes.Add('I');
            msgTypes.Add('G');
            msgTypes.Add('C');
            msgTypes.Add('L');
            //generateGrid(mapDetails);        
        }

        public static GameEngine GetGameEngine()        //singleton
        {
            if (gameEngine == null)
		    {
			    lock(syncRoot)
			    {
				    if (gameEngine == null)
				    { gameEngine = new GameEngine();	}
			    }            
		    }
		    return gameEngine;
        }

        public void handleMessage(String message)
        {
            Console.WriteLine(message);
            if (message[1] == ':')
            {
                char firstChar = message[0];
                //if (message.ElementAt(message.Length - 1) == '?')
                //{
                //    message = message.Substring(0, message.Length - 2);
                //    Console.WriteLine("????????hereeee");
                //}
                //else
                //{
                //    message = message.Substring(0, message.Length - 1);
                //    Console.WriteLine("########hereeeeeeee");
                //}
                message = message.Substring(0, message.LastIndexOf("#"));
                if (msgTypes.Contains(firstChar))
                {
                    if (firstChar == 'S')
                    {
                        initialize(message);
                    }
                    if (firstChar == 'I')
                    {
                        generateGrid(message);
                    }
                    if (firstChar == 'G')
                    {
                        updateMap(message);
                    }
                    if (firstChar == 'C')
                    {
                        handleCoins(message, grid);
                    }
                    if (firstChar == 'L')
                    {
                        handleLifePacks(message, grid);
                    }
                }
            }
            else
            {
                message = message.Substring(0, message.LastIndexOf("#"));
                setOtherMsg(message);
            }
        }

        private void generateGrid(string map)
        {
            this.mapSize = 10;
            brickLocations = new List<Point>();
            //stoneLocations = new List<Stone>();
            //waterLocations = new List<Water>();
            //coinLocations = new List<CoinPile>();
            grid = new GridEntity[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    grid[i, j] = new GridEntity(new Point(j, i));
                }
                //Console.WriteLine();
            }
            setLocations(map, grid);
            displayGrid(grid);
            //Program2.Main();
        }

        private void setLocations(string map, GridEntity[,] grid)
        {
            string[] splittedValues = map.Split(':');
            playerName = splittedValues[1];
            playerNum = int.Parse(playerName.Substring(1));
            setOtherMsg("You are Player " + playerNum + "in  " + getPlayerColor());
            setLocationLists(splittedValues[2], "brick", grid);
            setLocationLists(splittedValues[3], "stone", grid);
            setLocationLists(splittedValues[4], "water", grid);
        }

        private String getPlayerColor()
        {
            switch (playerNum)
            {
                case 0:
                    return "Red";
                case 1:
                    return "Purple";
                case 2:
                    return "Blue";
                case 3:
                    return "Brown";
                case 4:
                    return "Green";
                default:
                    return "";
            }
        }

        private void setLocationLists(string values, string type, GridEntity[,] grid)
        {
            Point p;
            string[] tokens = values.Split(';');
            for (int i = 0; i < tokens.Length; i++)
            {
                p = new Point(int.Parse(tokens[i].Split(',')[0]), int.Parse(tokens[i].Split(',')[1]));
                try
                {
                    if (type.Equals("brick"))
                    {
                        grid[p.Y, p.X] = new Brick(p);
                        brickLocations.Add(p);
                    }
                    else if (type.Equals("stone"))
                    {
                        grid[p.Y, p.X] = new Stone(p);                        
                        //locations.Add(new Stone(p));
                    }
                    else if (type.Equals("water"))
                    {
                        grid[p.Y, p.X] = new Water(p);                        
                        //locations.Add(new Water(p));
                    }
                    
                }
                catch (NullReferenceException)
                {
                    //Console.WriteLine("exception");
                }

            }
        }

        private void initialize(string starter)
        {
            playerList = new List<Player>();
            playerNames = new List<String>();
            starter = starter.Substring(2);
            string[] splittedValues = starter.Split(':');
            foreach (String s in splittedValues)
            {
                string[] tokens = s.Split(';');
                string loc = tokens[1];
                startLoc = new Point(int.Parse(loc.Split(',')[0]), int.Parse(loc.Split(',')[1]));
                if (tokens[0].Equals(playerName))           //me
                {
                    startDir = int.Parse(tokens[2]);
                    me = new MyPlayer(startLoc, playerName, startDir);
                    playerNames.Add(playerName);
                }
                else
                {
                    Player player = new Player(startLoc, tokens[0], int.Parse(tokens[2]));
                    playerList.Add(player);
                    players += 1;
                    playerNames.Add(tokens[0]);
                }
            }
        }

        private void updateMap(string msg)
        {
            msg = msg.Substring(2);
            string[] splittedValues = msg.Split(':');
            for (int i = 0; i < splittedValues.Length - 1; i++)             //for each player's details
                {
                    string[] tokens = splittedValues[i].Split(';');
                    String name = tokens[0];
                    //int num = int.Parse(name.Substring(1).ToString());
                    Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
                    if (!name.Equals(playerName))                           //not me
                    {
                        if (playerNames.Contains(name))                     //player already in game
                        {
                            Player pl = getPlayerFromList(name);
                            Point k = new Point(pl.getPrevP().X, pl.getPrevP().Y);
                            if (grid[k.Y, k.X].getName() == pl.getName())
                            {
                                grid[k.Y, k.X] = new GridEntity(k);
                            }
                            pl.updatePlayer(pl, p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                            grid[p.Y, p.X] = pl;
                        }
                        else                                                //new player
                        {
                            Player player = new Player(p, tokens[0], int.Parse(tokens[2]));
                            player.updatePlayer(player, p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                            grid[p.Y, p.X] = player;
                        }
                    }
                    else                                                     //me
                    {
                        if (isFirstDecode)                                   //my first decode
                        {
                            me.updatePlayer(me, p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                            grid[p.Y, p.X] = me;
                            isFirstDecode = false;
                        }
                        else                                                  //me already in game
                        {
                            Point k = new Point(me.getPrevP().X, me.getPrevP().Y);
                            if (grid[k.Y, k.X].getName() == me.getName())
                            {
                                grid[k.Y, k.X] = new GridEntity(k);
                            }
                            me.updatePlayer(me, p, int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
                            grid[p.Y, p.X] = me;
                        }
                    }
                }
            updateBricks(splittedValues[splittedValues.Length - 1], grid);
            displayGrid(grid);
            //game.setGrid(grid);
        }

        public Player getPlayerFromList(String name)
        {
            foreach (Player p in playerList){
                if (p.getName().Equals(name)){
                    return p;
                }
            }
            return null;
        }

        public void displayGrid(GridEntity[,] grid)
        {
            Console.WriteLine();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Console.Write(grid[i, j].getName());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void updateBricks(string splittedValues, GridEntity[,] grid)
        {   
            string[] bricks = splittedValues.Split(';');
            for (int i = 0; i < bricks.Length; i++)
            {
                string[] values = bricks[i].Split(',');
                Point l = new Point((int.Parse(values[0])), (int.Parse(values[1])));
                grid[(int.Parse(values[1])), (int.Parse(values[0]))].setDamageLevel(int.Parse(values[2]));
            }
        }

        private void handleCoins(string msg, GridEntity[,] grid)
        {
            string[] tokens = msg.Split(':');
            Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
            grid[p.Y, p.X] = new CoinPile(p, int.Parse(tokens[2]), 0, int.Parse(tokens[3]));
            InitTimer(p, int.Parse(tokens[2]), grid);
            //coinLocations.Add(p);
            //CoinPile coins = new CoinPile(p, int.Parse(tokens[2]), 0, int.Parse(tokens[3]));
        }

        private static void InitTimer(Point p, int lifeTime, GridEntity[,] grid)
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += (source, e) => OnTimedEvent(source, p, grid);
            aTimer.Interval = lifeTime;
            aTimer.Start();
        }

        private static void OnTimedEvent(object source, Point p, GridEntity[,] grid)
        {
            grid[p.Y, p.X] = new GridEntity(p);
        }

        private void handleLifePacks(string msg, GridEntity[,] grid)
        {
            string[] tokens = msg.Split(':');
            Point p = new Point(int.Parse(tokens[1].Split(',')[0]), int.Parse(tokens[1].Split(',')[1]));
            grid[p.Y, p.X] = new LifePack(p, int.Parse(tokens[2]), 0);
            InitTimer(p, int.Parse(tokens[2]), grid);
            //coinLocations.Add(p);
            //LifePack lifepack = new LifePack(p, int.Parse(tokens[2]), 0);
        }

        public GridEntity[,] getGrid()
        {
            return grid;
        }

        public void setOtherMsg(String msg){
            msg = msg.Replace("_", " ");
            otherMessage = msg;
        }

        public String getOtherMsg()
        {
            return otherMessage;
        }

        private void InitTimer2()
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            aTimer.Interval = 2000;
            aTimer.Start();
        }

        private void OnTimedEvent2(object source, ElapsedEventArgs e)
        {
            setOtherMsg("");
        }

        public Player getMyTank()
        {
            return me;
        }
    }
}
