using System.IO;
using UnityEngine;

public static class SaveLoad
{
    public static ProjectileCollection LoadProjectileCollection()
    {
        ProjectileCollection collection;
        string path = Application.dataPath + "/Resources/ProjectileCollection.json";
        string json;
        if (!File.Exists(path))
        {
            //json = Resources.Load<TextAsset>("SampleProjectileCollection").text; // this works the same as the following line;
            json = File.ReadAllText(Application.dataPath + "/Resources/SampleProjectileCollection.json");
            File.WriteAllText(path, json);
        }
        else
        {
            json = File.ReadAllText(path);
        }
        collection = JsonUtility.FromJson<ProjectileCollection>(json);

        return collection;
    }
}
