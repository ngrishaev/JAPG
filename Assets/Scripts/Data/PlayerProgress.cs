namespace Data
{
    public class PlayerProgress
    {
        public WorldData WorldData { get; set; }

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
        }
    }
}