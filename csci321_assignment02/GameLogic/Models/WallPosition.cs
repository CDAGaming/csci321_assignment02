using System;

namespace MarbleGame.Models
{
    public class WallPosition
    {
        public Position FirstSide { get; set; }
        public Position SecondSide { get; set; }

        public Sides GetRenderSide(int row, int column)
        {
            if (FirstSide.Row == SecondSide.Row)
            {
                int colDistance = Math.Abs(FirstSide.Column - SecondSide.Column);
                return (colDistance > 0 && column == FirstSide.Column) ? Sides.East : Sides.West;
            } else if (FirstSide.Column == SecondSide.Column)
            {
                int rowDistance = Math.Abs(FirstSide.Row - SecondSide.Row);
                return (rowDistance > 0 && row == FirstSide.Row) ? Sides.North : Sides.South;
            } else
            {
                return Sides.Unknown;
            }
        }
    }
}