using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int points = 10; // Pontos adicionados ao destruir este objeto

    void Start()
    {
        
    }

    void Update()
    {
        // Verificar se há toques na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Verificar se o toque está em cima de um objeto de lixo
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        DestroyTrash();
                    }
                }
            }
        }
    }

    void OnMouseDown()
    {
        DestroyTrash();
    }

    private void DestroyTrash()
    {
        // Adicionar pontos ao score
        ScoreManager.instance.AddScore(points);
        // Diminuir o contador de lixo e atualizar o nível de limpeza
        ScoreManager.instance.DecreaseTrashCount();
        Destroy(gameObject);
        Debug.Log("Trash caught!");
    }
}

