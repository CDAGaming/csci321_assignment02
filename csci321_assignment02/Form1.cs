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
        Image img = Image.FromFile("puzzleWide.jpg");
        int resWidth = 360;
        int resHeight = 360;
        int emptyXY = 0;
        int holeXY = 2;
        int ballXY = 4;
        int errXY = 6;
        int size;
        float ratio = 0;
        private GridBox[,] GameBoard;

        private void ToggleControls(bool value)
        {
            upButton.Enabled = value;
            downButton.Enabled = value;
            leftButton.Enabled = value;
            rightButton.Enabled = value;
        }

        private void ValidateGame(List<GridBox> arr)
        {
            // Check loss condition
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].Item == -1)
                {
                    ToggleControls(false);
                    initButton.Enabled = true;
                    initButton.Text = "Restart";
                    MessageBox.Show("Game Over");
                    return;
                }
            }

            // Check win condition
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (GameBoard[i, j].Item != 0)
                    {
                        return;
                    }
                }
            }

            ToggleControls(false);
            initButton.Enabled = true;
            initButton.Text = "Restart";
            MessageBox.Show("You Won!");
        }

        private void RenderBox(List<GridBox> arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                GridBox box = arr[i];
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
                else if (box.Item == -1)
                {
                    using (Graphics g = Graphics.FromImage(bm))
                    {
                        g.DrawImage(img, r, pw * errXY, ph * errXY, pw, ph, GraphicsUnit.Pixel);
                    }
                }
                box.Image = bm;
            }
            ValidateGame(arr);
        }

        private bool HasHoleNext(GridBox box)
        {
            return box.HoleNum > 0;
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            List<GridBox> Updates = new List<GridBox>();
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    GridBox box = GameBoard[row, col];
                    if (box.HasBall())
                    {
                        Updates.Add(box);
                        int currRow = row;
                        GridBox currBox;
                        while (true)
                        {
                            currBox = GameBoard[currRow, col];
                            if (currRow <= 0)
                            {
                                break;
                            }
                            if (!currBox.HasTopWall()) //no wall; move
                            {
                                GridBox nextBox = GameBoard[currRow - 1, col];
                                if (HasHoleNext(nextBox)) // has hole
                                {
                                    if (nextBox.HoleNum == currBox.BallNum) // correct hole
                                    {
                                        currBox.Item = 0;
                                        currBox.BallNum = 0;
                                        nextBox.Item = 0;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                    else // incorrect hole
                                    {
                                        currBox.Item = -1;
                                        currBox.BallNum = 0;
                                        nextBox.Item = -1;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                }
                                else if (nextBox.Item == 0) //empty
                                {
                                    nextBox.Item = 1;
                                    nextBox.BallNum = currBox.BallNum;
                                    currBox.Item = 0;
                                    currBox.BallNum = 0;
                                    currRow--;
                                }
                                else if (nextBox.Item == 1) // has ball
                                {
                                    break;
                                }
                            }
                            else // wall on bottom
                            {
                                break;
                            }
                        }
                        Updates.Add(currBox);
                    }
                }
            }
            RenderBox(Updates);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            List<GridBox> Updates = new List<GridBox>();
            for (int row = size - 2; row >= 0; row--)
            {
                for (int col = 0; col < size; col++)
                {
                    GridBox box = GameBoard[row, col];
                    if (box.HasBall())
                    {
                        Updates.Add(box);
                        int currRow = row;
                        GridBox currBox;
                        while (true)
                        {
                            currBox = GameBoard[currRow, col];
                            if (currRow >= size - 1)
                            {
                                break;
                            }
                            if (!currBox.HasBottomWall()) //no wall; move
                            {
                                GridBox nextBox = GameBoard[currRow + 1, col];
                                if (HasHoleNext(nextBox)) // has hole
                                {
                                    if (nextBox.HoleNum == currBox.BallNum) // correct hole
                                    {
                                        currBox.Item = 0;
                                        currBox.BallNum = 0;
                                        nextBox.Item = 0;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                    else // incorrect hole
                                    {
                                        currBox.Item = -1;
                                        currBox.BallNum = 0;
                                        nextBox.Item = -1;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                }
                                else if (nextBox.Item == 0) //empty
                                {
                                    nextBox.Item = 1;
                                    nextBox.BallNum = currBox.BallNum;
                                    currBox.Item = 0;
                                    currBox.BallNum = 0;
                                    currRow++;
                                }
                                else if (nextBox.Item == 1) // has ball
                                {
                                    break;
                                }
                            }
                            else // wall on bottom
                            {
                                break;
                            }
                        }
                        Updates.Add(currBox);
                    }
                }
            }
            RenderBox(Updates);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            List<GridBox> Updates = new List<GridBox>();
            for (int col = 1; col < size; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    GridBox box = GameBoard[row, col];
                    if (box.HasBall())
                    {
                        Updates.Add(box);
                        int currCol = col;
                        GridBox currBox;
                        while (true)
                        {
                            currBox = GameBoard[row, currCol];
                            if (currCol <= 0)
                            {
                                break;
                            }
                            if (!currBox.HasLeftWall()) //no wall; move
                            {
                                GridBox nextBox = GameBoard[row, currCol - 1];
                                if (HasHoleNext(nextBox)) // has hole
                                {
                                    if (nextBox.HoleNum == currBox.BallNum) // correct hole
                                    {
                                        currBox.Item = 0;
                                        currBox.BallNum = 0;
                                        nextBox.Item = 0;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                    else // incorrect hole
                                    {
                                        currBox.Item = -1;
                                        currBox.BallNum = 0;
                                        nextBox.Item = -1;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                }
                                else if (nextBox.Item == 0) //empty
                                {
                                    nextBox.Item = 1;
                                    nextBox.BallNum = currBox.BallNum;
                                    currBox.Item = 0;
                                    currBox.BallNum = 0;
                                    currCol--;
                                }
                                else if (nextBox.Item == 1) // has ball
                                {
                                    break;
                                }
                            }
                            else // wall on bottom
                            {
                                break;
                            }
                        }
                        Updates.Add(currBox);
                    }
                }
            }
            RenderBox(Updates);
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            List<GridBox> Updates = new List<GridBox>();
            for (int col = size - 2; col >= 0; col--)
            {
                for (int row = 0; row < size; row++)
                {
                    GridBox box = GameBoard[row, col];
                    if (box.HasBall())
                    {
                        Updates.Add(box);
                        int currCol = col;
                        GridBox currBox;
                        while (true)
                        {
                            currBox = GameBoard[row, currCol];
                            if (currCol >= size - 1)
                            {
                                break;
                            }
                            if (!currBox.HasRightWall()) //no wall; move
                            {
                                GridBox nextBox = GameBoard[row, currCol + 1];
                                if (HasHoleNext(nextBox)) // has hole
                                {
                                    if (nextBox.HoleNum == currBox.BallNum) // correct hole
                                    {
                                        currBox.Item = 0;
                                        currBox.BallNum = 0;
                                        nextBox.Item = 0;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                    else // incorrect hole
                                    {
                                        currBox.Item = -1;
                                        currBox.BallNum = 0;
                                        nextBox.Item = -1;
                                        nextBox.HoleNum = 0;
                                        Updates.Add(nextBox);
                                        break;
                                    }
                                }
                                else if (nextBox.Item == 0) //empty
                                {
                                    nextBox.Item = 1;
                                    nextBox.BallNum = currBox.BallNum;
                                    currBox.Item = 0;
                                    currBox.BallNum = 0;
                                    currCol++;
                                }
                                else if (nextBox.Item == 1) // has ball
                                {
                                    break;
                                }
                            }
                            else // wall on bottom
                            {
                                break;
                            }
                        }
                        Updates.Add(currBox);
                    }
                }
            }
            RenderBox(Updates);
        }

        private void initButton_Click(object sender, EventArgs e)
        {
            gameBox.Controls.Clear();
            ToggleControls(true);
            initButton.Text = "Game in progress";
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

            // Scaling ratio for gameboard
            if (img.Width >= resWidth) // shrink original image
            {
                if (img.Width >= img.Height) // shrink by width
                {
                    ratio = (float)img.Width / (float)resWidth;
                }
                else // shrink by height
                {
                    ratio = (float)img.Height / (float)resHeight;
                }
            }
            else // enlarge original image
            {
                if (img.Width >= img.Height) // enlarge by width
                {
                    ratio = (float)resWidth / (float)img.Width;
                }
                else // enlarge by height
                {
                    ratio = (float)resHeight / (float)img.Height;
                }
            }

            // Create gameboard (2D array of GridBox)
            //int gridHeight = (resHeight / size);
            //int gridWidth = (resWidth / size);
            float gridWidth = img.Width / ratio / (float)size;
            float gridHeight = img.Height / ratio / (float)size;
            float marginTop = 20;
            float marginLeft = 20;
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
                    GameBoard[i, j].BallNum = 0;
                    GameBoard[i, j].HoleNum = 0;
                    GameBoard[i, j].Location = new System.Drawing.Point((int)((gridWidth * j) + marginLeft), (int)((gridHeight * i) + marginTop));
                    GameBoard[i, j].Name = "grid" + i.ToString() + j.ToString();
                    GameBoard[i, j].BackColor = System.Drawing.Color.Black;
                    GameBoard[i, j].Size = new System.Drawing.Size((int)gridWidth, (int)gridHeight);
                    GameBoard[i, j].SizeMode = PictureBoxSizeMode.Normal;
                    gameBox.Controls.Add(GameBoard[i, j]);
                }
            }

            // Assign balls (Item = 1)
            for (int i = 1; i <= balls; i++)
            {
                string[] coords = lines[i].Split(' ');
                int row = Convert.ToInt32(coords[0]);
                int col = Convert.ToInt32(coords[1]);
                GameBoard[row, col].BallNum = i;
                GameBoard[row, col].Item = 1;
            }

            // Assign holes (Item = 2)
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
                    Console.WriteLine(box.Width.ToString());
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
                        Font f = new Font("Arial", 16.0f);
                        Brush by = new SolidBrush(Color.Yellow);
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;
                        string text = box.BallNum.ToString();
                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            g.DrawImage(img, r, pw * ballXY, ph * ballXY, pw, ph, GraphicsUnit.Pixel);
                            g.DrawString(text, f, by, r, sf);
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
                Pen pen = new Pen(Color.Red, 10);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topLeft, bottomLeft);
                }
            }

            if (curr.RightWall == 1)
            {
                Pen pen = new Pen(Color.Red, 10);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topRight, bottomRight);
                }
            }

            if (curr.TopWall == 1)
            {
                Pen pen = new Pen(Color.Red, 10);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topLeft, topRight);
                }
            }

            if (curr.BottomWall == 1) 
            {
                Pen pen = new Pen(Color.Red, 10);
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
                using (Font myFont = new Font("Arial", 16.0f))
                {
                    e.Graphics.DrawString(holeNum, myFont, brushYellow, r, drawFormat);
                }
            }

            if (curr.BallNum > 0)
            {
                using (Font myFont = new Font("Arial", 16.0f))
                {
                    e.Graphics.DrawString(ballNum, myFont, brushYellow, r, drawFormat);
                }
            }
        }
        
    }
}
