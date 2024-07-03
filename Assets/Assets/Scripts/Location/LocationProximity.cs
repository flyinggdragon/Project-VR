using System.Collections.Generic;
using UnityEngine;

public class LocationProximity : MonoBehaviour
{
    [System.Serializable]
    public class Coordinates
    {
        public float latitude;
        public float longitude;
    }

    [System.Serializable]
    public class Location
    {
        public string locationName;
        public string type;
        public string region;
        public string fish;
        public Coordinates coordinates;
    }
}



