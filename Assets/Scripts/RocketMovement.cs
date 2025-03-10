using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 20f;

    void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
