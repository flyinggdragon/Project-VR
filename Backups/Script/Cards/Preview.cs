using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preview : MonoBehaviour {
    private static GameObject previewObj;
    private static GameObject previewCardObj;
    private static GameObject detailedCard;
    private static GameObject infoContainer;
    private static bool isOpen;
    private static Button closeBtn;

    public static void Initialize(GameObject obj) {

        previewObj = obj;
        
        isOpen = false;
        Preview.previewObj.SetActive(isOpen);

        previewCardObj = previewObj.transform.GetChild(0).gameObject;
            
        closeBtn = previewObj.transform.GetChild(1).gameObject.GetComponent<Button>();
        closeBtn.onClick.AddListener(() => {
            Toggle();
            
            GameManager.fishInterface.Toggle();
        });

        // !! = VERIFICAR SE É O OBJETO CERTO = !!
        detailedCard = previewObj.transform.GetChild(0).gameObject;

        PreviewCard.cardObj = detailedCard;
    }

    public static void Toggle(Card card = null) {
        isOpen = !isOpen;

        previewObj.SetActive(isOpen);

        if (card != null) {
            PreviewCard.DrawCard(card.textureFront, card.textureBack);
        }
    }

    public static void AssignInfo(FishData data) {
        // Literalmente tudo.
    }

    // Remover depois
    public void clicarBotaoColecao() {
        GameManager.fishInterface.Toggle();
    }

    public void clicarBotaoVoltar() {
        GameManager.fishInterface.Toggle();
    }
}






