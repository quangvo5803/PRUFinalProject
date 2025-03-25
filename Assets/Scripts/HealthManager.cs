using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;
    public static int maxHealth = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            if (i < hearts.Length)
            {
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public static void AddLife()
    {
        if (health < maxHealth)
        {
            health++;
        }
    }
}
