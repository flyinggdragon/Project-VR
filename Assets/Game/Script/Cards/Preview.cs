using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preview : MonoBehaviour {
    public GameObject previewCardObj;
    public PreviewCard previewCard;
    public GameObject infoContainer;
    private bool isOpen;
    private Button closeBtn;

    void Start() {
        GameManager.preview = GetComponent<Preview>();

        isOpen = false;
        gameObject.SetActive(isOpen); // false

        previewCardObj = gameObject.transform.GetChild(1).gameObject;
        previewCard = previewCardObj.GetComponent<PreviewCard>();

        previewCard.cardObj = previewCardObj;
            
        closeBtn = gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
        closeBtn.onClick.AddListener(() => {
            GameManager.ClosePreview(); // Fechar

            // Destruir objeto.
        });
    }

    public void Toggle() {
        isOpen = !isOpen;

        gameObject.SetActive(isOpen);
    }

    public void AssignInfo(FishData data) {

        Debug.Log(data.fishName);
    }
}