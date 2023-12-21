using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public Transform[] spawnLocations;
    public float spawnInterval;

    void Start()
    {
        spawnInterval = 30f;
        InvokeRepeating("SpawnZombie", 30, spawnInterval);
    }

    void SpawnZombie()
    {
        // 랜덤한 좀비 프리팹 선택
        GameObject selectedZombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

        // 랜덤한 위치 선택
        Transform selectedSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];

        // 좀비 생성
        Instantiate(selectedZombiePrefab, selectedSpawnLocation.position, Quaternion.identity);
    }
}