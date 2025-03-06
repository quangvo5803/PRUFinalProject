using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float[] zombieSpeed = { 6f, 7f, 8f };
    private float speed;
    private Animator animator;
    private bool isDead = false;

    public int health = 2;

  
    public GameObject[] supportItems;

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
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetBool("IsDead", true);
            Invoke("DestroyObject", 1f);

            if (Random.value <= 0.5f)
            {
                Invoke("SpawnSupportItem", 1f);
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
