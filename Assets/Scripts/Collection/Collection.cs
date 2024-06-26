using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ao abrir a tela, o jogo atrás deve pausar. Isso deve ser possivel usando alguma função assíncrona ou co-rotina.
public class Collection : MonoBehaviour {
    private List<FishData> fishList;
    private List<Texture2D> sprites;
    private List<Card> cards;
    private bool isOpen = false;
    private Button backBtn;

    void Start() {
        cards = new List<Card>();
        sprites = new List<Texture2D>();

        backBtn = GetComponentInChildren<Button>();
        backBtn.onClick.AddListener(() => {
            Toggle();
        });

        GameManager.instance.collection = GetComponent<Collection>();
        GameManager.instance.fish = FishJSONReader.ReadFishDataFromJSON();

        fishList = GameManager.instance.fish;

        gameObject.SetActive(false);
        
        sprites = CardImageManager.LoadImages();
        GenerateCards();
    }

    void Update() {
        foreach (Card card in cards) {
            if (card.collected) {
                card.lockObj.SetActive(false);
            }
            else {
                card.lockObj.SetActive(true);
            }
        }
    }

    private void GenerateCards() {
        foreach (FishData fish in fishList) {
            Texture2D tex = CardImageManager.GetCardSpriteFromList(fish.spriteName, sprites);

            if (tex) {
                cards.Add(new Card(tex, fish));
            }
        }
        
        GameObject bottom = gameObject.transform.GetChild(0).gameObject;
        GameObject scrollArea = bottom.transform.GetChild(0).gameObject;
        GameObject container = scrollArea.transform.GetChild(0).transform.GetChild(0).gameObject;

        int i = 0;

        foreach (Card card in cards) {
            // Colocar um grid horizontal ao invés de usar posições absolutas. Diferença mínima, mas bem melhor estruturalmente.
            card.GenerateObject(new Vector3(0, 0, 0)).transform.SetParent(container.transform, false);
            

            i++;
        }
    }

    public void Toggle() {
        isOpen = !isOpen;

        gameObject.SetActive(isOpen);
        //score.SetActive(!isOpen);
    }
}