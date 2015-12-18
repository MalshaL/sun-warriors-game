using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tank_Game;
using TankClient;

namespace SunWarriorsGame
{
    class AIController
    {
        ConnectClient connect = new ConnectClient();
        ArrayList SolutionPathList = new ArrayList();


        public void startAI()
        {
            
            // start moving
            int genNo = 0;
            for (int i = 0; i < randomArray.Length; i++)
            {
                AImove(randomArray[i]);
                Thread.Sleep(3000);
            }
        }

            public void findPath(){
                Map gameMap = new Map();
                gameMap.generateMap();
                ArrayList SolutionPathList = new ArrayList();

                //Create a node containing the goal state node_goal
                Node node_goal = new Node(null, null, 1, 15, 15);

                //Create a node containing the start state node_start
                Node node_start = new Node(null, node_goal, 1, 0, 0);

                //creating sorted lists to keep path nodes
                SortedCostNodeList OPEN = new SortedCostNodeList();
                SortedCostNodeList CLOSED = new SortedCostNodeList();

                //start game by pushing start node to open
                OPEN.push(node_start);

                //while the OPEN list is not empty
                while (OPEN.Count > 0) {
                    //Get the node off the open list and assign to current node
                    
                    Node node_current = OPEN.pop();

                    //if current node= goal node, break
                    if (node_current.isMatch(node_goal))
                    {
                        node_goal.parentNode = node_current.parentNode;
                        break;
                    }

                    //else generate list (successors) of successors for current node
                    ArrayList successors = node_current.GetSuccessors();

                    //iterate in successors list
                    foreach (Node node_successor in successors)
                    {
                        //cost of node_successor is set to cost of node_current + cost to get to node_successor from node_current by //////
                     
                        //get index of node_successor from OPEN list if exist in it
                        int indexOpen = OPEN.IndexOf(node_successor);

                        //if index is in OPEN but existing is better: continue
                        if (indexOpen > 0)
                        {
                            // assign node at index to existing_index
                            Node existing_node = OPEN.NodeAt(indexOpen);
                            if (existing_node.CompareTo(node_current) <= 0)
                                continue;
                        }

                        //if index is in closed
                        int indexClosed = CLOSED.IndexOf(node_successor);

                        //if node_successor is on the CLOSED list but the existing one is as good   or better then discard this successor and continue;
                        if (indexClosed > 0)
                        {
                            Node existing_node = CLOSED.NodeAt(indexClosed);
                            if (existing_node.CompareTo(node_current) <= 0)
                                continue;
                        }

                        //Remove occurences of node_successor from OPEN and CLOSED
                        if (indexOpen != -1)
                            OPEN.RemoveAt(indexOpen);
                        if (indexClosed != -1)
                            CLOSED.RemoveAt(indexClosed);

                         //Add node_successor to the OPEN list
                   OPEN.push (node_successor);

                  }
                  //Add node_current to the CLOSED list
                  CLOSED.push (node_current);


                    }
                }

            
        public bool AImove(int no)
        {


            if (no == 1)
            {
                // MessageBox.Show("You pressed Up arrow key");
                connect.SendData("UP#");
                return true;
            }

            if (no == 2)
            {
                //MessageBox.Show("You pressed Down arrow key");
                connect.SendData("DOWN#");
                return true;
            }

            if (no == 3)
            {
                //MessageBox.Show("You pressed Left arrow key");
                connect.SendData("LEFT#");
                return true;
            }


            if (no == 4)
            {
                // MessageBox.Show("You pressed Right arrow key");
                connect.SendData("RIGHT#");
                return true;
            }
            if (no == 5)
            {
                // MessageBox.Show("You pressed Right arrow key");
                connect.SendData("SHOOT#");
                return true;
            }
            return true;
        }


    

        private int[] randomArray = new int[100] { 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 3, 4, 4, 4, 5, 5, 5, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 4, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 4, 4, 4, 4, 3, 3, 3, 3, 4, 4 };
    }
}


