using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetsManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
    }
}