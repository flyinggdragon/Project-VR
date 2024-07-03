using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public static FishSpawner Instance;

    public List<GameObject> fishPrefabs; // Lista de prefabs dos peixes
    public int maxNumberOfFish = 10; // Número máximo de peixes

    public List<Transform> spawnPoints; // Pontos de spawn para os peixes

    private List<GameObject> spawnedFish = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SpawnFish();
    }

    void SpawnFish()
    {
        for (int i = 0; i < maxNumberOfFish; i++)
        {
            SpawnSingleFish();
        }
    }

    void SpawnSingleFish()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject fishPrefab = GetRandomFishPrefab();

        // Gera uma rotação aleatória para o peixe
        Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        GameObject newFish = Instantiate(fishPrefab, spawnPosition, spawnRotation);
        spawnedFish.Add(newFish);

        // Inicializa o peixe com os dados do FishData
        FishData fishData = GetRandomFishData();
        Fish fishComponent = newFish.GetComponent<Fish>();
        if (fishComponent != null)
        {
            fishComponent.Initialize(fishData);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Seleciona aleatoriamente um ponto de spawn da lista de spawnPoints
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        
        // Obtém a caixa de colisão do ponto de spawn (caso seja um Collider)
        Collider spawnCollider = randomSpawnPoint.GetComponent<Collider>();
        if (spawnCollider == null)
        {
            Debug.LogError("Collider não encontrado no ponto de spawn: " + randomSpawnPoint.name);
            return randomSpawnPoint.position; // Retorna a posição do ponto de spawn caso não haja um Collider
        }

        // Gera uma posição aleatória dentro da caixa de colisão
        Vector3 randomPointInCollider = spawnCollider.bounds.center + new Vector3(
            Random.Range(-spawnCollider.bounds.extents.x, spawnCollider.bounds.extents.x),
            Random.Range(-spawnCollider.bounds.extents.y, spawnCollider.bounds.extents.y),
            Random.Range(-spawnCollider.bounds.extents.z, spawnCollider.bounds.extents.z)
        );

        return randomPointInCollider;
    }

    GameObject GetRandomFishPrefab()
    {
        // Retorna um prefab de peixe aleatório da lista
        return fishPrefabs[Random.Range(0, fishPrefabs.Count)];
    }

    FishData GetRandomFishData()
    {
        // Método para retornar um FishData aleatório para inicializar o peixe
        // Este método deve ser implementado para carregar os dados do JSON
        // Aqui você precisa implementar a lógica para obter os dados do JSON
        return new FishData();
    }

    public void FishDestroyed(GameObject fishObject)
    {
        if (spawnedFish.Contains(fishObject))
        {
            spawnedFish.Remove(fishObject);

            // Verifica se é necessário spawnar um novo peixe para substituir o que foi destruído
            if (spawnedFish.Count < maxNumberOfFish)
            {
                SpawnSingleFish();
            }
        }
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
                if (spawnedFish[i] == null || spawnedFish[j] == null)
                {
                    continue;
                }

                GameObject fish1 = spawnedFish[i];
                GameObject fish2 = spawnedFish[j];

                // Calcula a distância entre os peixes
                float distance = Vector3.Distance(fish1.transform.position, fish2.transform.position);

                // Se a distância entre os peixes for menor que a distância mínima desejada, ajusta a posição do segundo peixe
                if (distance < 1f) // Ajuste a distância mínima aqui
                {
                    Vector3 direction = (fish2.transform.position - fish1.transform.position).normalized;
                    fish2.transform.position = fish1.transform.position + direction * 1f; // Ajuste a distância mínima aqui
                }
            }
        }
    }
}

