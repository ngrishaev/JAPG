using Data;

namespace Infrastructure.Services.PersistentProgress
{
    public interface IProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface IProgressUpdater
    {
        void UpdateProgress(PlayerProgress progress);
    }
}