namespace Redox.API.Player
{
   #pragma warning disable CS0659
    
    /// <summary>
    /// Represents a generic vector point.
    /// </summary>
    public class Position
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is Position))
                return false;

            Position pos = (Position)obj;
            return (this.X == pos.X) && (this.Y == pos.Y) && (this.Z == pos.Z);
        }
    }
}