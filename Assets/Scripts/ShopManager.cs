using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text playDamage;
    public Text playerDamagePriceText;
    public Text robotDamageText;
    public Text robotDamagePriceText;
    public Slider playerDamageSlider;
    public Slider robotDamageSlider;
    public Button buyRobotButton;
    public Button updatePlayerButton;
    public Button updateRobotButton;
    private int playerDamage = 5;
    private int robotDamage = 3;

    public int playerLevel;
    public int robotLevel;
    public bool isRobot;
    private int playerDamagePrice = 50;
    private int robotDamagePrice = 30;

    public Text totalCoinText;
    public int totalCoin;

    public void Start()
    {
        totalCoin = PlayerPrefs.GetInt("TotalsCoin", 500);
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        robotLevel = PlayerPrefs.GetInt("RobotLevel", 1);
        isRobot = PlayerPrefs.GetInt("IsRobot", 0) == 1;
        playerDamage = 5 + (playerLevel - 1) * 3;
        playerDamagePrice = 50 + (playerLevel - 1) * 20;

        robotDamage = 3 + (robotLevel - 1) * 2;
        robotDamagePrice = 30 + (robotLevel - 1) * 20;
        UpdateUI();
    }

    public void UpgradePlayer()
    {
        if (totalCoin > playerDamagePrice)
        {
            totalCoin -= playerDamagePrice;
            playerLevel++;

            // Cập nhật damage và giá nâng cấp
            playerDamage = 5 + (playerLevel - 1) * 3;
            playerDamagePrice = 50 + (playerLevel - 1) * 20;

            // Lưu vào PlayerPrefs
            PlayerPrefs.SetInt("PlayerLevel", playerLevel);
            PlayerPrefs.SetInt("TotalsCoin", totalCoin);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    public void UpgradeRobot()
    {
        if (totalCoin > robotDamagePrice)
        {
            totalCoin -= robotDamagePrice;
            robotLevel++;

            // Cập nhật damage và giá nâng cấp
            robotDamage = 3 + (robotLevel - 1) * 2;
            robotDamagePrice = 30 + (robotLevel - 1) * 20;

            // Lưu vào PlayerPrefs
            PlayerPrefs.SetInt("RobotLevel", robotLevel);
            PlayerPrefs.SetInt("TotalsCoin", totalCoin);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    public void BuyRobot()
    {
        if (totalCoin > 300)
        {
            isRobot = true;
            // Lưu trạng thái đã mua robot
            PlayerPrefs.SetInt("IsRobot", 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        totalCoinText.text = "Total coin: " + totalCoin;
        buyRobotButton.gameObject.SetActive(!isRobot);

        playerDamageSlider.value = playerLevel;
        playDamage.text = "Player Damage: " + playerDamage;
        playerDamagePriceText.text = playerDamagePrice.ToString();

        if (isRobot)
        {
            robotDamageSlider.value = robotLevel;
            robotDamageText.text = "Robot Damage: " + robotDamage;
            robotDamagePriceText.text = robotDamagePrice.ToString();
        }

        updatePlayerButton.interactable = playerLevel < 10;
        updateRobotButton.interactable = robotLevel < 10;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey("PlayerLevel");
        PlayerPrefs.DeleteKey("RobotLevel");
        PlayerPrefs.DeleteKey("IsRobot");
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.DeleteKey("TotalsCoin");
        PlayerPrefs.Save();
    }
}
