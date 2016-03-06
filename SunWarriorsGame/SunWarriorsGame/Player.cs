using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    public class Player : GridEntity
    {
        private string name = "";
        //private string ip = "";
       // private int port = -1;
        //private Point startP;
        private Point currentP;
        private Point prevP;
        private int direction = 0;
        private int shot = 0;
        private bool isAlive = true;
        //private bool invalidCell = false;
       // private DateTime updatedTime;
       // private int index = -1;

        public Player(Point currentP, string name, int direction) : base(currentP)
        {
            setName(name);
            setCurrentP(currentP);
            setPosition(currentP);
            setDirection(direction);
            setPrevP(currentP);
        }

        public void updatePlayer(Player p, Point currentP, int direction, int isShot, int health, int coins, int points)
        {
            p.setCurrentP(currentP);
            p.setPosition(currentP);
            p.setDirection(direction);
            p.setIsShot(isShot);
            p.setHealth(health);
            p.setCoins(coins);
            p.setPoints(points);
            p.setPrevP(currentP);
        }

      

        public void setCurrentP(Point p)
        {
            this.currentP = p;
        }

        public Point getCurrentP()
        {
            return currentP;
        }

        public void setPrevP(Point p)
        {
            this.prevP = p;
        }

        public Point getPrevP()
        {
            return prevP;
        }

        public void setIsShot(int n)
        {
            this.shot = n;
        }

        public int getIsShot()
        {
            return shot;
        }

        public void setIsAlive(bool n)
        {
            this.isAlive = n;
        }

        public bool getIsAlive()
        {
            return isAlive;
        }
    }
}
