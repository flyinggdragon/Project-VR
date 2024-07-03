using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Parâmetros de movimento e rotação
    public float moveSpeed = 0.5f;
    public float maxDistanceFromCenter = 20f;
    public float smoothRotationSpeed = 0.3f;
    public float swimHeight = 0.5f;
    public float verticalSpeed = 0.5f;

    // Propriedades do peixe
    public string spriteName;
    public float rarity;
    private float speed;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float initialYPosition;
    private bool hasBeenClicked = false;

    void Start()
    {
        initialYPosition = transform.position.y;
        SetNewTargetPosition();
        InitializeSpeed();
    }

    void Update()
    {
        // Movimento contínuo em direção ao alvo
        transform.position += -transform.forward * Time.deltaTime * speed;

        // Rotação suave em direção ao alvo
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothRotationSpeed);

        // Movimento vertical (opcional)
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * swimHeight;
        transform.position = new Vector3(transform.position.x, initialYPosition + yOffset, transform.position.z);

        // Checa a distância até o alvo e define um novo alvo se estiver próximo o suficiente
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        // Define uma nova posição aleatória dentro do raio máximo
        targetPosition = Random.insideUnitSphere * maxDistanceFromCenter + transform.position;
        targetPosition.y = initialYPosition; // Mantém a posição vertical constante

        // Define a nova rotação para olhar em direção ao novo alvo
        targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
    }

    void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;

            Collection c = GameManager.instance.collection;
            Card fishCard = c.GetCardByName(gameObject.name.Replace("(Clone)", ""));
            fishCard.collected = true;
            
            int points;

            if (rarity > 0.3) {
                points = 150;
            } else if (rarity < 0.1) {
                points = 600;
            } else {
                points = 300;
            }

            ScoreManager.instance.AddScore(points);
        }
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

    void InitializeSpeed()
    {
        // Ajusta a velocidade para ser mais rápida ou mais lenta aleatoriamente
        moveSpeed *= Random.Range(0.5f, 1.5f);
    }
}









