using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    public class Brick : GridEntity
    {
        int damageLevel = 0;
        public Brick(Point p) : base(p)
        {
            this.setName("B  ");
            //this.setDamageLevel(damageLevel);
        }

        public void setDamageLevel(int level)
        {
            this.damageLevel = level;
        }

        public int getDamageLevel()
        {
            return damageLevel;
        }
    }
}
