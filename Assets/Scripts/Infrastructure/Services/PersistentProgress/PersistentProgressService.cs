using Data;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress? ProgressData { get; set; }
    }
}