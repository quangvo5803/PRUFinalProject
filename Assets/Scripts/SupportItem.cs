using UnityEngine;

public enum SupportItemType { Magnet, SpeedBoost, ExtraLife }
public class SupportItem : MonoBehaviour
{
    public SupportItemType itemType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            return;
        }
        transform.position = new Vector3(
            transform.position.x - 2.0f * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
        if (transform.position.x < -15)
        {
            Destroy(this.gameObject);
        }
    }
}
