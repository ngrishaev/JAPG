using Data;

namespace Infrastructure.Services.PersistentProgress
{
    public interface IProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface IProgressWriter
    {
        void WriteProgress(PlayerProgress progress);
    }
}