using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Parâmetros de movimento e rotação
    public float moveSpeed = 0.5f;
    public float changeDirectionInterval = 5f;
    public float maxDistanceFromCenter = 20f;
    public float smoothRotationSpeed = 0.3f;
    public float swimHeight = 0.5f;
    public float verticalSpeed = 0.5f;
    public float rotationChangeProbability = 0.1f;

    // Propriedades do peixe
    public string spriteName;
    public float rarity;
    private float speed;

    private Vector3 targetPosition;
    private float timer;
    private Quaternion targetRotation;
    private float initialYPosition;
    private bool hasBeenClicked = false;

    void Start()
    {
        SetNewTargetPosition();
        timer = changeDirectionInterval;
        targetRotation = transform.rotation;
        initialYPosition = transform.position.y;
        speed = 5f; // Defina uma velocidade padrão inicial
    }

    void Update()
    {
        // Movimento suave em direção ao alvo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        // Rotação suave em direção ao alvo
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothRotationSpeed);

        // Movimento vertical (opcional)
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * swimHeight;
        transform.position = new Vector3(transform.position.x, initialYPosition + yOffset, transform.position.z);

        // Atualiza o temporizador e muda de direção quando necessário
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetNewTargetPosition();
            if (Random.value < rotationChangeProbability)
            {
                targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
            }
            timer = changeDirectionInterval;
        }
    }

    void SetNewTargetPosition()
    {
        // Define uma nova posição aleatória dentro do raio máximo
        targetPosition = Random.insideUnitSphere * maxDistanceFromCenter + transform.position;
        targetPosition.y = initialYPosition; // Mantém a posição vertical constante
    }

    void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            int points = CalculatePointsFromRarity(rarity);
            ScoreManager.instance.AddScore(points);

            Collection c = GameManager.instance.collection;
            Card fishCard = c.GetCardByName(gameObject.name.Replace("(Clone)", ""));
            fishCard.collected = true;
        }
    }

    int CalculatePointsFromRarity(float rarityValue)
    {
        // Ajuste a escala de pontos conforme necessário
        float scale = 20f; // Aumente este valor para dar mais pontos aos peixes mais raros
        int basePoints = 5; // Pontos base para peixes comuns

        // Calcula os pontos com base na raridade (quanto mais raro, mais pontos)
        int points = Mathf.RoundToInt(basePoints + (1 - rarityValue) * scale); // Inverte a raridade

        return points;
    }

    public void Initialize(FishData fishData)
    {
        spriteName = fishData.spriteName;
        rarity = fishData.rarity;

        // Configura a velocidade com base na raridade (exemplo)
        float baseSpeed = 5f; // Velocidade base comum para todos os peixes
        float rarityMultiplier = 1f - rarity; // Quanto menor a raridade, maior a variação

        speed = baseSpeed * (1f + Random.Range(-0.2f, 0.2f) * rarityMultiplier);
    }
}








