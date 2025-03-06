using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float[] zombieSpeed = { 6f, 7f, 8f };
    private float speed;
    private Animator animator;
    private bool isDead = false;
    public GameObject[] supportItems;

    private int health = 30;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetSpeed();
    }

    public void SetSpeed()
    {
        speed = zombieSpeed[Random.Range(0, zombieSpeed.Length)];
    }

    void Update()
    {
        if (GameManager.Instance.IsBoss || !GameManager.Instance.IsPlaying || isDead)
        {
            if (GameManager.Instance.IsBoss)
            {
                DestroyObject();
            }
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
        if (supportItems.Length > 0)
        {
            int randomIndex = Random.Range(0, supportItems.Length);
            Instantiate(supportItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
