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

        private GridBox[,] GameBoard;

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
            int balls = Convert.ToInt32(counts[1]);
            int holes = Convert.ToInt32(counts[1]);
            int walls = Convert.ToInt32(counts[2]);

            // Draw gameboard (2D array of GridBox)
            int gridHeight = 50;
            int gridWidth = 50;
            GameBoard = new GridBox[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    GameBoard[i, j] = new GridBox();
                    GameBoard[i, j].Row = i;
                    GameBoard[i, j].Col = j;
                    GameBoard[i, j].Location = new System.Drawing.Point(gridHeight * j + 50, gridWidth * i + 100);
                    GameBoard[i, j].Name = "grid" + i.ToString() + j.ToString();
                    GameBoard[i, j].BackColor = System.Drawing.Color.Green;
                    GameBoard[i, j].Size = new System.Drawing.Size(gridHeight, gridWidth);
                    gameBox.Controls.Add(GameBoard[i, j]);
                    
                    GameBoard[i, j].Paint += new System.Windows.Forms.PaintEventHandler(this.gridbox_Paint);
                }
            }
        }

        // Writes row and col number for ewach grid (TESTING PURPOSES)
        private void gridbox_Paint(object sender, PaintEventArgs e)
        {
            GridBox tmp = sender as GridBox;
            string txt = tmp.Row.ToString() + tmp.Col.ToString();
            using (Font myFont = new Font("Arial", 10))
            {
                e.Graphics.DrawString(txt, myFont, Brushes.Black, new Point(2,2));
            }
        }
    }
}
