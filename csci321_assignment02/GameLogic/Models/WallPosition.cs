namespace MarbleGame.Models
{
    public class WallPosition
    {
        public Position FirstSide { get; set; }
        public Position SecondSide { get; set; }

        public Sides GetRenderSide()
        {
            if (SecondSide.Column > FirstSide.Column)
            {
                return Sides.West;
            } else if (SecondSide.Column < FirstSide.Column)
            {
                return Sides.East;
            } else if (SecondSide.Row > FirstSide.Row)
            {
                return Sides.North;
            } else if (SecondSide.Row < FirstSide.Row)
            {
                return Sides.West;
            } else
            {
                return Sides.Unknown;
            }
        }
    }
}