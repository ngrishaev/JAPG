using UnityEngine;

namespace Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector) => 
            new(vector.x, vector.y, vector.z);
        
        public static Vector3 AsVector3(this Vector3Data vector) =>
            new(vector.X, vector.Y, vector.Z);

        public static TTargetType ToDeserialized<TTargetType>(this string json) => 
            JsonUtility.FromJson<TTargetType>(json);
        
        public static string ToJson(this object obj) => 
            JsonUtility.ToJson(obj);
    }
}