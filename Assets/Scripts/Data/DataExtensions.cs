namespace Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this UnityEngine.Vector3 vector) => 
            new(vector.x, vector.y, vector.z);
        
        public static UnityEngine.Vector3 AsVector3(this Vector3Data vector) =>
            new(vector.X, vector.Y, vector.Z);
    }
}