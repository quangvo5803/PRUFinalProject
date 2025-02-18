using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text currentScoreText;
    bool horizontalInput;
    float flyPower = 5f;
    float fallPower = 4.5f;
    bool isFly = false;
    bool isGround = true;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            horizontalInput = Input.GetKey(KeyCode.Space);
        }
        else
        {
            horizontalInput = false;
        }
        // FlyOn
        if (horizontalInput)
        {
            Debug.Log("Fly");
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
        }
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Player Dead");
            animator.SetBool("IsDead", true);
            GameManager.Instance.StopGame();
        }
    }
}
