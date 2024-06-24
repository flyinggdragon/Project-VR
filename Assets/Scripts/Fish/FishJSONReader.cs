using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace MyGame.Fish // Defina o mesmo namespace para FishData
{
    public static class FishJSONReader
    {
        public static List<FishData> ReadFishDataFromJSON()
        {
            string filePath = "FishData";

            TextAsset jsonFile = Resources.Load<TextAsset>(filePath);

            if (jsonFile == null)
            {
                Debug.LogError($"Could not find file at {filePath}");
                return null;
            }

            string json = jsonFile.text;

            FishList fishList = JsonUtility.FromJson<FishList>(json);
            return fishList != null ? fishList.fish : new List<FishData>();
        }
    }

    [System.Serializable]
    public class FishList
    {
        public List<FishData> fish;
    }

    [System.Serializable]
    public class FishData
    {
        public string spriteName;
        public string fishName;
        public string popularName;
        public string scientificName;
        public float rarity;
        public string description;
    }
}

