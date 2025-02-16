//QuangVV - 2025/02/12 - Create a script to manage all of the spawning.
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //Spawning Coin
    public GameObject[] coinMaps;
    public Transform[] positionSpawnCoin;
    private float startTimeCoin = 5;
    private float spawnTimeCoin;

    //Spawning Zombie
    public GameObject[] zombiePrefabs;
    private Vector3 spawnZombiePosition = new Vector3(19.64f, -3.72f, 0.08f);
    private float startTimeZombie = 8;
    private float spawnTimeZombie;

    //Spawning Zapper
    public GameObject[] zapperPrefabs;
    public Transform[] positionSpawnZapper;
    private float startTimeZapper = 13;
    private float spawnTimeZapper;

    private bool isPlaying = true;

    void Start()
    {
        spawnTimeCoin = startTimeCoin;
        spawnTimeZombie = startTimeZombie;
        spawnTimeZapper = startTimeZapper;
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }
        //Spawning Coin
        if (spawnTimeCoin <= 0)
        {
            SpawnCoin();
            spawnTimeCoin = Random.Range(10, 15);
        }
        else
        {
            spawnTimeCoin -= Time.deltaTime;
        }
        //Spawning Zombie
        if (spawnTimeZombie <= 0)
        {
            SpawnZombie();
            spawnTimeZombie = Random.Range(13, 18);
        }
        else
        {
            spawnTimeZombie -= Time.deltaTime;
        }
        //Spawning Zapper
        if (spawnTimeZapper <= 0)
        {
            SpawnZapper();
            spawnTimeZapper = Random.Range(5, 10);
        }
        else
        {
            spawnTimeZapper -= Time.deltaTime;
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
        int numberOfZombies = Random.Range(1, 4);
        bool[] spawned = new bool[zombiePrefabs.Length - 1];

        for (int i = 0; i < numberOfZombies; i++)
        {
            int randomZombieIndex;
            do
            {
                randomZombieIndex = Random.Range(0, zombiePrefabs.Length - 1);
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

    // QuangVV - 2025/02/12 - Create a method to spawn zapper - Start
    void SpawnZapper()
    {
        int numberOfZapper = Random.Range(1, 4);
        int randomZapperIndex = Random.Range(0, zapperPrefabs.Length - 1);
        int randomPostion;
        if (randomZapperIndex == 0)
        {
            for (int i = 0; i < numberOfZapper; i++)
            {
                randomPostion = Random.Range(0, 3);

                Instantiate(
                    zapperPrefabs[randomZapperIndex],
                    positionSpawnZapper[randomPostion].position,
                    Quaternion.identity
                );
            }
        }
        else
        {
            randomPostion = Random.Range(4, 6);

            if (randomZapperIndex == 1)
            {
                Instantiate(
                    zapperPrefabs[randomZapperIndex],
                    positionSpawnZapper[randomPostion].position,
                    Quaternion.Euler(0, 0, 120)
                );
            }
            else
            {
                Instantiate(
                    zapperPrefabs[randomZapperIndex],
                    positionSpawnZapper[randomPostion].position,
                    Quaternion.Euler(0, 0, 90)
                );
            }
        }
    }

    // QuangVV - 2025/02/12 - Create a method to spawn coins - End
    public void StopGame()
    {
        isPlaying = false;
    }
}
