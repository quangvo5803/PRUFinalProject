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
    public bool IsBoss = false;
    private int multiScore = 5;
    private int currentScore = 0;
    private double accumulatedTime = 0;
    private int currentCoin = 0;
    private int totalCoin = 0;

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
        if (currentScore >= 100)
        {
            IsBoss = true;
            return;
        }
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
        totalCoin += currentCoin;
        SaveBestScore();
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

    public void WiningGame()
    {
        UnlockNextLevel();
        totalCoin += currentCoin;
    }

    public void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedLevel", 1))
        {
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
            PlayerPrefs.SetInt("ReachedLevel", nextLevel);
            PlayerPrefs.Save();
            multiScore += 5;
        }
    }

    private void SaveBestScore()
    {
        double bestScore = PlayerPrefs.GetFloat("BestScore", 0);

        if (currentScore > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", (float)currentScore);
            PlayerPrefs.Save();
        }
    }
}
