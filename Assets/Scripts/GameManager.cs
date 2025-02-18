using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    public Text coinText;
    public Background background;
    public SpawnerManager spawnerManager;
    public bool IsPlaying = true;
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

    public void StopGame()
    {
        IsPlaying = false;
    }
}
