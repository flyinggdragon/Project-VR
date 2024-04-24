using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Fish : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float changeDirectionInterval = 2f;
    public float maxDistanceFromCenter = 5f; // Define a distância máxima do centro para a movimentação
    public float smoothRotationSpeed = 2f; // Velocidade de rotação suave

    private Vector3 moveDirection;
    private Vector3 targetPosition;
    private float timer;
    private Quaternion targetRotation;

    //public FishData fishData;

    // Start is called before the first frame update
    void Start()
    {
        // Define a posição inicial dentro de uma esfera centrada na posição atual do Fish
        targetPosition = Random.insideUnitSphere * maxDistanceFromCenter + transform.position;
        targetPosition.y = transform.position.y; // Mantém a mesma altura

        // Inicializa o timer
        timer = changeDirectionInterval;

        // Inicializa a rotação inicial
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento suave em direção ao ponto alvo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Rotação suave em direção ao movimento
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothRotationSpeed);

        // Atualiza o timer
        timer -= Time.deltaTime;

        // Se o timer atingir zero, define um novo ponto alvo dentro da área definida
        if (timer <= 0f)
        {
            // Gera uma nova posição alvo dentro de uma esfera centrada na posição atual do Fish
            targetPosition = Random.insideUnitSphere * maxDistanceFromCenter + transform.position;
            targetPosition.y = transform.position.y; // Mantém a mesma altura

            // Gera uma nova rotação em direção ao ponto alvo
            targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);

            // Reseta o timer
            timer = changeDirectionInterval;
        }
    }
}





