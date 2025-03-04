using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float[] zombieSpeed = { 6f, 7f, 8f };
    private float speed;
    private Animator animator;
    private bool isDead = false;
    public GameObject supportItem;

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
            //Vô hiệu hóa collider để không nhận đạn
            gameObject.GetComponent<Collider2D>().enabled = false;
            isDead = true;
            //Chạy animation và hủy zombie sau 1s
            Destroy(other.gameObject);
            animator.SetBool("IsDead", isDead);
            Invoke("DestroyObject", 1f);
            // Xác suất 50% xuất hiện supportItem
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
        if (supportItem != null)
        {
            Instantiate(supportItem, transform.position, Quaternion.identity);
        }
    }
}
