using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float[] zombieSpeed = { 4f, 5f, 6f };
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
