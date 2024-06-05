using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour {
    public static GameObject cardObj;
    public static float rotationSpeed = 100f;
    public static bool isFrontDisplayed = true; // Flag para controlar qual lado do cartão está sendo exibido

    void Update() {
        if (Input.GetMouseButton(0)) {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, -rotationX, Space.World);
            transform.Rotate(Vector3.right, rotationY, Space.World);
        }
    }

    public static void DrawCard(Texture2D frontTexture, Texture2D backTexture) {
        // Criar o objeto da frente do card
        GameObject previewCardFrente = new GameObject("PreviewCardFrente");
        previewCardFrente.transform.SetParent(cardObj.transform, false);
        Image frenteImage = previewCardFrente.AddComponent<Image>();
        frenteImage.sprite = Sprite.Create(frontTexture, new Rect(0, 0, frontTexture.width, frontTexture.height), new Vector2(0.5f, 0.5f));
        previewCardFrente.SetActive(isFrontDisplayed); // Ativa a frente do cartão se estiver definido como verdadeiro

        // Criar o objeto do verso do card
        GameObject previewCardVerso = new GameObject("PreviewCardVerso");
        previewCardVerso.transform.SetParent(cardObj.transform, false);
        Image versoImage = previewCardVerso.AddComponent<Image>();
        versoImage.sprite = Sprite.Create(backTexture, new Rect(0, 0, backTexture.width, backTexture.height), new Vector2(0.5f, 0.5f));
        previewCardVerso.SetActive(!isFrontDisplayed); // Ativa o verso do cartão se estiver definido como falso
    }
}
