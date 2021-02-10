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
    }
}