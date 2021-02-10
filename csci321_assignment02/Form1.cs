using MarbleGame;
using MarbleGame.Models;
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
        Board board;
        Bitmap[,] imageData, boardRenderData;

        public App()
        {
            InitializeComponent();
            SetupData();
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("UP clicked");
            if (board != null)
            {
                // Solution#Routes also stores the move history, but we may need to replace string.history to store a localized copy
                board.LiftBoard(Sides.North, string.Empty);
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("DOWN clicked");
            if (board != null)
            {
                // Solution#Routes also stores the move history, but we may need to replace string.history to store a localized copy
                board.LiftBoard(Sides.South, string.Empty);
            }
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LEFT clicked");
            if (board != null)
            {
                // Solution#Routes also stores the move history, but we may need to replace string.history to store a localized copy
                board.LiftBoard(Sides.West, string.Empty);
            }
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("RIGHT clicked");
            if (board != null)
            {
                // Solution#Routes also stores the move history, but we may need to replace string.history to store a localized copy
                board.LiftBoard(Sides.East, string.Empty);
            }
        }

        public void SetupData()
        {
            Console.WriteLine("Setting up board based on pre-supplied data");
            Board board = new Board();
            board.InitializeBasicParams("puzzle.txt", "file");

            if (!(board.Size == 0 && board.MarblesCount == 0 && board.WallsCount == 0))
            {
                //Initialize marbles
                Console.WriteLine("Retrieving marble positions");
                board.InitializeBoardObjects(BoardObjectTypes.Marble, "puzzle.txt", "file");

                //Initialize holes
                Console.WriteLine("Retrieving hole positions");
                board.InitializeBoardObjects(BoardObjectTypes.Hole, "puzzle.txt", "file");

                //Initialize walls
                Console.WriteLine("Retrieving wall positions");
                board.InitializeBoardObjects("puzzle.txt", "file");
            }

            this.board = board;
            Console.WriteLine("Puzzle Data Read success, continuing to read Image Data...");
            imageData = ReadImageData("puzzle.jpg", 7);
            UpdateImageData();
        }

        public void UpdateImageData()
        {
            boardRenderData = new Bitmap[board.Size, board.Size];
            if (imageData != null)
            {
                // TODO: Assign images to form based on the layout of puzzle.txt (board.Holes, board.Walls, and board.Marbles will help here)
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
                                    boardRenderData[row, column] = imageData[1, 1];
                                } else
                                {
                                    // Bottom Walls
                                    boardRenderData[row, column] = imageData[0, 4];
                                }
                            }
                            else if (column == 0)
                            {
                                // Left Side Data
                                if (row == 0)
                                {
                                    // TODO
                                }
                                else if (row == board.Size - 1)
                                {
                                    // TODO
                                } else
                                {
                                    // TODO
                                }
                            }
                            else if (column == board.Size - 1)
                            {
                                // Right Side Data
                                if (row == 0)
                                {
                                    // TODO
                                }
                                else if (row == board.Size - 1)
                                {
                                    // TODO
                                } else
                                {
                                    // TODO
                                }
                            } else
                            {
                                // All other Empty space
                                boardRenderData[row, column] = imageData[0, 0];
                            }
                        } else if (possibleWall == null)
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
                            // Marble with West Wall
                            if (possibleWall.GetRenderSide() == Sides.West)
                            {
                                if (possibleMarble != null)
                                {
                                    boardRenderData[row, column] = imageData[5, 4];
                                } else if (possibleHole != null)
                                {
                                    boardRenderData[row, column] = imageData[3, 2];
                                }
                            } else if (possibleWall.GetRenderSide() == Sides.East)
                            {
                                // TODO
                            } else if (possibleWall.GetRenderSide() == Sides.North)
                            {
                                // TODO
                            } else if (possibleWall.GetRenderSide() == Sides.South)
                            {
                                // TODO
                            } else
                            {
                                // If the Side is Unknown, we'll check if we're at edge of bounds for wall generation
                                if (possibleMarble != null)
                                {
                                    // TODO
                                } else if (possibleHole != null)
                                {
                                    // TODO
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
                    //Graphics g = Graphics.FromImage(parsedData[i, j]);
                    //g.DrawImage(img, new Rectangle(0, 0, widthFactored, heightFactored), new Rectangle(j * widthFactored, i * heightFactored, widthFactored, heightFactored), GraphicsUnit.Pixel);
                    //g.Dispose();
                }

            return parsedData;
        }

        public void ConsoleTest()
        {
            try
            {
                var board = new Board();
                Console.WriteLine("Please enter 'N M W' params");
                board.InitializeBasicParams();

                for (int i = 0; !(board.Size == 0 && board.MarblesCount == 0 && board.WallsCount == 0); i++)
                {
                    //Initialize marbles
                    Console.WriteLine("Please enter marble positions");
                    board.InitializeBoardObjects(BoardObjectTypes.Marble);

                    //Initialize holes
                    Console.WriteLine("Please enter hole positions");
                    board.InitializeBoardObjects(BoardObjectTypes.Hole);

                    //Initialize walls
                    Console.WriteLine("Please enter wall positions");
                    board.InitializeBoardObjects();

                    //check for possible solution
                    var solution = board.FindRoute(string.Empty);
                    Console.WriteLine("Case {0}: {1}\n", i + 1, solution.Success ? solution.Route.Length + " moves " + solution.Route : "impossible");

                    //try again
                    Console.WriteLine("Please enter 'N M W' params");
                    board = new Board();
                    board.InitializeBasicParams();
                }

                Console.WriteLine("You finished the test!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
