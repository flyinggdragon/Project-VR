using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    public TMP_Text scoreText;

    

    void Update() {
        scoreText.text = GameManager.score.ToString();
    }    


}