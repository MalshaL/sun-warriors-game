using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    public class Water : GridEntity
    {
        public Water(Point p) : base(p)
        {
            this.setName("water");
        }
    }
}
