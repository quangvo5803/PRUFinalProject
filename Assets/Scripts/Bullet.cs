using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.position = new Vector3(
            transform.position.x + 5.0f * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
        if (transform.position.x > 9.5)
        {
            Destroy(this.gameObject);
        }
    }
}
