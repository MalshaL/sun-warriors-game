using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankClient
{
    public partial class GUI : Form
    {
        ConnectClient connect = ConnectClient.GetClient();
        public GUI()
        {
            
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            //capture up arrow key
            if (keyData == Keys.Up)
            {
               // MessageBox.Show("You pressed Up arrow key");
                connect.SendData("UP#");
                return true;
            }
            //capture down arrow key
            if (keyData == Keys.Down)
            {
                //MessageBox.Show("You pressed Down arrow key");
                connect.SendData("DOWN#");
                return true;
            }
            //capture left arrow key
            if (keyData == Keys.Left)
            {
                //MessageBox.Show("You pressed Left arrow key");
                connect.SendData("LEFT#");
                return true;
            }
            //capture right arrow key
            if (keyData == Keys.Right)
            {
               // MessageBox.Show("You pressed Right arrow key");
                connect.SendData("RIGHT#");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void GUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.CadetBlue;
           // this.Size = new Size(100, 100);
            this.Location = new Point(900, 50);
            
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect.SendData("JOIN#");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect.SendData("SHOOT#");
        }
    }
}
