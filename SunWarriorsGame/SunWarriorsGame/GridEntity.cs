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
        private int direction;
        private int damageLevel;
        private int pointsEarned = 0;
        private int coins = 0;
        private int health = 0;

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

        public void setDirection(int d)
        {
            this.direction = d;
        }

        public float getDirection()
        {
            switch (direction)
            {
                case 0:
                    return 0;
                case 1:
                    return 90;
                case 2:
                    return 180;
                case 3:
                    return 270;
                default:
                    return 0;
            }
        }

        public void setDamageLevel(int level)
        {
            this.damageLevel = level;
        }

        public int getDamageLevel()
        {
            return damageLevel;
        }

        public void setPoints(int n)
        {
            this.pointsEarned = n;
        }

        public int getPoints()
        {
            return pointsEarned;
        }

        public void setCoins(int n)
        {
            this.coins = n;
        }

        public int getCoins()
        {
            return coins;
        }

        public void setHealth(int n)
        {
            this.health = n;
        }

        public int getHealth()
        {
            return health;
        }
    }
}
