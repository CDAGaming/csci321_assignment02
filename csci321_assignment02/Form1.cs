using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csci321_assignment02
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("UP clicked");
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("DOWN clicked");
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LEFT clicked");
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("RIGHT clicked");
        }

    }
}
