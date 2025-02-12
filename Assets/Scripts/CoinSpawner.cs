using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject[] coinMaps;
    public Transform[] positionSpawn;
    public float startTime = 5;
    private float timeBetweenSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeBetweenSpawn = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenSpawn <= 0)
        {
            SpawnCoin();
            timeBetweenSpawn = Random.Range(5, 10);
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }

    void SpawnCoin()
    {
        int randomCoin = Random.Range(0, coinMaps.Length);
        if (randomCoin != coinMaps.Length - 1)
        {
            Instantiate(coinMaps[randomCoin], positionSpawn[0].position, Quaternion.identity);
        }
        else
        {
            int randomPostion = Random.Range(0, positionSpawn.Length);
            Instantiate(
                coinMaps[randomCoin],
                positionSpawn[randomPostion].position,
                Quaternion.identity
            );
        }
    }
}
