using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    public class Stone : GridEntity
    {
        public Stone(Point p) : base(p)
        {
            this.setName("stone");
        }
    }
}
