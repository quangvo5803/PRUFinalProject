using UnityEngine;

public class Boss : MonoBehaviour
{
    private int currentHealth = 100;
    private int hurtHeath = 450;
    private bool isHurt = false;
    private bool isAttack = false;
    private bool isWalk = true;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        //Lặp lại tấn công sau mỗi 3-10s
        InvokeRepeating("Attack", 0, Random.Range(3f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    void Attack()
    {
        isAttack = true;
        animator.SetBool("IsAttack", isAttack);
        Invoke("StopAttack", 1.0f);
    }

    void StopAttack()
    {
        isAttack = false;
        animator.SetBool("IsAttack", isAttack);
    }

    void Walk()
    {
        if (isWalk)
        {
            transform.position = new Vector3(
                transform.position.x - 3.0f * Time.deltaTime,
                transform.position.y,
                transform.position.z
            );
        }
        if (transform.position.x <= 5)
        {
            isWalk = false;
        }
        animator.SetBool("IsWalk", isWalk);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Hurt();
        }
    }

    void Hurt()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            Invoke("DestroyObject", 1f);
            return;
        }
        currentHealth -= 10;
        if (currentHealth <= hurtHeath)
        {
            hurtHeath -= 100;
            isHurt = true;
            animator.SetBool("IsHurt", isHurt);
            Invoke("StopHurt", 2f);
        }
        isHurt = false;
    }

    void StopHurt()
    {
        isHurt = false;
        animator.SetBool("IsHurt", isHurt);
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
