using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background Instance;
    public GameObject background;
    private GameObject[] layers;
    private float[] baseSpeeds = { 5.0f, 0.2f, 1, 2, 3, 4 };
    private float[] speeds;
    private float[] resetPosition = { -20.8f, -33, -33, -33, -33, -33 };
    private float[] startPosition = { 21, 7, 33, 33, 33, 33, 33 };

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int childCount = background.transform.childCount;
        layers = new GameObject[childCount];
        speeds = new float[baseSpeeds.Length];
        Array.Copy(baseSpeeds, speeds, baseSpeeds.Length);

        for (int i = 0; i < childCount; i++)
        {
            layers[i] = background.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsBoss || !GameManager.Instance.IsPlaying)
            return;

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].transform.Translate(-speeds[i] * Time.deltaTime, 0, 0);

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

    public void SetSpeedMultiplier(float multiplier)
    {
        for (int i = 0; i < speeds.Length; i++)
        {
            speeds[i] = baseSpeeds[i] * multiplier;
        }
    }
}
