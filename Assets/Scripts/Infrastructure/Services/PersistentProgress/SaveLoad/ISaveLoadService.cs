using System.Collections.Generic;
using Data;

namespace Infrastructure.Services.PersistentProgress.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerProgress? LoadProgress();
        void SaveProgress();
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressWriter> ProgressWriters { get; }
        void RegisterWriter(IProgressWriter reader);
        void RegisterReader(IProgressReader reader);
        void CleanUp();
    }
}