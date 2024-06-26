using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public GameObject scoreObject;
    public Collection collection;
    public List<FishData> fish;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        fish = new List<FishData>();
    }

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene"); // Substitua "VictoryScene" pelo nome da sua cena de vitória
    }
}