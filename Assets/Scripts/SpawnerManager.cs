//QuangVV - 2025/02/12 - Create a script to manage all of the spawning.
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] coinMaps;
    public Transform[] positionSpawnCoin;
    private float startTimeCoin = 5;
    private float spawnTimeCoin;
    public GameObject[] zombiePrefabs;
    private float startTimeZombie = 5;
    private float spawnTimeZombie;
    private Vector3 spawnZombiePosition = new Vector3(19.64f, -3.72f, 0.08f);

    void Start()
    {
        spawnTimeCoin = startTimeCoin;
        spawnTimeZombie = startTimeZombie;
    }

    void Update()
    {
        if (spawnTimeCoin <= 0)
        {
            SpawnCoin();
            spawnTimeCoin = Random.Range(10, 15);
        }
        else
        {
            spawnTimeCoin -= Time.deltaTime;
        }
        if (spawnTimeZombie <= 0)
        {
            SpawnZombie();
            spawnTimeZombie = Random.Range(5, 10);
        }
        else
        {
            spawnTimeZombie -= Time.deltaTime;
        }
    }

    //QuangVV - 2025/02/12 - Create a method to spawn coins - Start
    void SpawnCoin()
    {
        int randomCoin = Random.Range(0, coinMaps.Length);
        if (randomCoin != coinMaps.Length - 1)
        {
            int randomPostion = Random.Range(0, positionSpawnCoin.Length - 1);
            Instantiate(
                coinMaps[randomCoin],
                positionSpawnCoin[randomPostion].position,
                Quaternion.identity
            );
        }
        else
        {
            int randomPostion = Random.Range(0, positionSpawnCoin.Length - 2);
            Instantiate(
                coinMaps[randomCoin],
                positionSpawnCoin[randomPostion].position,
                Quaternion.identity
            );
        }
    }

    // QuangVV - 2025/02/12 - Create a method to spawn coins - End

    // ThangDCS - 2025/02/12 - Create a method to spawn zombies - Start
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

            GameObject zombie = Instantiate(
                zombiePrefabs[randomZombieIndex],
                spawnZombiePosition,
                Quaternion.identity
            );

            Zombie zombieScript = zombie.GetComponent<Zombie>();
            if (zombieScript != null)
            {
                zombieScript.SetSpeed();
            }
        }
    }
    // ThangDCS - 2025/02/12 - Create a method to spawn zombies - End
}
