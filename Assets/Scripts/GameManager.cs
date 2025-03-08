using System.Linq;
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
    public GameObject loseMenu;
    public GameObject winMenu;
    public bool IsPlaying = true;
    public bool IsPause = false;
    public bool IsBoss = false;
    private int multiScore = 5;
    private int currentScore = 0;
    private double accumulatedTime = 0;
    private int currentCoin = 0;
    private int totalCoin;
    public int playerDamage;
    public int robotDamage;
    public int playerLevel;
    public int robotLevel;

    public int playerDamagePrice;
    public int robotDamagePrice;

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
        totalCoin = PlayerPrefs.GetInt("TotalsCoin", 0);
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        robotLevel = PlayerPrefs.GetInt("RobotLevel", 1);

        //Tính toán damage dựa theo level
        playerDamage = 5 + (playerLevel - 1) * 3;
        robotDamage = 3 + (robotLevel - 1) * 2;
        IsBoss = false;
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
        if (currentScore >= 100000000)
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

    public void AddScore(int score)
    {
        currentScore += score;
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

    [System.Obsolete]
    public void ResumeGame()
    {
        IsPlaying = true;
        IsPause = false;
        RobotShooting robot = FindObjectsOfType<RobotShooting>().First();
        robot.StartShooting();
    }

    public void StopGame()
    {
        IsPlaying = false;
        totalCoin += currentCoin;
        SaveBestScore();
        PlayerPrefs.SetInt("TotalsCoin", totalCoin);
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
        winMenu.SetActive(true);
        IsPlaying = false;
        UnlockNextLevel();
        totalCoin += currentCoin;
        PlayerPrefs.SetInt("TotalsCoin", totalCoin);
        PlayerPrefs.Save();
    }

    public void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("UnlockedLevel", 1))
        {
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
            PlayerPrefs.Save();
            multiScore += 5;
        }
    }

    public void NextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel < SceneManager.sceneCountInBuildSettings) // Kiểm tra nếu level tiếp theo hợp lệ
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            BackToHome();
        }
    }

    private void SaveBestScore()
    {
        double bestScore = PlayerPrefs.GetFloat("BestScore", 0);

        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save();
        }
    }
}
