using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int points = 100; // Pontos adicionados ao destruir este objeto

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
        ScoreManager.instance.AddScore(points);
        ScoreManager.instance.DecreaseTrashCount();
        Destroy(gameObject);
    }


}


