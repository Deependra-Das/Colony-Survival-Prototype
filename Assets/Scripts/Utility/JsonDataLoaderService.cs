using System.IO;
using UnityEngine;

namespace ColonySurvivalPrototype.Utility
{
    public class JsonDataLoaderService
    {
        public T Load<T>(string fileName)
        {
            string path = Path.Combine(Application.streamingAssetsPath, fileName);

            if (!File.Exists(path))
            {
                Debug.LogError($"JSON file not found: {path}");
                return default;
            }

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
    }
}