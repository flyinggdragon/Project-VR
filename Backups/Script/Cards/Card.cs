using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card {
    public Texture2D textureFront;
    public Texture2D textureBack;
    public FishData data; // Após o fim dos testes, torná-lo privado.
    public bool collected;
    private Rarity rarity;
    private GameObject cardObj;
    public GameObject lockObj;
    private static Texture2D lockTex;
    private static Texture2D[] backTextures;

    public enum Rarity {
        Common,
        Rare,
        Epic
    }

    public Card(Texture2D tex, FishData fish) {
        textureFront = tex;
        data = fish;
        collected = fish.collected;

        lockTex = Resources.Load<Texture2D>("spritesGerais/bloqueadaCard");
        backTextures = new Texture2D[] {
            Resources.Load<Texture2D>("CardTextures/Card_Comum_Verso"),
            Resources.Load<Texture2D>("+CardTextures/Card_Rara_Verso"),
            Resources.Load<Texture2D>("CardTextures/Card_Epica_Verso")
        };

        if (data.rarity < 0.3) {
            rarity = Rarity.Rare;
            textureBack = backTextures[1]; // Textura rara
        }

        else if (data.rarity < 0.075) {
            rarity = Rarity.Epic;
            textureBack = backTextures[2]; // Textura épica
        }

        else {
            rarity = Rarity.Common;
            textureBack = backTextures[0]; // Textura comum
        }
    }

    public GameObject GenerateObject(Vector3 position) {
        cardObj = new GameObject(data.fishName + " Card");

        Image imageComponent = cardObj.AddComponent<Image>();
        imageComponent.sprite = Sprite.Create(textureFront, new Rect(0, 0, textureFront.width, textureFront.height), new Vector2(0.5f, 0.5f));
        Button buttonComponent = cardObj.AddComponent<Button>();
        buttonComponent.onClick.AddListener(() => {
            Preview.Toggle(this);
            Preview.AssignInfo(data);
        });

        cardObj.transform.localPosition = position;
        imageComponent.transform.localScale = new Vector3(3.5f, 4.0f, 3.5f);

        lockObj = new GameObject("Lock");
        lockObj.transform.SetParent(cardObj.transform, false);

        Image lockImage = lockObj.AddComponent<Image>();
        lockImage.sprite = Sprite.Create(lockTex, new Rect(0, 0, lockTex.width, lockTex.height), new Vector2(0.5f, 0.5f));

        return cardObj;
    }
}