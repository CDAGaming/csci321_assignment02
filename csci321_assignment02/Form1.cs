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

        public App()
        {
            InitializeComponent();
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
