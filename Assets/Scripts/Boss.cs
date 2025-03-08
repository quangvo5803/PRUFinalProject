using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private int currentHealth = 100;
    private int maxhHeath = 100;
    private int hurtHeath = 80;
    private bool isHurt = false;
    private bool isAttack = false;
    private bool isWalk = true;
    private Animator animator;
    public GameObject warningSignPrefab;
    private GameObject currentWarning;

    public GameObject[] lazer;
    public Slider healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.maxValue = maxhHeath;
        healthBar.value = currentHealth;
        animator = GetComponent<Animator>();
        //Lặp lại tấn công sau mỗi 3-10s
        InvokeRepeating("Attack", 0, Random.Range(3f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            animator.speed = 0;
            CancelInvoke("Attack"); // Ngừng Attack khi game dừng
            return;
        }
        else
        {
            animator.speed = 1;
            if (!IsInvoking("Attack")) // Nếu Attack chưa được gọi lại, kích hoạt lại
            {
                InvokeRepeating("Attack", 0, Random.Range(3f, 10f));
            }
        }
        Walk();
    }

    void Attack()
    {
        isAttack = true;
        animator.SetBool("IsAttack", isAttack);

        int numLazers = Random.Range(1, 3); // Chọn 1 hoặc 2 laser
        StartCoroutine(Lazer(numLazers));

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
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "RobotBullet")
        {
            Destroy(other.gameObject);
            Hurt(
                other.gameObject.tag == "Bullet"
                    ? GameManager.Instance.playerDamage
                    : GameManager.Instance.robotDamage
            );
        }
    }

    void Hurt(int damage)
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.WiningGame();
            animator.SetBool("IsDead", true);
            Invoke("DestroyObject", 1f);
            return;
        }
        currentHealth -= damage;
        Debug.Log(damage);
        healthBar.value = currentHealth;
        if (currentHealth <= hurtHeath)
        {
            hurtHeath -= 20;
            isHurt = true;
            animator.SetBool("IsHurt", isHurt);
            Invoke("StopHurt", 1f);
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
        animator.SetBool("IsDead", false);
        Destroy(this.gameObject);
    }

    System.Collections.IEnumerator Lazer(int numberLazer)
    {
        for (int i = 0; i < numberLazer; i++)
        {
            if (!GameManager.Instance.IsPlaying)
                yield break; //
            float randomY = Random.Range(-4.7f, 4.7f);
            Vector3 warningPosition = new Vector3(8f, randomY, 0);
            currentWarning = Instantiate(warningSignPrefab, warningPosition, Quaternion.identity);

            yield return new WaitForSeconds(1.5f);
            if (!GameManager.Instance.IsPlaying)
                yield break; // Dừng coroutine nếu game đang dừng
            Destroy(currentWarning);

            Vector3 spawnPosition = new Vector3(0, randomY, 0);
            GameObject laze = Instantiate(
                lazer[Random.Range(0, 2)],
                spawnPosition,
                Quaternion.identity
            );
            Destroy(laze, 1f);
        }
    }
}
