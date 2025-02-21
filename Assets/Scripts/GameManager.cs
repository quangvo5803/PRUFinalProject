using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    public Text coinText;
    public Background background;
    public SpawnerManager spawnerManager;
    public GameObject loseMenu;
    public bool IsPlaying = true;
    public bool IsPause = false;
    private int multiScore = 1;
    private double currentScore = 0;
    private double accumulatedTime = 0;
    private int currentCoin = 0;

    private void Awake()
    {
        // Đảm bảo chỉ có một thể hiện duy nhất của GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlaying)
        {
            return;
        }
        UpdateScore();
        UpdateUI();
    }

    public void UpdateCoin()
    {
        currentCoin++;
    }

    private void UpdateScore()
    {
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime >= 0.1)
        {
            currentScore += multiScore;
            accumulatedTime = 0;
        }
    }

    private void UpdateUI()
    {
        scoreText.text = ((uint)currentScore).ToString("D12");
        coinText.text = currentCoin.ToString("D5");
    }

    public void BackToHome()
    {
        currentCoin = 0;
        currentScore = 0;
        SceneManager.LoadSceneAsync(0);
    }

    public void PauseGame()
    {
        IsPlaying = false;
        IsPause = true;
        Debug.Log("Pause Game");
    }

    public void ResumeGame()
    {
        IsPlaying = true;
        IsPause = false;
    }

    public void StopGame()
    {
        IsPlaying = false;
        currentCoin = 0;
        currentScore = 0;
        loseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        currentCoin = 0;
        currentScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
