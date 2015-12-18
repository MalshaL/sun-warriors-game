using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections ;


namespace SunWarriorsGame
{
    public class Node : IComparable {

        public int g;
		public int h;
		public int x;
		public int y;
		private Node goalNode;
		public Node parentNode;
		private int gCost;

        //Innisiaze node class
        public Node(Node parentNode, Node goalNode, int gCost, int x, int y) {

            this.parentNode = parentNode;
            this.goalNode = goalNode;
            this.gCost = gCost;
            this.x = x;
            this.y = y;
            InitNode();
        }

        //calculate total path cost
        public int totalCost {
			get {
				return g+h;
			}
			set {
				totalCost = value;
			}
		}
		
	
        //set g & h for node
		private void InitNode() {
			this.g = (parentNode!=null)? this.parentNode.g + gCost:gCost;
			this.h = (goalNode!=null)? (int) Euclidean_H():0;
		}

		private double Euclidean_H() {
			double xd = this.x - this.goalNode .x ;
			double yd = this.y - this.goalNode .y ;
			return Math.Sqrt((xd*xd) + (yd*yd));
		}
		
		public int CompareTo(object obj) {
			
			Node n = (Node) obj;
			int cFactor = this.totalCost - n.totalCost ;
			return cFactor;
		}

		public bool isMatch(Node n) {
			if (n!=null)
				return (x==n.x && y==n.y);
			else
				return false;
		}

        Map map = new Map();
		public ArrayList GetSuccessors() {
			ArrayList successors = new ArrayList ();

			for (int xd=-1;xd<=1;xd++)
			{
				for (int yd=-1;yd<=1;yd++)
				{
					if (map.getMap (x+xd,y+yd) !=-1)
					{
						Node n = new Node (this,this.goalNode ,map.getMap (x+xd,y+yd) ,x+xd,y+yd);
						if (!n.isMatch (this.parentNode) && !n.isMatch (this))
                            successors.Add (n);

					}
				}
			}
			return successors;
		}
	}
}

