namespace Tetris3D.Logic
{
    public class MovementRestrictions
    {
        public float maximum_x;
        public float maximum_y;
        public float maximum_z;
        public float minimum_x;
        public float minimum_y;
        public float minimum_z;

        public MovementRestrictions(
            float minimumX,
            float maximumX,
            float minimumY,
            float maximumY,
            float minimumZ,
            float maximumZ
        )
        {
            this.maximum_x = maximumX;
            this.maximum_y = maximumY;
            this.maximum_z = maximumZ;
            this.minimum_x = minimumX;
            this.minimum_y = minimumY;
            this.minimum_z = minimumZ;
        }
    }
}
