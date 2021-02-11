using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csci321_assignment02
{
    class GridBox : System.Windows.Forms.PictureBox
    {
        private int row;
        private int col;
        private int ballNum;
        private int holeNum;
        private int wallCount;
        private int item; // 0 = empty; 1 = ball; 2 = hole; -1 = error
        private int leftWall;
        private int rightWall;
        private int topWall;
        private int bottomWall;

        public bool HasBall()
        {
            return item == 1;
        }

        public bool HasLeftWall()
        {
            return leftWall == 1;
        }

        public bool HasRightWall()
        {
            return rightWall == 1;
        }

        public bool HasTopWall()
        {
            return topWall == 1;
        }

        public bool HasBottomWall()
        {
            return bottomWall == 1;
        }

        public int Row
        {
            set
            {
                row = value;
            }
            get
            {
                return row;
            }
        }

        public int Col
        {
            set
            {
                col = value;
            }
            get
            {
                return col;
            }
        }

        public int BallNum
        {
            set
            {
                ballNum = value;
            }
            get
            {
                return ballNum;
            }
        }

        public int HoleNum
        {
            set
            {
                holeNum = value;
            }
            get
            {
                return holeNum;
            }
        }

        public int WallCount
        {
            set
            {
                wallCount = value;
            }
            get
            {
                return wallCount;
            }
        }

        public int Item
        {
            set
            {
                item = value;
            }
            get
            {
                return item;
            }
        }

        public int LeftWall
        {
            set
            {
                leftWall = value;
            }
            get
            {
                return leftWall;
            }
        }

        public int RightWall
        {
            set
            {
                rightWall = value;
            }
            get
            {
                return rightWall;
            }
        }

        public int TopWall
        {
            set
            {
                topWall = value;
            }
            get
            {
                return topWall;
            }
        }

        public int BottomWall
        {
            set
            {
                bottomWall = value;
            }
            get
            {
                return bottomWall;
            }
        }

    }
}
