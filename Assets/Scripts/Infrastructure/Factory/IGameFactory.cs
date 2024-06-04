using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressWriter> ProgressWriters { get; }
        GameObject? Hero { get; }
        GameObject CreateHero(GameObject at);
        void CleanUp();
        event Action<GameObject>? OnHeroCreated;
    }
}