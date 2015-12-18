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

        
    }
}
