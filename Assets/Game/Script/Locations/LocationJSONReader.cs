using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public static class LocationJSONReader {
    private static string jsonFilePath = "Assets/Game/Script/JSON/LocationData.json"; 

    public static List<LocationProximity.Location> ReadLocationDataFromJSON() {
        string jsonText = File.ReadAllText(jsonFilePath);
        LocationListWrapper wrapper = JsonUtility.FromJson<LocationListWrapper>(jsonText);
        
        return wrapper.location;
    }

    [System.Serializable]
    public class LocationListWrapper {
        public List<LocationProximity.Location> location;
    }
}