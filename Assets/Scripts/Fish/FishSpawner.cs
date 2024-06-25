using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public List<GameObject> fishPrefabs; // Lista de prefabs dos peixes
    public int numberOfFish = 10; // Número de peixes a serem spawnados
    public float spawnRadius = 10f; // Raio dentro do qual os peixes são spawnados
    public float minDistanceBetweenFish = 1f; // Distância mínima entre os peixes

    private List<GameObject> spawnedFish = new List<GameObject>();

    void Start()
    {
        SpawnFish();
    }

    void SpawnFish()
    {
        // Carrega o JSON de fishData
        string jsonString = Resources.Load<TextAsset>("fishData").text;
        FishDataList fishDataList = JsonUtility.FromJson<FishDataList>(jsonString);

        for (int i = 0; i < numberOfFish; i++)
        {
            // Seleciona um peixe aleatório com base na raridade
            FishData randomFishData = GetRandomFishData(fishDataList.fish);

            // Instancia o prefab do peixe correspondente
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            GameObject fishPrefab = GetFishPrefab(randomFishData.spriteName);
            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

            // Inicializa o peixe com os dados do FishData
            newFish.GetComponent<Fish>().Initialize(randomFishData);

            spawnedFish.Add(newFish);
        }
    }

    FishData GetRandomFishData(List<FishData> fishDataList)
    {
        float totalRarity = 0f;

        // Calcula a soma total das raridades para escolher um peixe aleatório ponderado
        foreach (var fishData in fishDataList)
        {
            totalRarity += fishData.rarity;
        }

        // Escolhe um número aleatório dentro do intervalo total de raridade
        float randomValue = Random.Range(0f, totalRarity);
        float cumulativeRarity = 0f;

        // Encontra o peixe com base no valor aleatório escolhido
        foreach (var fishData in fishDataList)
        {
            cumulativeRarity += fishData.rarity;
            if (randomValue <= cumulativeRarity)
            {
                return fishData;
            }
        }

        // Caso não encontre, retorna o primeiro da lista (não deve ocorrer com probabilidades corretamente definidas)
        return fishDataList[0];
    }

    GameObject GetFishPrefab(string spriteName)
    {
        // Procura na lista de prefabs pelo prefab com o nome do sprite correspondente
        foreach (var prefab in fishPrefabs)
        {
            if (prefab.GetComponent<Fish>().spriteName == spriteName)
            {
                return prefab;
            }
        }

        // Caso não encontre, retorna o primeiro da lista (ou um padrão)
        return fishPrefabs[0];
    }

    void Update()
    {
        // Verificar colisão entre os peixes
        CheckFishCollisions();
    }

    void CheckFishCollisions()
    {
        for (int i = 0; i < spawnedFish.Count; i++)
        {
            for (int j = i + 1; j < spawnedFish.Count; j++)
            {
                GameObject fish1 = spawnedFish[i];
                GameObject fish2 = spawnedFish[j];

                // Calcula a distância entre os peixes
                float distance = Vector3.Distance(fish1.transform.position, fish2.transform.position);

                // Se a distância entre os peixes for menor que a distância mínima desejada, ajusta a posição do segundo peixe
                if (distance < minDistanceBetweenFish)
                {
                    Vector3 direction = (fish2.transform.position - fish1.transform.position).normalized;
                    fish2.transform.position = fish1.transform.position + direction * minDistanceBetweenFish;
                }
            }
        }
    }
}

