using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text scoreText;
    public Background background;
    public SpawnerManager spawnerManager;
    public bool IsPlaying = true;
    private int multiScore = 1;
    private double currentScore = 0;
    private double accumulatedTime = 0;
    private int coin = 0;

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
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlaying)
        {
            return;
        }
        UpdateScoreUI();
    }

    public void UpdateCoin()
    {
        coin++;
    }

    private void UpdateScoreUI()
    {
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime >= 0.2)
        {
            currentScore += multiScore;
            accumulatedTime = 0;
        }
        scoreText.text = ((uint)currentScore).ToString("D12");
    }

    public void StopGame()
    {
        IsPlaying = false;
    }
}
