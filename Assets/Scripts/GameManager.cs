using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int score = 0;
    public static GameObject scoreObject;

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

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene"); // Substitua "VictoryScene" pelo nome da sua cena de vitória
    }
}
