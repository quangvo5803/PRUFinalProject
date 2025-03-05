using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float[] zombieSpeed = { 1, 1.5f, 2 };
    private float speed;
    private Animator animator;
    private bool isDead = false;
    public GameObject supportItem;

    private int health = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        SetSpeed();
    }

    public void SetSpeed()
    {
        speed = zombieSpeed[Random.Range(0, zombieSpeed.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying || isDead)
        {
            return;
        }
        transform.position = new Vector3(
            transform.position.x - speed * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
        if (transform.position.x < -30)
        {
            DestroyObject();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("RobotBullet"))
        {
            //Trừ máu tùy vào đạn
            health -= other.CompareTag("Bullet")
                ? GameManager.Instance.playerDamage
                : GameManager.Instance.robotDamage;
            //Hủy viên đạn
            Destroy(other.gameObject);

            if (health <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                isDead = true;
                animator.SetBool("IsDead", isDead);

                Invoke(nameof(DestroyObject), 1f);
                GameManager.Instance.AddScore(10);
                // 30% cơ hội xuất hiện SupportItem
                if (Random.value <= 0.3f)
                {
                    Invoke(nameof(SpawnSupportItem), 1f);
                }
            }
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    void SpawnSupportItem()
    {
        if (supportItem != null)
        {
            Instantiate(supportItem, transform.position, Quaternion.identity);
        }
    }
}
