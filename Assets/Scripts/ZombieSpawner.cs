using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public float startTime = 5;
    public float spawnTime;

    // Fixed spawn positions
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(19.64f, -3.72f, 0.08f),
    };

    void Start()
    {
        spawnTime = startTime;
    }

    void Update()
    {
        if (spawnTime <= 0)
        {
            SpawnZombie();
            spawnTime = Random.Range(5, 10);
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }
    void SpawnZombie()
    {
        int numberOfZombies = Random.Range(1, Mathf.Min(zombiePrefabs.Length, 4));
        bool[] spawned = new bool[zombiePrefabs.Length];

        for (int i = 0; i < numberOfZombies; i++)
        {
            int randomZombieIndex;
            do
            {
                randomZombieIndex = Random.Range(0, zombiePrefabs.Length);
            } while (spawned[randomZombieIndex]); 

            spawned[randomZombieIndex] = true; 

            GameObject zombie = Instantiate(zombiePrefabs[randomZombieIndex], spawnPositions[0], Quaternion.identity);

            Zombie zombieScript = zombie.GetComponent<Zombie>();
            if (zombieScript != null)
            {
                zombieScript.SetSpeed();
            }
        }
    }
}
