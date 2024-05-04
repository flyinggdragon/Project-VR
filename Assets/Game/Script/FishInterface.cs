using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInterface : MonoBehaviour {
    private GameObject score;
    private List<FishData> fishList;
    public bool isOpen = false;

    void Start() {
        GameManager.fishInterface = GetComponent<FishInterface>();
        score = GameObject.Find("Score");

        fishList = FishJSONReader.ReadFishDataFromJSON();
        gameObject.SetActive(false);
    }

    void Update() {
        
    }

    public void Toggle() {
        isOpen = !isOpen;

        if (isOpen) { Show(); }
        else { UnShow(); }
    }

    public void Show() {
        gameObject.SetActive(true);
        score.SetActive(false);
    }

    public void UnShow() {
        gameObject.SetActive(false);
        score.SetActive(true);
    }
}