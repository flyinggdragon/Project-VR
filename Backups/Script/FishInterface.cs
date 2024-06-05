using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ao abrir a tela, o jogo atrás deve pausar. Isso deve ser possivel usando alguma função assíncrona ou co-rotina.
public class FishInterface : MonoBehaviour {
    private GameObject score;
    private List<FishData> fishList;
    private List<Texture2D> sprites;
    private List<Card> cards;
    private bool isOpen = false;

    void Start() {
        GameManager.fishInterface = GetComponent<FishInterface>();
        score = GameObject.Find("Score");
        
        Preview.Initialize(gameObject.transform.GetChild(0).gameObject);

        fishList = FishJSONReader.ReadFishDataFromJSON();

        gameObject.SetActive(false);

        cards = new List<Card>();
        sprites = new List<Texture2D>();
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
        
        // Objeto responsável pelo scroll
        GameObject scrollBack = gameObject.transform.GetChild(2).gameObject;

        // Container das cartas, onde todas elas estão dispostas
        GameObject container = scrollBack.transform.GetChild(0).gameObject;

        int i = 0;

        foreach (Card card in cards) {
            // Colocar um grid horizontal ao invés de usar posições absolutas. Diferença mínima, mas bem melhor estruturalmente.
            card.GenerateObject(new Vector3(i * 350, 0, 0)).transform.SetParent(container.transform, false);

            i++;
        }
    }

    public void Toggle() {
        isOpen = !isOpen;

        gameObject.SetActive(isOpen);
        score.SetActive(!isOpen);
    }
}
