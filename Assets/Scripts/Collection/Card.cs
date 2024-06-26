using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card {
    public Texture2D textureFront;
    //public Texture2D textureBack;
    public FishData data;
    public bool collected;
    private Rarity rarity;
    private GameObject cardObj;
    public GameObject lockObj;
    private static Texture2D lockTex;
    //private static Texture2D[] backTextures;

    public enum Rarity {
        Common,
        Rare,
        Epic
    }

    public Card(Texture2D tex, FishData fish) {
        textureFront = tex;
        data = fish;
        collected = false;

        lockTex = Resources.Load<Texture2D>("spritesGerais/bloqueadaCard");

        if (data.rarity < 0.3) {
            rarity = Rarity.Rare;
        }

        else if (data.rarity < 0.075) {
            rarity = Rarity.Epic;
        }

        else {
            rarity = Rarity.Common;
        }
    }

    public GameObject GenerateObject(Vector3 position) {
        cardObj = new GameObject(data.fishName + " Card");

        Image imageComponent = cardObj.AddComponent<Image>();
        imageComponent.sprite = Sprite.Create(textureFront, new Rect(0, 0, textureFront.width, textureFront.height), new Vector2(0.5f, 0.5f));
        Button buttonComponent = cardObj.AddComponent<Button>();

        cardObj.transform.localPosition = position;
        imageComponent.transform.localScale = new Vector3(3.00f, 3.00f, 3.00f);

        lockObj = new GameObject("Lock");
        lockObj.transform.SetParent(cardObj.transform, false);

        Image lockImage = lockObj.AddComponent<Image>();
        lockImage.sprite = Sprite.Create(lockTex, new Rect(0, 0, lockTex.width, lockTex.height), new Vector2(0.5f, 0.5f));

        return cardObj;
    }
}