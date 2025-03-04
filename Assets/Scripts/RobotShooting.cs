using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab viên ??n xanh
    public Transform firePoint; // V? trí b?n ??n
    public float bulletSpeed = 10f;
    public AudioClip shootSound; // Âm thanh b?n

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
        rb.linearVelocity = new Vector2(bulletSpeed, 0); // ??n bay sang ph?i

        // Phát âm thanh b?n, luôn phát l?i t? ??u
        audioSource.Stop();
        audioSource.PlayOneShot(shootSound);
    }
}
