using MarbleGame;
using MarbleGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csci321_assignment02
{
    public partial class App : Form
    {
        string cacheDirectory;
        Board board;
        Bitmap[,] imageData, boardRenderData;
        GridBox[,] GameBoard;

        public App()
        {
            InitializeComponent();
            // TODO: Relocate this call when selecting a custom image/puzzle is setup
            SetupData();
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("UP clicked");
            UpdateBoard(Sides.South);
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("DOWN clicked");
            UpdateBoard(Sides.North);
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LEFT clicked");
            UpdateBoard(Sides.East);
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("RIGHT clicked");
            UpdateBoard(Sides.West);
        }

        public void SetupData(string puzzleDataLoc = "cache/puzzle.txt", string puzzleImageLoc = "cache/puzzle.jpg")
        {
            Console.WriteLine("Setting up board based on pre-supplied data");
            Board board = new Board();
            board.InitializeBasicParams(puzzleDataLoc, "file");

            if (!(board.Size == 0 && board.MarblesCount == 0 && board.WallsCount == 0))
            {
                //Initialize marbles
                Console.WriteLine("Retrieving marble positions");
                board.InitializeBoardObjects(BoardObjectTypes.Marble, puzzleDataLoc, "file");

                //Initialize holes
                Console.WriteLine("Retrieving hole positions");
                board.InitializeBoardObjects(BoardObjectTypes.Hole, puzzleDataLoc, "file");

                //Initialize walls
                Console.WriteLine("Retrieving wall positions");
                board.InitializeBoardObjects(puzzleDataLoc, "file");
            } else
            {
                Console.WriteLine("Error: Invalid Data, please try again...");
                return;
            }

            this.board = board;
            GameBoard = new GridBox[this.board.Size, this.board.Size];
            Console.WriteLine("Puzzle Data Read success, continuing to read Image Data...");
            imageData = ReadImageData(puzzleImageLoc, 7);
            UpdateImageData();
        }

        public void UpdateBoard(Sides side, bool allowFailures = true)
        {
            if (board != null)
            {
                // Solution#Routes also stores the move history, but we may need to replace string.history to store a localized copy
                Solution solveState = board.LiftBoard(side, string.Empty, allowFailures);
                board = solveState.BoardState;
                UpdateImageData();

                if (solveState.Success)
                {
                    EndGameEvent("You won, please restart the game for a new game!");
                }
                else
                {
                    UpdateImageForLoss();
                }
            }
        }

        public void UpdateImageData()
        {
            // Clear the Current Board before supplying new data
            boardRenderData = new Bitmap[board.Size, board.Size];
            if (imageData != null)
            {
                // Assign images to form based on the layout of puzzle.txt
                for (int row = 0; row < board.Size; row++)
                    for (int column = 0; column < board.Size; column++)
                    {
                        Marble possibleMarble = board.Marbles.Where(m => m.Position.Row == row && m.Position.Column == column).FirstOrDefault();
                        Hole possibleHole = board.Holes.Where(h => (h.Position.Row == row && h.Position.Column == column)).FirstOrDefault();

                        WallPosition possibleWall = board.Walls.Where(w => (w.FirstSide.Row == row && w.FirstSide.Column == column) || (w.SecondSide.Row == row && w.SecondSide.Column == column)).FirstOrDefault();

                        Console.WriteLine("Position: " + row + ", " + column);
                        Console.WriteLine("Marble: " + (possibleMarble == null ? "N/A" : possibleMarble.Number.ToString()));
                        Console.WriteLine("Hole: " + (possibleHole == null ? "N/A" : possibleHole.Number.ToString()));
                        Console.WriteLine("Wall: " + (possibleWall == null ? "N/A" : ("[" + possibleWall.FirstSide.Row + ", " + possibleWall.FirstSide.Column + " :: " + possibleWall.SecondSide.Row + ", " + possibleWall.SecondSide.Column + "]")));

                        if (possibleHole == null && possibleMarble == null && possibleWall == null)
                        {
                            // OuterWall and WhiteSpace Rendering
                            if (row == 0)
                            {
                                // Top Row Data
                                if (column == 0)
                                {
                                    // Left Top
                                    boardRenderData[row, column] = imageData[0, 5];
                                } else if (column == board.Size - 1)
                                {
                                    // Right Top
                                    boardRenderData[row, column] = imageData[1, 1];
                                } else
                                {
                                    // Top Walls
                                    boardRenderData[row, column] = imageData[0, 3];
                                }
                            } else if (row == board.Size - 1)
                            {
                                // Bottom Row Data
                                if (column == 0)
                                {
                                    // Left Bottom
                                    boardRenderData[row, column] = imageData[1, 0];
                                }
                                else if (column == board.Size - 1)
                                {
                                    // Right Bottom
                                    boardRenderData[row, column] = imageData[1, 2];
                                } else
                                {
                                    // Bottom Walls
                                    boardRenderData[row, column] = imageData[0, 4];
                                }
                            }
                            else if (column == 0)
                            {
                                // Left Side Data
                                boardRenderData[row, column] = imageData[0, 1];
                            }
                            else if (column == board.Size - 1)
                            {
                                // Right Side Data
                                boardRenderData[row, column] = imageData[0, 2];
                            } else
                            {
                                // All other Empty space
                                boardRenderData[row, column] = imageData[0, 0];
                            }
                        } else if (possibleWall == null && ((possibleMarble != null && possibleMarble.GetBorderSide(board.Size - 1) == BorderSides.Unknown) || (possibleHole != null && possibleHole.GetBorderSide(board.Size - 1) == BorderSides.Unknown)))
                        {
                            if (possibleMarble != null)
                            {
                                boardRenderData[row, column] = imageData[4, 4];
                            } else if (possibleHole != null)
                            {
                                boardRenderData[row, column] = imageData[2, 2];
                            }
                        } else
                        {
                            BorderSides border = possibleMarble != null ? possibleMarble.GetBorderSide(board.Size - 1) : (possibleHole != null ? possibleHole.GetBorderSide(board.Size - 1) : BorderSides.Unknown);
                            Sides wallBorder = possibleWall != null ? possibleWall.GetRenderSide(row, column) : Sides.Unknown;
                            if (wallBorder == Sides.West || border == BorderSides.Left)
                            {
                                // West Wall
                                if (possibleMarble != null)
                                {
                                    boardRenderData[row, column] = imageData[4, 5];
                                } else if (possibleHole != null)
                                {
                                    boardRenderData[row, column] = imageData[2, 3];
                                } else
                                {
                                    boardRenderData[row, column] = imageData[0, 1];
                                }
                            } else if (wallBorder == Sides.East || border == BorderSides.Right)
                            {
                                // East Wall
                                if (possibleMarble != null)
                                {
                                    boardRenderData[row, column] = imageData[4, 6];
                                }
                                else if (possibleHole != null)
                                {
                                    boardRenderData[row, column] = imageData[2, 4];
                                } else
                                {
                                    boardRenderData[row, column] = imageData[0, 2];
                                }
                            } else if (wallBorder == Sides.North || border == BorderSides.Top)
                            {
                                // North Wall
                                if (possibleMarble != null)
                                {
                                    boardRenderData[row, column] = imageData[5, 0];
                                }
                                else if (possibleHole != null)
                                {
                                    boardRenderData[row, column] = imageData[2, 5];
                                } else
                                {
                                    boardRenderData[row, column] = imageData[0, 3];
                                }
                            } else if (wallBorder == Sides.South || border == BorderSides.Bottom)
                            {
                                // South Wall
                                if (possibleMarble != null)
                                {
                                    boardRenderData[row, column] = imageData[5, 1];
                                }
                                else if (possibleHole != null)
                                {
                                    boardRenderData[row, column] = imageData[2, 6];
                                } else
                                {
                                    boardRenderData[row, column] = imageData[0, 4];
                                }
                            } else
                            {
                                // This should only be reached for corner-marble wall cases
                                if (border != BorderSides.Unknown)
                                {
                                    if (border == BorderSides.LeftTop)
                                    {
                                        if (possibleMarble != null)
                                        {
                                            boardRenderData[row, column] = imageData[5, 2];
                                        } else if (possibleHole != null)
                                        {
                                            boardRenderData[row, column] = imageData[3, 0];
                                        }
                                    } else if (border == BorderSides.RightTop)
                                    {
                                        if (possibleMarble != null)
                                        {
                                            boardRenderData[row, column] = imageData[5, 5];
                                        }
                                        else if (possibleHole != null)
                                        {
                                            boardRenderData[row, column] = imageData[3, 3];
                                        }
                                    } else if (border == BorderSides.LeftBottom)
                                    {
                                        if (possibleMarble != null)
                                        {
                                            boardRenderData[row, column] = imageData[5, 4];
                                        }
                                        else if (possibleHole != null)
                                        {
                                            boardRenderData[row, column] = imageData[3, 2];
                                        }
                                    } else if (border == BorderSides.RightBottom)
                                    {
                                        if (possibleMarble != null)
                                        {
                                            boardRenderData[row, column] = imageData[5, 6];
                                        }
                                        else if (possibleHole != null)
                                        {
                                            boardRenderData[row, column] = imageData[3, 4];
                                        }
                                    }
                                } else
                                {
                                    Console.WriteLine("Unknown Circumstances for Generation, skipping [" + row + ", " + column + "]");
                                }
                            }
                        }
                    }
            } else
            {
                Console.WriteLine("Error: Image data is null");
            }
            DrawGrid();
        }

        private void EndGameEvent(string message)
        {
            MessageBox.Show(message);
            upButton.Enabled = false;
            downButton.Enabled = false;
            leftButton.Enabled = false;
            rightButton.Enabled = false;
        }

        private void UpdateImageForLoss()
        {
            bool hasLosses = false;
            foreach (var marble in board.Marbles.Where(m => m.IsInWrongHole))
            {
                boardRenderData[marble.Position.Row, marble.Position.Column] = imageData[6, 6];
                hasLosses = true;
            }
            if (hasLosses)
            {
                DrawGrid();
                EndGameEvent("You've landed your marble in a bad hole, you lose!");
            }
        }

        private void DrawGrid()
        {
            // Draw gameboard (2D array of GridBox)
            gameBox.Controls.Clear();

            int gridHeight = 95;
            int gridWidth = 95;
            for (int row = 0; row < board.Size; row++)
            {
                for (int column = 0; column < board.Size; column++)
                {
                    GameBoard[row, column] = new GridBox
                    {
                        Row = row,
                        Col = column,
                        Location = new Point(10 + (gridWidth * column), 20 + (gridHeight * row)),
                        Name = "grid" + row.ToString() + "_" + column.ToString(),
                        Image = boardRenderData[row, column],
                        Size = new Size(100, 100),
                        SizeMode = PictureBoxSizeMode.CenterImage
                    };
                    GameBoard[row, column].Paint += new PaintEventHandler(GridBox_Paint);
                    gameBox.Controls.Add(GameBoard[row, column]);
                }
            }
        }

        // Writes row and col number for each grid
        private void GridBox_Paint(object sender, PaintEventArgs e)
        {
            GridBox tmp = sender as GridBox;
            Marble possibleMarble = board.Marbles.Where(m => m.Position.Row == tmp.Row && m.Position.Column == tmp.Col).FirstOrDefault();
            Hole possibleHole = board.Holes.Where(h => (h.Position.Row == tmp.Row && h.Position.Column == tmp.Col)).FirstOrDefault();
            string txt = null;
            if (possibleHole != null)
            {
                txt = possibleHole.Number.ToString();
            } else if (possibleMarble != null)
            {
                txt = possibleMarble.Number.ToString();
            }

            using (Font myFont = new Font("Arial", 10))
            {
                if (txt != null)
                {
                    StringFormat drawFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(txt, myFont, Brushes.White, tmp.Width / 2, tmp.Height / 2, drawFormat);
                }
            }

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black, ButtonBorderStyle.Solid);
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
            // When form is closing, remove all files within the cache folder
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            // Open the MarbleExplorer - TODO: Edit with return type
            MarbleExplorer explorer = new MarbleExplorer(Environment.CurrentDirectory);
            explorer.ShowDialog();
        }

        public Bitmap[,] ReadImageData(string name, int factor)
        {
            Bitmap[,] parsedData = new Bitmap[factor, factor];
            Image img = Image.FromFile(name);
            int widthFactored = (int)((double)img.Width / factor + 0.5);
            int heightFactored = (int)((double)img.Height / factor + 0.5);
            for (int i = 0; i < factor; i++)
                for (int j = 0; j < factor; j++)
                {
                    parsedData[i, j] = new Bitmap(widthFactored, heightFactored);
                    Graphics g = Graphics.FromImage(parsedData[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthFactored, heightFactored), new Rectangle(j * widthFactored, i * heightFactored, widthFactored, heightFactored), GraphicsUnit.Pixel);
                    g.Dispose();
                }

            return parsedData;
        }

    }
}
