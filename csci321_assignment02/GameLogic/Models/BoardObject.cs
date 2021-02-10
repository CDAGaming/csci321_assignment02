namespace MarbleGame.Models
{
    public class BoardObject
    {
        public int Number { get; set; }
        public Position Position { get; set; }

        // WARN : Do not use with WallPosition's
        public BorderSides GetBorderSide(int boardSize)
        {
            if (Position.Column <= 0)
            {
                if (Position.Row <= 0)
                {
                    return BorderSides.LeftTop;
                }
                else if (Position.Row >= boardSize)
                {
                    return BorderSides.LeftBottom;
                }
                else
                {
                    return BorderSides.Left;
                }
            }
            else if (Position.Column >= boardSize)
            {
                if (Position.Row <= 0)
                {
                    return BorderSides.RightTop;
                }
                else if (Position.Row >= boardSize)
                {
                    return BorderSides.RightBottom;
                }
                else
                {
                    return BorderSides.Right;
                }
            }
            else
            {
                if (Position.Column > 0 && Position.Column < boardSize)
                {
                    if (Position.Row == 0)
                    {
                        return BorderSides.Top;
                    }
                    else if (Position.Row == boardSize)
                    {
                        return BorderSides.Bottom;
                    }
                    else
                    {
                        return BorderSides.Unknown; // Should never be reached
                    }
                }
                else
                {
                    return BorderSides.Unknown; // Should never be reached
                }
            }
        }
    }
}