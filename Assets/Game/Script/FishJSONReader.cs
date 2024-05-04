using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public static class FishJSONReader {
    private static string jsonFilePath = "Assets/Game/Script/JSON/FishData.json"; 

    public static List<FishData> ReadFishDataFromJSON() {
        string jsonText = File.ReadAllText(jsonFilePath);
        FishListWrapper wrapper = JsonUtility.FromJson<FishListWrapper>(jsonText);
        
        return wrapper.fish;
    }

    [System.Serializable]
    public class FishListWrapper {
        public List<FishData> fish;
    }
}