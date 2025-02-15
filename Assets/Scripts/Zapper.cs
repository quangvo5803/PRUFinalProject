using UnityEngine;

public class Zapper : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D zapperCollider;

    public Sprite laserOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        zapperCollider = GetComponent<Collider2D>();
        animator.speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.sprite == laserOn)
        {
            zapperCollider.enabled = true;
        }
        else
        {
            zapperCollider.enabled = false;
        }
    }
}
