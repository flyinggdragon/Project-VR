using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Velocidade de movimento reduzida para suavidade
    public float changeDirectionInterval = 5f; // Intervalo maior para mudar de direção
    public float maxDistanceFromCenter = 20f; // Área maior para se mover
    public float smoothRotationSpeed = 0.3f; // Velocidade de rotação mais suave
    public float swimHeight = 0.5f; // Amplitude da oscilação vertical
    public float verticalSpeed = 0.5f; // Velocidade do movimento vertical
    public float rotationChangeProbability = 0.1f; // Probabilidade de mudar de rotação

    private Vector3 targetPosition;
    private float timer;
    private Quaternion targetRotation;
    private float initialYPosition;
    private bool hasBeenClicked = false; // Verifica se o peixe já foi clicado

    void Start()
    {
        // Define a posição inicial dentro de uma esfera centrada na posição inicial do Fish
        SetNewTargetPosition();

        // Inicializa o timer
        timer = changeDirectionInterval;

        // Inicializa a rotação inicial
        targetRotation = transform.rotation;

        // Salva a posição inicial no eixo Y
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        // Movimento suave em direção ao ponto alvo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Rotação suave em direção ao movimento
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothRotationSpeed);

        // Ajusta a altura do movimento
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * swimHeight;
        transform.position = new Vector3(transform.position.x, initialYPosition + yOffset, transform.position.z);

        // Atualiza o timer
        timer -= Time.deltaTime;

        // Se o timer atingir zero, verifica se deve mudar de direção
        if (timer <= 0f)
        {
            // Define uma nova posição alvo e, com base na probabilidade, talvez mude a rotação
            SetNewTargetPosition();

            // Determina aleatoriamente se deve mudar de rotação com base na probabilidade
            if (Random.value < rotationChangeProbability)
            {
                // Gera uma nova rotação em direção ao ponto alvo
                targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
            }

            // Reseta o timer
            timer = changeDirectionInterval;
        }
    }

    void SetNewTargetPosition()
    {
        // Gera uma nova posição alvo dentro de uma esfera centrada na posição inicial do Fish
        targetPosition = Random.insideUnitSphere * maxDistanceFromCenter + transform.position;
        targetPosition.y = initialYPosition; // Mantém a mesma altura inicial
    }

    void OnMouseDown()
    {
        // Verifica se o peixe já foi clicado antes
        if (!hasBeenClicked)
        {
            hasBeenClicked = true; // Marca que o peixe foi clicado

            // Aumenta a pontuação
            ScoreManager.instance.AddScore(5);
            Debug.Log("Fish caught! Score increased.");

            // Aqui você pode adicionar quaisquer outras ações que deseja executar ao clicar no peixe pela primeira vez

            // Não destruímos o peixe para permitir interações futuras sem alterar a pontuação
        }
        else
        {
            Debug.Log("Fish has already been caught!");
        }
    }
}










