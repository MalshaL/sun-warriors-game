using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunWarriorsGame
{
    class NodeComparer
    {
        public int Compare(object x, object y)
        {
            return ((Node)x).totalCost - ((Node)y).totalCost;
        }
    }
}
