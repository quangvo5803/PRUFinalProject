using UnityEngine;

[System.Serializable]
public class ColorToPrefab
{
    public Color color;
    public GameObject prefab;
}

public class CoinGenerate : MonoBehaviour
{
    public Texture2D coinMap;
    public ColorToPrefab[] colorMappings;
    public GameObject parentObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update() { }

    void GenerateMap()
    {
        for (int x = 0; x < coinMap.width; x++)
        {
            for (int y = 0; y < coinMap.height; y++)
            {
                GenerateCoins(x, y);
            }
        }
    }

    void GenerateCoins(int x, int y)
    {
        Color pixelColor = coinMap.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }
        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(
                    colorMapping.prefab,
                    position,
                    Quaternion.identity,
                    parentObj.transform
                );
            }
        }
    }
}
