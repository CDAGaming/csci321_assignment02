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
        Bitmap[,] imageData;

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
            if (imageData != null)
            {
                // TODO: Assign images to form based on the layout of puzzle.txt (board.Holes, board.Walls, and board.Marbles will help here)
                for (int row = 0; row < board.Size; row++)
                    for (int column = 0; column < board.Size; column++)
                    {
                        Marble possibleMarble = board.Marbles.Where(m => m.Position.Row == row && m.Position.Column == column).First();
                        Hole possibleHole = board.Holes.Where(h => (h.Position.Row == row && h.Position.Column == column)).First();
                        WallPosition possibleWall_FirstSide = board.Walls.Where(w => (w.FirstSide.Row == row && w.FirstSide.Column == column)).First();
                        WallPosition possibleWall_SecondSide = board.Walls.Where(w => (w.SecondSide.Row == row && w.SecondSide.Column == column)).First();
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
