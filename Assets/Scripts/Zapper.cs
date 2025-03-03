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
        animator.speed = Random.Range(1f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsBoss || !GameManager.Instance.IsPlaying)
        {
            if (GameManager.Instance.IsBoss)
            {
                Destroy(this.gameObject);
            }
            animator.speed = 0;
            return;
        }
        //On Off Laser
        if (spriteRenderer.sprite == laserOn)
        {
            zapperCollider.enabled = true;
        }
        else
        {
            zapperCollider.enabled = false;
        }

        transform.position = new Vector3(
            transform.position.x - 4.0f * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
        if (transform.position.x < -15)
        {
            Destroy(this.gameObject);
        }
    }
}
