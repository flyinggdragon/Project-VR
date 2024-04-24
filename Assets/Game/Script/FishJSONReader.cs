using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class FishJSONReader : MonoBehaviour {

    private string jsonFilePath = "Assets/Game/Script/JSON/FishData.json"; 
    [SerializeField] public List<FishData> fishList;

    void Start() {
        fishList = ReadFishDataFromJSON(jsonFilePath);
    }

    private List<FishData> ReadFishDataFromJSON(string path) {
        string jsonText = File.ReadAllText(path);
        FishListWrapper wrapper = JsonUtility.FromJson<FishListWrapper>(jsonText);
        
        return wrapper.fish;
    }

    [System.Serializable]
    public class FishListWrapper {
        public List<FishData> fish;
    }
}