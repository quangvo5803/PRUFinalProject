using UnityEngine;

public class RobotShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab vi�n ??n xanh
    public Transform firePoint; // V? tr� b?n ??n
    public float bulletSpeed = 10f;
    public AudioClip shootSound; // �m thanh b?n

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartShooting();
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            CancelInvoke(nameof(Shoot));
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        audioSource.Stop();
        audioSource.PlayOneShot(shootSound);
    }

    public void StartShooting()
    {
        if (!IsInvoking(nameof(Shoot))) // Kiểm tra nếu chưa bắn thì mới bắt đầu
        {
            InvokeRepeating(nameof(Shoot), 0f, 1f);
        }
    }
}
