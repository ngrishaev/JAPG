namespace Data
{
    public class PlayerProgress
    {
        private readonly string _initialLevel;
        public WorldData WorldData { get; set; }

        public PlayerProgress(string initialLevel)
        {
            _initialLevel = initialLevel;
        }
    }
}