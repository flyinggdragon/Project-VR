using System.Collections;
using UnityEngine;

public class SimpleLocationManager : MonoBehaviour
{/*
    private bool permissionGranted = false;
    private LocationInfo currentLocation;

    void Start()
    {
        StartCoroutine(StartLocationService());
    }
    
    public IEnumerator StartLocationService()
    {   
        // Solicitar permissão de localização
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            yield return new WaitForSeconds(1);
        }

        // Verificar se a localização é ativada no dispositivo
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Localização não está ativada pelo usuário.");
            yield break;
        }

        // Iniciar a obtenção da localização
        Input.location.Start();

        // Aguarde até que a localização seja inicializada
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Se não foi possível inicializar a localização em 20 segundos, aborte
        if (maxWait <= 0)
        {
            Debug.Log("Timed out ao tentar inicializar a localização.");
            yield break;
        }

        // Verifique se obtivemos uma conexão válida
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // Permissão concedida e localização obtida
            permissionGranted = true;
            currentLocation = Input.location.lastData;
            Debug.Log("Localização obtida com sucesso.");

            LocationProximity.currentLatitude = currentLocation.latitude;
            LocationProximity.currentLongitude = currentLocation.longitude;
        }
    }

    void Update()
    {
        if (permissionGranted)
        {
            Debug.Log("Latitude: " + currentLocation.latitude + ", Longitude: " + currentLocation.longitude);
        }
    }

    void OnGUI()
    {
        if (permissionGranted)
        {
            GUI.Label(new Rect(10, 10, 200, 40), "Latitude: " + currentLocation.latitude + "\nLongitude: " + currentLocation.longitude);
        }
    }*/
}