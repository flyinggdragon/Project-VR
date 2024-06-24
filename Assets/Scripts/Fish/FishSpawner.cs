using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab; // Prefab do peixe que será spawnado
    public string jsonFileName = "fish_data.json"; // Nome do arquivo JSON onde os dados estão armazenados
    public Transform spawnArea; // Área onde os peixes serão spawnados

    private List<FishData> fishDataList = new List<FishData>(); // Lista para armazenar os dados dos peixes

    void Start()
    {
        // Carregar dados do JSON
        LoadFishData();

        // Spawnar os peixes com base nos dados carregados
        SpawnFish();
    }

    void LoadFishData()
    {
        // Carregar o JSON do arquivo
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile != null)
        {
            // Converter o JSON para uma lista de objetos FishData
            FishData[] fishDataArray = JsonUtility.FromJson<FishData[]>(jsonFile.text);
            fishDataList.AddRange(fishDataArray);
        }
        else
        {
            Debug.LogError("Failed to load JSON file: " + jsonFileName);
        }
    }

    void SpawnFish()
    {
        foreach (FishData fishData in fishDataList)
        {
            // Calcular se o peixe deve ser spawnado com base na raridade
            float spawnChance = Random.Range(0f, 1f);

            if (spawnChance <= fishData.rarity)
            {
                // Spawnar o peixe apenas se o número aleatório for menor ou igual à raridade
                Vector3 spawnPosition = spawnArea.position + Random.insideUnitSphere * 10f; // Spawn dentro de uma esfera de raio 10
                GameObject newFishObject = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
                Fish newFish = newFishObject.GetComponent<Fish>();

                // Configurar os dados do peixe diretamente no script Fish
                newFish.spriteName = fishData.spriteName;
                newFish.fishName = fishData.fishName;
                newFish.popularName = fishData.popularName;
                newFish.scientificName = fishData.scientificName;
                newFish.rarity = fishData.rarity;
                newFish.description = fishData.description;
            }
        }
    }

}

