using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        sprites = Loader.LoadImages();

        GenCards();
    }

    void Update() {
        
    }

    private void GenCards() {

        foreach (FishData fish in fishList) {
            Debug.Log(fish.spriteName + "Card");
            Texture2D spr = Loader.GetCardSpriteFromList(fish.spriteName, sprites);

            if (spr) {
                cards.Add(new Card(spr, fish));
                Debug.Log("Criada carta: " + fish.spriteName + "Card");
            } else {
                Debug.Log("Não existe carta com o nome: " + fish.spriteName + "Card");
            }
        }

        foreach (Card card in cards) {
            //teste
            Debug.Log("Card");

            // aqui criar todos os gameobjects necessários.
            // precisa também de um grid horizontal sepa
            // movimentação lateral não sei como fazer ainda
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
        private Texture2D sprite;
        private FishData data;

        public Card(Texture2D spr, FishData fish) {
            sprite = spr;
            data = fish;
        }
    }
}