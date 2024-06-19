using UnityEngine;
using System.Collections.Generic;

public class LocationProximity : MonoBehaviour
{
    public TextAsset locationJson; // Assumindo que o JSON é importado como um TextAsset
    public float proximityRadius = 1000.0f; // Raio de proximidade em metros
    List<Location> locations;

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

    [System.Serializable]
    public class LocationList
    {
        public List<Location> location;
    }

    private LocationList locationData;

    void Start()
    {
        //locationData = JsonUtility.FromJson<LocationList>(locationJson.text);
        locations = LocationJSONReader.ReadLocationDataFromJSON();
    }

    void Update() {
        // Pegar as coordenadas atuais do usuário (substitua pelo método que você usa para pegar as coordenadas)
        float currentLatitude = -27.8701f;
        float currentLongitude = -54.4834f;

        Coordinates coord = new Coordinates { latitude = currentLatitude, longitude = currentLongitude };

        GameManager.currentLocation = ReturnClosestLocation(coord);
    }

    private Location ReturnClosestLocation(Coordinates coord) {
        Location closestLocation = locations[0];
        float lowestDistance = 999999999999.0f; // Infinito

        foreach (var loc in locations) {
            float currentDistance = CalculateDistance(
                loc.coordinates.longitude, loc.coordinates.latitude, coord.longitude, coord.latitude
                );

            if (currentDistance < lowestDistance) {
                lowestDistance = currentDistance;
                closestLocation = loc;
            }
        }

        return closestLocation;
    }

    private float CalculateDistance(float lat1, float lon1, float lat2, float lon2)
    {
        float R = 6371e3f; // Raio da Terra em metros
        float φ1 = lat1 * Mathf.Deg2Rad;
        float φ2 = lat2 * Mathf.Deg2Rad;
        float Δφ = (lat2 - lat1) * Mathf.Deg2Rad;
        float Δλ = (lon2 - lon1) * Mathf.Deg2Rad;

        float a = Mathf.Sin(Δφ / 2) * Mathf.Sin(Δφ / 2) +
                  Mathf.Cos(φ1) * Mathf.Cos(φ2) *
                  Mathf.Sin(Δλ / 2) * Mathf.Sin(Δλ / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        float distance = R * c;
        return distance;
    }

    private float GetCurrentLatitude()
    {
        // Método para pegar a latitude atual do usuário
        return 0.0f;
    }

    private float GetCurrentLongitude()
    {
        // Método para pegar a longitude atual do usuário
        return 0.0f;
    }
}