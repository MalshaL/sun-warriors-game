using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    public class GridEntity
    {
        private string name = "default";
        private Point position;

        public GridEntity(Point p)
        {
            this.setPosition(p);
        }

        public string getName()
        {
            return name;
        }

        public void setName(string n)
        {
            this.name = n;
        }

        public void setPosition(Point value)
        {
            this.position = value;
        }

        public Point getPosition()
        {
            return position;
        }

        public virtual float getDirection()
        {
            return 0;
        }

        public virtual void updatePlayer(GridEntity p, Point currentP, int direction, int isShot, int health, int coins, int points)
        {

        }

        public virtual Point getPrevP()
        {
            return new Point(0, 0);
        }


        public virtual void setCurrentP(Point currentP)
        {
            throw new NotImplementedException();
        }

        public virtual void setDirection(int direction)
        {
            throw new NotImplementedException();
        }

        public virtual void setIsShot(int isShot)
        {
            throw new NotImplementedException();
        }

        public virtual void setHealth(int health)
        {
            throw new NotImplementedException();
        }

        public virtual void setCoins(int coins)
        {
            throw new NotImplementedException();
        }

        public virtual void setPoints(int points)
        {
            throw new NotImplementedException();
        }

        public virtual void setPrevP(Point currentP)
        {
            throw new NotImplementedException();
        }
    }
}
