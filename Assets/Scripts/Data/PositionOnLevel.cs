using System;

namespace Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data? Position = null;

        public PositionOnLevel(string initialLevel)
        {
            Level = initialLevel;
        }

        public PositionOnLevel(string level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }
    }
}