using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoManager : MonoBehaviour {
    public TMP_Text regionName;
    public TMP_Text cleanlinessLevel;

    void Update() {
        //regionName.text = GameManager.currentLocation.locationName;
        cleanlinessLevel.text = "0%";
    }
}