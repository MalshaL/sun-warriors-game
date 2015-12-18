using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections ;
using Tank_Game;

namespace SunWarriorsGame
{
    public class Map
    {
        public Map map = new Map();
        int[,] Mapdata;
		
        GameEngine gameEngine = GameEngine.GetGameEngine();
        public int coinposiionx;
        public int coinposiiony;
        public void generateMap()
        {
           
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (gameEngine.getGrid()[i,j].getName() == "default"){
                           Mapdata[i,j] =1;
                    } else if (gameEngine.getGrid()[i,j].getName() == "water"){
                            Mapdata[i,j] =-1;
                    } else if (gameEngine.getGrid()[i,j].getName() == "stone"){
                              Mapdata[i,j] =-1;
                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "brick")
                    {
                        Mapdata[i, j] = -1;
                    } else if (gameEngine.getGrid()[i,j].getName() == "coin"){
                              Mapdata[i,j] =1;
                              coinposiionx=i;
                              coinposiiony=j;
                    }
                    else if (gameEngine.getGrid()[i, j].getName() == "lifePack")
                    {
                        Mapdata[i, j] = 1;
                        coinposiionx = i;
                        coinposiiony = j;
            }
        
           }
        }
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

