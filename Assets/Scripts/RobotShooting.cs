using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab viên đạn xanh
    public Transform firePoint; // Vị trí bắn đạn
    public float bulletSpeed = 10f;
    public AudioClip shootSound; // Âm thanh bắn

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        InvokeRepeating(nameof(Shoot), 0f, 0.5f);
    }

  

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(bulletSpeed, 0); // Đạn bay sang phải

        // Phát âm thanh bắn, luôn phát lại từ đầu
        audioSource.Stop();
        audioSource.PlayOneShot(shootSound);
    }
}
