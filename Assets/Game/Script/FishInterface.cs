using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FishInterface : MonoBehaviour {
    private GameObject score;
    private List<FishData> fishList;
    private List<Texture2D> sprites;
    private List<Card> cards;
    private int currentCardIndex;
    public bool isOpen = false;

    void Start() {
        GameManager.fishInterface = GetComponent<FishInterface>();
        score = GameObject.Find("Score");

        fishList = FishJSONReader.ReadFishDataFromJSON();
        gameObject.SetActive(false);

        currentCardIndex = 0;  

        cards = new List<Card>();
        sprites = new List<Texture2D>();
        sprites = CardImageManager.LoadImages();

        GenCards();
    }

    void Update() {
        
    }

    private void GenCards() {
        foreach (FishData fish in fishList) {
            Texture2D tex = CardImageManager.GetCardSpriteFromList(fish.spriteName, sprites);

            if (tex) {
                cards.Add(new Card(tex, fish));
            }
        }

        List<Vector3> positions = new List<Vector3>();
        int i = 0;
        
        GameObject scrollBack = gameObject.transform.GetChild(2).gameObject;
        GameObject container = scrollBack.transform.GetChild(0).gameObject;

        foreach (Card card in cards) {
            positions.Add(new Vector3(i * 350, 0, 0));

            GameObject imageObject = new GameObject(card.data.fishName + " Card");
            imageObject.transform.SetParent(container.transform, false);

            Image imageComponent = imageObject.AddComponent<Image>();
            imageComponent.sprite = Sprite.Create(card.texture, new Rect(0, 0, card.texture.width, card.texture.height), new Vector2(0.5f, 0.5f));

            imageObject.transform.localPosition = positions[i];
            imageComponent.transform.localScale = new Vector3(3.5f, 4.0f, 3.5f);

            i++;
        }
    }

    public void Scroll(int inc) {
        currentCardIndex += inc;
        Debug.Log("Index: " + currentCardIndex);
    }

    public void Toggle() {
        isOpen = !isOpen;

        gameObject.SetActive(isOpen);
        score.SetActive(!isOpen);
    }

    public class Card {
        public Texture2D texture;
        public FishData data;

        public Card(Texture2D tex, FishData fish) {
            texture = tex;
            data = fish;
        }
    }
}