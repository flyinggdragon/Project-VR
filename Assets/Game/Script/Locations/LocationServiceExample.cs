using UnityEngine;
using System.Collections;

public class LocationServiceExample : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        // Verificar se a permissão de localização foi concedida
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("A localização não está habilitada pelo usuário.");
            yield break;
        }

        // Inicializar o serviço de localização
        Input.location.Start();

        // Esperar até que o serviço de localização esteja inicializado
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Verificar se a inicialização falhou
        if (maxWait < 1)
        {
            Debug.Log("Tempo limite atingido.");
            yield break;
        }

        // Verificar se houve falha no serviço de localização
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Não foi possível determinar a localização.");
            yield break;
        }
        else
        {
            // Sucesso - Acesso à localização permitida
            Debug.Log("Localização: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
            CheckLocationInRioGrandeDoSul(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }

        // Parar o serviço de localização se não for mais necessário
        Input.location.Stop();
    }

    void CheckLocationInRioGrandeDoSul(double latitude, double longitude)
    {
        // Definindo os limites aproximados do Rio Grande do Sul
        double minLatitude = -34.0;
        double maxLatitude = -30.0;
        double minLongitude = -57.0;
        double maxLongitude = -49.0;

        if (latitude >= minLatitude && latitude <= maxLatitude &&
            longitude >= minLongitude && longitude <= maxLongitude)
        {
            Debug.Log("O dispositivo está no Rio Grande do Sul.");
        }
        else
        {
            Debug.Log("O dispositivo NÃO está no Rio Grande do Sul.");
        }
    }
}
