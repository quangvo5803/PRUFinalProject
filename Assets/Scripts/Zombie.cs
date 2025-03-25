using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public float[] zombieSpeed = { 3, 4, 5 };
    private float speed;
    private Animator animator;
    private bool isDead = false;
    public GameObject[] supportItems;

    private int health = 30;
    public Slider enemyHealthSlider;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetSpeed();
        enemyHealthSlider.maxValue = health;
        enemyHealthSlider.value = health;
    }

    public void SetSpeed()
    {
        speed = zombieSpeed[Random.Range(0, zombieSpeed.Length)];
    }

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

        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.value = health;
            Debug.Log(
                gameObject.name
                    + " health: "
                    + health
                    + ", Slider value: "
                    + enemyHealthSlider.value
            );
        }

        if (transform.position.x < -30)
        {
            DestroyObject();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("RobotBullet"))
        {
            int damage = other.CompareTag("Bullet")
                ? GameManager.Instance.playerDamage
                : GameManager.Instance.robotDamage;
            health -= damage;

            if (enemyHealthSlider != null)
            {
                enemyHealthSlider.value = health;
            }

            Destroy(other.gameObject);

            if (health <= 0)
            {
                if (enemyHealthSlider != null)
                {
                    enemyHealthSlider.value = 0;
                }

                GetComponent<Collider2D>().enabled = false;
                isDead = true;
                animator.SetBool("IsDead", isDead);

                Invoke(nameof(DestroyObject), 1f);
                GameManager.Instance.AddScore(10);
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
