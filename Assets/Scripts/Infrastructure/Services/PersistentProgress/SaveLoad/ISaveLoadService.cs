using Data;

namespace Infrastructure.Services.PersistentProgress.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerProgress? LoadProgress();
        void SaveProgress();
    }
}