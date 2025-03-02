using UnityEngine;

public class Boss : MonoBehaviour
{
    private int currentHealth = 500;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Chỗ ni xử lí khi đụng đạn trừ bao nhiêu máu đó rồi kêu hàm hurt
    }

    void Hurt()
    {
        if (currentHealth <= hurtHeath)
        {
            hurtHeath -= 50;
            isHurt = true;
            animator.SetBool("IsHurt", isHurt);
            Invoke("StopHurt", 0.5f);
        }
        isHurt = false;
    }

    void StopHurt()
    {
        isHurt = false;
        animator.SetBool("IsHurt", isHurt);
    }
}
