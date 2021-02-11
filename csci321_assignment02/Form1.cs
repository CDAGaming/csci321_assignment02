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

        // Variables
        Image img = Image.FromFile("puzzle.jpg");
        int resWidth = 360;
        int resHeight = 360;
        int emptyXY = 1;
        int holeXY = 3;
        int ballXY = 5;
        int size;
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
            Button current = sender as Button;
            current.Enabled = false;
            string path = Environment.CurrentDirectory + "/" + "puzzle.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            // Counts of components
            string[] counts = lines[0].Split(' ');
            size = Convert.ToInt32(counts[0]);
            int balls = Convert.ToInt32(counts[1]);
            int holes = Convert.ToInt32(counts[1]);
            int walls = Convert.ToInt32(counts[2]);

            // Create gameboard (2D array of GridBox)
            int gridHeight = resHeight / size;
            int gridWidth = resHeight / size;
            GameBoard = new GridBox[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    GameBoard[i, j] = new GridBox();
                    GameBoard[i, j].Row = i;
                    GameBoard[i, j].Col = j;
                    GameBoard[i, j].Item = 0;
                    GameBoard[i, j].WallCount = 0;
                    GameBoard[i, j].LeftWall = 0;
                    GameBoard[i, j].RightWall = 0;
                    GameBoard[i, j].TopWall = 0;
                    GameBoard[i, j].BottomWall = 0;
                    GameBoard[i, j].Location = new System.Drawing.Point(gridWidth * j + 20, gridHeight * i + 20);
                    GameBoard[i, j].Name = "grid" + i.ToString() + j.ToString();
                    GameBoard[i, j].BackColor = System.Drawing.Color.Black;
                    GameBoard[i, j].Size = new System.Drawing.Size(gridWidth, gridHeight);
                    GameBoard[i, j].SizeMode = PictureBoxSizeMode.Normal;
                    gameBox.Controls.Add(GameBoard[i, j]);
                }
            }

            // Assign balls
            for (int i = 1; i <= balls; i++)
            {
                string[] coords = lines[i].Split(' ');
                int row = Convert.ToInt32(coords[0]);
                int col = Convert.ToInt32(coords[1]);
                GameBoard[row, col].BallNum = i;
                GameBoard[row, col].Item = 1;
            }

            // Assign holes
            for (int i = balls + 1; i <= (balls * 2); i++)
            {
                string[] coords = lines[i].Split(' ');
                int row = Convert.ToInt32(coords[0]);
                int col = Convert.ToInt32(coords[1]);
                GameBoard[row, col].HoleNum = i - balls;
                GameBoard[row, col].Item = 2;
            }

            // Assign walls
            for (int i = (balls * 2) + 1; i <= (balls * 2) + walls; i++)
            {
                string[] coords = lines[i].Split(' ');
                int row1 = Convert.ToInt32(coords[0]);
                int col1 = Convert.ToInt32(coords[1]);
                int row2 = Convert.ToInt32(coords[2]);
                int col2 = Convert.ToInt32(coords[3]);
                GridBox box1 = GameBoard[row1, col1];
                GridBox box2 = GameBoard[row2, col2];
                box1.WallCount++;
                box2.WallCount++;
                if (row1 == row2) // same row
                {
                    if (col1 > col2)
                    {
                        box1.LeftWall = 1;
                        box2.RightWall = 1;
                    }
                    else
                    {
                        box1.RightWall = 1;
                        box2.LeftWall = 1;
                    }
                }
                else // same column
                {
                    if (row1 > row2)
                    {
                        box1.TopWall = 1;
                        box2.BottomWall = 1;
                    }
                    else
                    {
                        box1.BottomWall = 1;
                        box2.TopWall = 1;
                    }
                }
            }

            // Render board
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    GridBox box = GameBoard[i, j];

                    //box.Paint += new System.Windows.Forms.PaintEventHandler(this.GridBox_DrawBorder);
                    box.Paint += new System.Windows.Forms.PaintEventHandler(this.GridBox_DrawWalls);
                    box.Paint += new System.Windows.Forms.PaintEventHandler(this.GridBox_WriteNum);
                    int pw = (img.Width / 7);
                    int ph = (img.Height / 7);

                    Bitmap bm = new Bitmap(box.Width, box.Height);
                    Rectangle r = new Rectangle(0, 0, box.Width, box.Height);

                    if (box.Item == 0)
                    {
                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            g.DrawImage(img, r, pw * emptyXY, ph * emptyXY, pw, ph, GraphicsUnit.Pixel);
                        }
                    }
                    else if (box.Item == 1)
                    {
                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            g.DrawImage(img, r, pw * ballXY, ph * ballXY, pw, ph, GraphicsUnit.Pixel);
                        }
                    }
                    else if (box.Item == 2)
                    {
                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            g.DrawImage(img, r, pw * holeXY, ph * holeXY, pw, ph, GraphicsUnit.Pixel);
                        }
                    }
                    box.Image = bm;
                }
            }
        }

        public void GridBox_DrawWalls(object sender, PaintEventArgs e)
        {
            GridBox curr = sender as GridBox;
            PointF topLeft = new PointF(0, 0);
            PointF topRight = new PointF(curr.Width, 0);
            PointF bottomLeft = new PointF(0, curr.Height);
            PointF bottomRight = new PointF(curr.Width, curr.Height);

            if (curr.LeftWall == 1)
            {
                Pen pen = new Pen(Color.Black, 10);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topLeft, bottomLeft);
                }
            }

            if (curr.RightWall == 1)
            {
                Pen pen = new Pen(Color.Black, 10);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topRight, bottomRight);
                }
            }

            if (curr.TopWall == 1)
            {
                Pen pen = new Pen(Color.Black, 10);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topLeft, topRight);
                }
            }

            if (curr.BottomWall == 1) 
            {
                Pen pen = new Pen(Color.Black, 10);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, bottomLeft, bottomRight);
                }
            }
        }

        private void GridBox_WriteNum(object sender, PaintEventArgs e)
        {
            GridBox curr = sender as GridBox;
            string holeNum = curr.HoleNum.ToString();
            string ballNum = curr.BallNum.ToString();

            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;
            Rectangle r = new Rectangle(0, 0, curr.Width, curr.Height);
            Brush brushYellow = new SolidBrush(Color.Yellow);

            if (curr.HoleNum > 0)
            {
                using (Font myFont = new Font("Arial", 10))
                {
                    e.Graphics.DrawString(holeNum, myFont, brushYellow, r, drawFormat);
                }
            }

            if (curr.BallNum > 0)
            {
                using (Font myFont = new Font("Arial", 10))
                {
                    e.Graphics.DrawString(ballNum, myFont, brushYellow, r, drawFormat);
                }
            }
        }
        
    }
}
