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
    int multiScore = 1;
    double currentScore = 0;
    private double accumulatedTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetKey(KeyCode.Space);
        // FlyOn
        if (horizontalInput)
        {
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
        UpdateScore();
    }

    private void UpdateScore()
    {
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime >= 0.2)
        {
            currentScore += multiScore;
            accumulatedTime = 0;
        }
        currentScoreText.text = ((uint)currentScore).ToString("D12");
    }
}
