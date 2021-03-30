using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace csci321_assignment02
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
            timerObj.syncWithCurrentTime = false;
        }

        // Variables
        string cacheDirectory;
        Image img = null;
        string dataPath = null, lastScorePath = null, lastMrbPath = null;
        int size, moves = 0;
        float ratio = 0;
        private GridBox[,] GameBoard;
        private List<Highscore> latestScores = new List<Highscore>();

        // Preset Data for 7 factor image
        readonly int emptyXY = 0;
        readonly int holeXY = 2;
        readonly int ballXY = 4;
        readonly int errXY = 6;

        // SCORING DATA START

        private List<Highscore> ReadScoresFromFile(string path, bool isSerializedData)
        {
            latestScores = new List<Highscore>();

            if (isSerializedData)
            {
                try
                {
                    using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        var score = bin.Deserialize(stream);
                        if (score.GetType() == typeof(Highscore))
                        {
                            latestScores.Add((Highscore)score);
                        }
                        else if (score.GetType() == typeof(List<Highscore>))
                        {
                            latestScores = ((List<Highscore>)score);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Data Type for score data: {0}", score.GetType());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to retrieve score data: {0}", ex);
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        try
                        {
                            latestScores.Add(new Highscore(line));
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine("Invalid score at line \"{0}\": {1}", line, ex);
                        }
                    }
                }
            }

            return SortAndPositionHighscores(latestScores);
        }

        static List<Highscore> SortAndPositionHighscores(List<Highscore> scores)
        {
            scores = scores.OrderByDescending(s => s.Score).ThenByDescending(s => s.Time).ToList();

            int pos = 1;

            scores.ForEach(s => s.Position = pos++);

            return scores.ToList();
        }

        // Function to pad an integer number 
        // with leading zeros
        static string padNumber(int N, int P)
        {
            // string used in Format() method
            string s = "{0:";
            for (int i = 0; i < P; i++)
            {
                s += "0";
            }
            s += "}";

            // use of string.Format() method
            string value = string.Format(s, N);

            // return output
            return value;
        }

        // SCORING DATA END

        private void ToggleControls(bool value)
        {
            upButton.Enabled = value;
            downButton.Enabled = value;
            leftButton.Enabled = value;
            rightButton.Enabled = value;
        }

        private void ValidateGame(List<GridBox> arr)
        {
            string timestamp = padNumber(timerObj.currentHour, 2) + ":" + padNumber(timerObj.currentMinute, 2) + ":" + padNumber(timerObj.currentSeconds, 2);
            // Check loss condition
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].Item == -1)
                {
                    ToggleControls(false);
                    timerObj.PauseTiming();
                    initButton.Enabled = true;
                    playerName.Enabled = true;
                    initButton.Text = "Restart";
                    MessageBox.Show("Game Over! You lasted " + timestamp + " with " + moves + " move(s)");
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
            timerObj.PauseTiming();
            initButton.Enabled = true;
            playerName.Enabled = true;
            initButton.Text = "Restart";
            MessageBox.Show("You Won in " + timestamp + " with " + moves + " move(s)");

            // Save Data in Highscores and Refresh Layout
            Highscore newScore = new Highscore(playerName.Text + " " + moves + " " + timestamp);
            latestScores.Add(newScore);

            using (Stream stream = new FileStream(lastScorePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, latestScores);
            }

            // Save Scores to ZIP File
            MarbleExplorer.AddFilesToZip(lastMrbPath, new[] { lastScorePath });

            // Read Data Copied from MarbleExplorer End Event
            latestScores = ReadScoresFromFile(lastScorePath, true);
            if (latestScores.Any())
            {
                highScores.Text = "Scores" + Environment.NewLine;
                latestScores.ForEach(s => highScores.Text += s + Environment.NewLine);
            }
            else
            {
                highScores.Text = "No Scores Available";
            }
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

        private void UpButton_Click(object sender, EventArgs e)
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
            moves += 1;
        }

        private void DownButton_Click(object sender, EventArgs e)
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
            moves += 1;
        }

        private void LeftButton_Click(object sender, EventArgs e)
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
            moves += 1;
        }

        private void RightButton_Click(object sender, EventArgs e)
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
            moves += 1;
        }

        private void InitButton_Click(object sender, EventArgs e)
        {
            bool nameValid = playerName.Text.Any(x => char.IsLetter(x));
            if (!nameValid)
            {
                MessageBox.Show("Pleas enter a valid name to continue...");
                return;
            }
            string originalText = initButton.Text;
            switch (originalText)
            {
                case "Start Game":
                case "Restart":
                case "Start New Game":
                    gameBox.Controls.Clear();
                    gameBox.Visible = true;
                    moves = 0;
                    playerName.Enabled = false;
                    ToggleControls(true);
                    initButton.Text = "Pause Game";
                    timerObj.ResetTiming();
                    timerObj.StartTiming();
                    break;
                case "Pause Game":
                    ToggleControls(false);
                    gameBox.Visible = false;
                    initButton.Text = "Resume Game";
                    timerObj.PauseTiming();
                    break;
                case "Resume Game":
                    ToggleControls(true);
                    gameBox.Visible = true;
                    initButton.Text = "Pause Game";
                    timerObj.StartTiming();
                    break;
                default:
                    initButton.Text = "UNKNOWN_STATE";
                    initButton.Enabled = false;
                    playerName.Enabled = false;
                    ToggleControls(false);
                    gameBox.Visible = false;
                    moves = 0;
                    timerObj.PauseTiming();
                    break;
            }

            if (originalText == "Pause Game" || originalText == "Resume Game")
            {
                return;
            }

            string path = dataPath;
            string[] lines = File.ReadAllLines(path);

            // Counts of components
            string[] counts = lines[0].Split(' ');
            size = Convert.ToInt32(counts[0]);
            int balls = Convert.ToInt32(counts[1]);
            int holes = Convert.ToInt32(counts[1]);
            int walls = Convert.ToInt32(counts[2]);

            // Scaling Data
            Console.WriteLine("Max Render Size: " + gameBox.Size.Width + ", " + gameBox.Size.Height);
            int resWidth = gameBox.Size.Width - (gameBox.Size.Width / 3); // Max width is 2/3 the gameBox width
            int resHeight = gameBox.Size.Height - (gameBox.Size.Height / 3); // Max height is 2/3 the gameBox height

            // Scaling ratio for gameboard
            if (img.Width >= resWidth) // shrink original image
            {
                if (img.Width >= img.Height) // shrink by width
                {
                    ratio = img.Width / (float)resWidth;
                }
                else // shrink by height
                {
                    ratio = img.Height / (float)resHeight;
                }
            }
            else // enlarge original image
            {
                if (img.Width >= img.Height) // enlarge by width
                {
                    ratio = resWidth / (float)img.Width;
                }
                else // enlarge by height
                {
                    ratio = resHeight / (float)img.Height;
                }
            }

            // Create gameboard (2D array of GridBox)
            float gridWidth = img.Width / ratio / size;
            float gridHeight = img.Height / ratio / size;
            float marginTop = 20;
            float marginLeft = 20;
            GameBoard = new GridBox[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    GameBoard[i, j] = new GridBox
                    {
                        Row = i,
                        Col = j,
                        Item = 0,
                        WallCount = 0,
                        LeftWall = 0,
                        RightWall = 0,
                        TopWall = 0,
                        BottomWall = 0,
                        BallNum = 0,
                        HoleNum = 0,
                        Location = new Point((int)((gridWidth * j) + marginLeft), (int)((gridHeight * i) + marginTop)),
                        Name = "grid" + i.ToString() + j.ToString(),
                        BackColor = Color.Black,
                        Size = new Size((int)gridWidth, (int)gridHeight),
                        SizeMode = PictureBoxSizeMode.Normal
                    };
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
                    box.Paint += new PaintEventHandler(GridBox_DrawWalls);
                    box.Paint += new PaintEventHandler(GridBox_WriteNum);
                    box.Paint += new PaintEventHandler(GridBox_DrawBorder);

                    int pw = img.Width / 7;
                    int ph = img.Height / 7;

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
                        StringFormat sf = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        };
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
            float outerWallWidth = 10;
            float innerWallWidth = 7.5f;
            PointF topLeft = new PointF(0, 0);
            PointF topRight = new PointF(curr.Width, 0);
            PointF bottomLeft = new PointF(0, curr.Height);
            PointF bottomRight = new PointF(curr.Width, curr.Height);

            if (curr.HasLeftWall() || curr.Col == 0)
            {
                Pen pen = new Pen(Color.Red, curr.Col == 0 ? outerWallWidth : innerWallWidth);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topLeft, bottomLeft);
                }
            }

            if (curr.HasRightWall() || curr.Col == size - 1)
            {
                Pen pen = new Pen(Color.Red, curr.Col == size - 1 ? outerWallWidth : innerWallWidth);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topRight, bottomRight);
                }
            }

            if (curr.HasTopWall() || curr.Row == 0)
            {
                Pen pen = new Pen(Color.Red, curr.Row == 0 ? outerWallWidth : innerWallWidth);
                using (pen)
                {
                    e.Graphics.DrawLine(pen, topLeft, topRight);
                }
            }

            if (curr.HasBottomWall() || curr.Row == size - 1)
            {
                Pen pen = new Pen(Color.Red, curr.Row == size - 1 ? outerWallWidth : innerWallWidth);
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

            StringFormat drawFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
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

        private void GridBox_DrawBorder(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            string imgPath = null;
            using (MarbleExplorer explorer = new MarbleExplorer(Environment.CurrentDirectory, cacheDirectory))
            {
                DialogResult result = explorer.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        imgPath = explorer.returnDirectory + Path.DirectorySeparatorChar + "puzzle.jpg";
                        lastScorePath = explorer.returnDirectory + Path.DirectorySeparatorChar + "puzzle.bin";
                        lastMrbPath = explorer.currentlySelectedItemPath;
                        img = Image.FromFile(imgPath);
                        dataPath = explorer.returnDirectory + Path.DirectorySeparatorChar + "puzzle.txt";

                        // Parse Score Data if available
                        if (File.Exists(lastScorePath))
                        {
                            latestScores = ReadScoresFromFile(lastScorePath, true);
                            if (latestScores.Any())
                            {
                                highScores.Text = "Scores" + Environment.NewLine;
                                latestScores.ForEach(s => highScores.Text += s + Environment.NewLine);
                            }
                            else
                            {
                                highScores.Text = "No Scores Available";
                            }
                        }
                        else
                        {
                            highScores.Text = "No Scores Available";
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to parse data in cached directory '" + explorer.returnDirectory + "' -- Please verify file and try again");
                    }
                }
            }
            bool verifiedData = (img != null && imgPath != null && dataPath != null && File.Exists(dataPath) && File.Exists(imgPath));
            initButton.Enabled = verifiedData || initButton.Enabled;
            initButton.Text = verifiedData ? "Start New Game" : initButton.Text;
        }

        private void App_Load(object sender, EventArgs e)
        {
            // When form is loaded, create an empty cache folder if not already present
            // This folder will house mrb file data, primarily for image previews and data retrieval
            // Format: cache/<mrb_file_name_no_exst>/<unzipped_files>
            cacheDirectory = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "cache";
            if (!Directory.Exists(cacheDirectory))
            {
                Console.WriteLine("Cache directory not present, creating...");
                Directory.CreateDirectory(cacheDirectory);
            }
            OpenFileButton.Enabled = true;
        }

        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            // When form is closing, remove all files within the cache folder and perform garbage collection
            img = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (Directory.Exists(cacheDirectory))
            {
                Directory.Delete(cacheDirectory, true);
            }
        }

        private void App_ResizeEnd(object sender, EventArgs e)
        {
            if (img != null && GameBoard != null)
            {
                gameBox.Controls.Clear();
                // Scaling Data
                Console.WriteLine("Max Render Size: " + gameBox.Size.Width + ", " + gameBox.Size.Height);
                int resWidth = gameBox.Size.Width - (gameBox.Size.Width / 3); // Max width is 2/3 the gameBox width
                int resHeight = gameBox.Size.Height - (gameBox.Size.Height / 3); // Max height is 2/3 the gameBox height

                // Scaling ratio for gameboard
                if (img.Width >= resWidth) // shrink original image
                {
                    if (img.Width >= img.Height) // shrink by width
                    {
                        ratio = img.Width / (float)resWidth;
                    }
                    else // shrink by height
                    {
                        ratio = img.Height / (float)resHeight;
                    }
                }
                else // enlarge original image
                {
                    if (img.Width >= img.Height) // enlarge by width
                    {
                        ratio = resWidth / (float)img.Width;
                    }
                    else // enlarge by height
                    {
                        ratio = resHeight / (float)img.Height;
                    }
                }

                // Sync gameboard (2D array of GridBox)
                float gridWidth = img.Width / ratio / size;
                float gridHeight = img.Height / ratio / size;
                float marginTop = 20;
                float marginLeft = 20;
                GridBox emptyBox = new GridBox();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        GameBoard[i, j] = GameBoard[i, j];
                        GameBoard[i, j].Location = new Point((int)((gridWidth * j) + marginLeft), (int)((gridHeight * i) + marginTop));
                        GameBoard[i, j].Size = new Size((int)gridWidth, (int)gridHeight);
                        gameBox.Controls.Add(GameBoard[i, j]);
                    }
                }

                // Render board
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        GridBox box = GameBoard[i, j];
                        box.Paint += new PaintEventHandler(GridBox_DrawWalls);
                        box.Paint += new PaintEventHandler(GridBox_WriteNum);
                        box.Paint += new PaintEventHandler(GridBox_DrawBorder);

                        int pw = img.Width / 7;
                        int ph = img.Height / 7;

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
                            StringFormat sf = new StringFormat
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Center
                            };
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
        }
    }

    // SCORING DATA
    [Serializable]
    class Highscore
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public string Time { get; set; }

        public Highscore(string data)
        {
            var d = data.Split(' ');

            if (string.IsNullOrEmpty(data) || d.Length < 3)
                throw new ArgumentException("Invalid high score string", data);

            Name = d[0];

            if (int.TryParse(d[1], out int num))
            {
                Score = num;
            }
            else
            {
                throw new ArgumentException("Invalid score", "data");
            }

            Time = d[2];
        }

        public override string ToString()
        {
            return string.Format("{0}. {1}: {2} ({3})", Position, Name, Score, Time);
        }
    }
}
