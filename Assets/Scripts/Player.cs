using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public Text currentScoreText;
    bool horizontalInput;
    float flyPower = 5f;
    float fallPower = 4.5f;
    bool isFly = false;
    bool isGround = true;

    // Magnet Power-up
    public static bool isMagnetActive = false;
    private float magnetDuration = 5f;

    // Speed Boost Power-up
    private bool isSpeedBoostActive = false;
    private float speedBoostDuration = 10f;
    private float boostedSpeedMultiplier = 2f;

    // Extra Life
    private int maxLives = 3;
    private int currentLives = 1;


    private Animator animator;
    public AudioClip flySound;
    private AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip zapperSound;
    public AudioClip zombieSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsBoss)
        {
            animator.SetBool("IsBoss", true);
        }
        if (GameManager.Instance.IsPause)
        {
            animator.speed = 0;
            return;
        }
        else
        {
            animator.speed = 1;
        }
        if (GameManager.Instance.IsPlaying)
        {
            horizontalInput = Input.GetKey(KeyCode.Space);
            Shooting();
        }
        else
        {
            horizontalInput = false;
        }
        // FlyOn
        if (horizontalInput)
        {
            if (!isFly)
            {
                if (!audioSource.isPlaying || audioSource.time > 0.9f) // Ð?m b?o âm thanh không quá dài
                {
                    audioSource.clip = flySound;
                    audioSource.time = 0; // Reset th?i gian v? 0 ð? phát t? ð?u
                    audioSource.Play();
                }
            }

            isFly = true;
            isGround = false;
            transform.Translate(0, flyPower * Time.deltaTime, 0);
        }
        else
        {
            isFly = false;
            transform.Translate(0, -fallPower * Time.deltaTime, 0);
        }

        // Run
        if (transform.position.y < -3.5f)
        {
            isGround = true;
            transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
        }

        /// Change Animator
        animator.SetBool("IsFlyIng", isFly);
        animator.SetBool("IsGround", isGround);

        UpdateCoin();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            other.transform.position += Vector3.up * 0.5f;
            Destroy(other.gameObject, 0.2f);
            GameManager.Instance.UpdateCoin();
            audioSource.PlayOneShot(coinSound);

        }
        if (other.gameObject.tag == "Obstacle")
        {
            audioSource.PlayOneShot(zombieSound);

            animator.SetBool("IsDead", true);
            GameManager.Instance.StopGame();
        }
        if (other.CompareTag("Magnet") || other.CompareTag("SpeedBoost") || other.CompareTag("ExtraLife"))
        {
            if (other.CompareTag("Magnet"))
            {
                ActivateMagnet();
            }
            else if (other.CompareTag("SpeedBoost") && !isSpeedBoostActive)
            {
                StartCoroutine(ActivateSpeedBoost());
            }
            else if (other.CompareTag("ExtraLife"))
            {
                IncreaseLife();
            }

            Destroy(other.gameObject);
        }
    }
    public void ActivateMagnet()
    {
        isMagnetActive = true;
        Invoke("DeactivateMagnet", magnetDuration);
    }

    void DeactivateMagnet()
    {
        isMagnetActive = false;
    }

    IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        flyPower *= boostedSpeedMultiplier;

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        Background.Instance.SetSpeedMultiplier(boostedSpeedMultiplier);

        yield return new WaitForSeconds(speedBoostDuration);

        flyPower /= boostedSpeedMultiplier;

        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }


        Background.Instance.SetSpeedMultiplier(1f);

        isSpeedBoostActive = false;
    }


    void IncreaseLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            Debug.Log("Extra Life Gained! Lives: " + currentLives);
        }
        else
        {
            Debug.Log("Already at max lives!");
        }
    }

    void UpdateCoin()
    {
        if (isMagnetActive)
        {
            AttractGold();
        }
    }

    void AttractGold()
    {
        GameObject[] golds = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject gold in golds)
        {
            gold.transform.position = Vector3.MoveTowards(gold.transform.position, transform.position, 7f * Time.deltaTime);
        }
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 bulletPostion = new Vector3(
                -3.4f,
                transform.position.y - 0.05f,
                transform.position.z
            );
            Instantiate(bullet, bulletPostion, Quaternion.identity);
        }
    }
}