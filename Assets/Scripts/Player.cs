using System;
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
        if (other.gameObject.tag == "SupportItem")
        {
            Destroy(other.gameObject);
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
