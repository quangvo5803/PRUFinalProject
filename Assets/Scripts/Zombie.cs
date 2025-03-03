using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float[] zombieSpeed = { 6f, 7f, 8f };
    private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSpeed();
    }

    public void SetSpeed()
    {
        speed = zombieSpeed[Random.Range(0, zombieSpeed.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsBoss || !GameManager.Instance.IsPlaying)
        {
            if (GameManager.Instance.IsBoss)
            {
                Destroy(this.gameObject);
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
            Destroy(this.gameObject);
        }
    }
}
