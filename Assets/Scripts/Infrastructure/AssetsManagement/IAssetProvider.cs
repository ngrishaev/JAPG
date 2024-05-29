using UnityEngine;

namespace Infrastructure.AssetsManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, Vector3 at);
    }
}