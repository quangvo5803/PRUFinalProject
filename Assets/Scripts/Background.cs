using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject background;
    private GameObject[] layers;
    private float[] speeds = { 5.0f, 0.2f, 1, 2, 3, 4 };
    private float[] resetPosition = { -20.8f, -33, -33, -33, -33, -33 };
    private float[] startPosition = { 21, 7, 33, 33, 33, 33, 33 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int childCount = background.transform.childCount;
        layers = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            layers[i] = background.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        for (int i = 0; i < layers.Length; i++)
        {
            // Di chuyển từng layer theo tốc độ riêng
            layers[i].transform.Translate(-speeds[i] * Time.deltaTime, 0, 0);

            // Kiểm tra nếu layer ra khỏi màn hình thì đặt lại vị trí
            if (layers[i].transform.position.x < resetPosition[i])
            {
                layers[i].transform.position = new Vector3(
                    startPosition[i],
                    layers[i].transform.position.y,
                    layers[i].transform.position.z
                );
            }
        }
    }
}
