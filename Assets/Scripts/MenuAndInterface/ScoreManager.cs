using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText; // Texto que exibirá a pontuação na UI
    public TextMeshProUGUI cleanlinessText; // Texto que exibirá o nível de limpeza na UI

    private int score = 0;
    private int totalTrashCount; // Total de objetos de lixo no início do jogo
    private int currentTrashCount; // Contador de objetos de lixo restantes
    [SerializeField] public float cleanlinessLevel = 0.0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Contar dinamicamente o número total de objetos de lixo na cena no início do jogo
        totalTrashCount = GameObject.FindGameObjectsWithTag("Trash").Length;
        currentTrashCount = totalTrashCount;

        UpdateCleanlinessUI();
    }

    void Update() {
        if (cleanlinessLevel == 100.0f) {
            Debug.Log("GANHOU");
            SceneManager.LoadScene("VictoryScene");
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Atualiza o texto apenas com o número da pontuação
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned in ScoreManager.");
        }
    }

    public void DecreaseTrashCount()
    {
        currentTrashCount--;
        UpdateCleanlinessUI();
    }

    private void UpdateCleanlinessUI()
    {
        if (cleanlinessText != null)
        {
            cleanlinessLevel = 100f * (totalTrashCount - currentTrashCount) / totalTrashCount;
            cleanlinessText.text = "Nível de Limpeza: " + cleanlinessLevel.ToString("F1") + "%";
        }
    }
}