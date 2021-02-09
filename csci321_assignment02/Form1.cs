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

        private void initButton_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + "/" + "puzzle.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            
            // Counts of components
            string[] counts = lines[0].Split(' ');
            int size = Convert.ToInt32(counts[0]);
            int balls, holes = Convert.ToInt32(counts[1]);
            int walls = Convert.ToInt32(counts[2]);

            // Draw gameboard

        }
    }
}
