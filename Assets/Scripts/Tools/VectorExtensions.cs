using UnityEngine;

namespace Tools
{
    public static class VectorExtensions
    {
        public static Vector2 WithX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }

        public static Vector2 WithY(this Vector2 vector, float y)
        {
            return new Vector2(vector.x, y);
        }
        
        public static Vector3 WithZ(this Vector2 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector3 WithX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        public static Vector3 WithY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
        
        public static bool IsCloseEnough(this Vector2 from, Vector2 to, float threshold = 0.001f)
        {
            return (from - to).sqrMagnitude < threshold * threshold;
        }
        
        public static bool IsCloseEnough(this Vector3 from, Vector3 to, float threshold = 0.001f)
        {
            return (from - to).sqrMagnitude < threshold * threshold;
        }
    }
}