using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Tank_Game;
using System.Drawing;

namespace SunWarriorsGame
{
    public class Map
    {
        
        int[,] Mapdata;
        int[,] mapArray = new int[10, 10];

        GameEngine gameEngine = GameEngine.GetGameEngine();
        public int coinposiionx;
        public int coinposiiony;
        public int myi ;
        public int myj;
        List<int> findi = new List<int>();
        List<int> findj = new List<int>();
        public int goi = 0;
        public int goj = 0;
        private int go = 0;
        Point p;
        int nowi;
        int nowj;



        public void generateMap()
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (gameEngine.getGrid()[i, j].getName() == "default")
                    {
                        mapArray[i, j] = 1;
                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "water")
                    {
                        mapArray[i, j] = 0;
                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "stone")
                    {
                        mapArray[i, j] = 0;
                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "brick")
                    {
                        mapArray[i, j] = 0;
                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "coin")
                    {
                        mapArray[i, j] = 20;
                        findi.Add(i);
                        findj.Add(j);

                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "lifePack")
                    {
                        mapArray[i, j] = 30;
                        findi.Add(i);
                        findj.Add(j);
                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "P0")
                    {
                        mapArray[i, j] = 5;
                        p = gameEngine.getMyTank().getPosition();
                        myi = p.Y;
                        myj = p.X;
                    }

                }
            }
        }

        public void minPoint()
        {
            int ival = 0;
            int jval = 0;
            int min = 100;
            for (int x = 0; x < findi.Count; x++)
            {
                ival = Math.Abs(myi - findi[x]);
                jval = Math.Abs(myj - findj[x]);

                if (min > ival)
                {
                    min = ival;
                    go = x;
                }

                if (min > jval)
                {
                    min = jval;
                    go = x;
                }

            }
        }

        public void getCoordinate()
        {
            minPoint();
            goi = findi[go];
            goj = findj[go];
        }

        /*
        public void AImove()
        {
            int horizontal = myi - goi;
            int verticle = myj - goj;

            for (int p = 0; p < Math.Abs(horizontal); p++)
            {
                if (horizontal < 0)
                {
                    AIcommand.Add(3);
                }
                else
                {
                    AIcommand.Add(4);
                }
            }

            for (int p = 0; p < Math.Abs(verticle); p++)
            {
                if (horizontal < 0)
                {
                    AIcommand.Add(2);
                }
                else
                {
                    AIcommand.Add(1);
                }
            }
        }
         * */

        public int goRight(int i, int j)
        {
            if (i<9 && mapArray[i + 1, j] == 0)
            {
                int take = change(i, j, 'R');
                return take;
            }
            //nowi--;
            return 4;
        }

        public int goLeft(int i, int j)
        {
            if (i>1 && mapArray[i - 1, j] == 0 )
            {
                int take = change(i, j, 'L');
                return take;
            }
            nowi++;
            return 3;
        }

        public int goUp(int i, int j)
        {
            if (mapArray[i, j - 1] == 0)
            {
                int take = change(i, j, 'U');
                return take;
            }
            nowj--;
            return 1;
        }

        public int goDown(int i, int j)
        {
            if (mapArray[i, j + 1] == 0)
            {
                int take = change(i, j, 'D');
                //return 4;
                return take;
            }
            nowj++;
            return 2;
        }



        public int change(int i, int j, char no)
        {
            if (no == 'R')
            {
                if (j<9 && mapArray[i, j + 1] != 0)  
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else if (no == 'L')
            {
                if (mapArray[i, j + 1] != 0 && j<9)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }

            }
            else if (no == 'D')
            {
                if (mapArray[i - 1, j] != 0 && i>0)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }

            }
            else if (no == 'U')
            {
                if (mapArray[i - 1, j] != 0 && i>0)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
            return 5;

        }
        public void insailize()
        {
            generateMap();
            getCoordinate();

            move();
            
        }

        public int move()
        {
            //goi = 7;
            //goj = 5;

           p = gameEngine.getMyTank().getPosition();
           nowi = p.Y;
          nowj = p.X;
            
            //goi = 0;
            //goj = 2;

            int horizontal = goi - nowi;
            int verticle = goj - nowj;
            //int horizontal = goi;
           // int verticle = goj;

            if (horizontal < 0 && horizontal> -10)
            {
                int command = goLeft(nowi, nowj);
                return command;
            }
            else if (horizontal > 0 && horizontal<10)
            {
                int command = goRight(nowi, nowj);
                return command;
            }

            if (verticle < 0 && verticle >-10)
            {
                int command = goUp(nowi, nowj);
                return command;
            }
            else if (verticle > 0 && verticle < 10)
            {
                int command = goDown(nowi, nowj);
                return command;
            }

            else 
            {
                return 5;
            }

            return 5;
        }



        public int getMap(int x, int y)
        {
            int yMax = Mapdata.GetUpperBound(0);
            int xMax = Mapdata.GetUpperBound(1);
            if (x < 0 || x > xMax)
                return -1;
            else if (y < 0 || y > yMax)
                return -1;
            else
                return Mapdata[y, x];
        }

    }
}

