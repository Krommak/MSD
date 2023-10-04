using System.Threading.Tasks;
using UnityEngine;

namespace Game.Loader
{
    public static class AssetLoader
    {
        public async static Task<T> LoadAsset<T>(string path) where T : Object
        {
            var task = new Task<T>(() =>
            {
                return Resources.Load<T>(path);
            });

            await task;

            return task.Result;
        }
    }
}