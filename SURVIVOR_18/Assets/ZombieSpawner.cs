using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public Transform[] spawnLocations;
    public float spawnInterval = 30f;

    void Start()
    {
        SpawnZombie();
        InvokeRepeating("SpawnZombie", spawnInterval, spawnInterval);
    }

    void SpawnZombie()
    {
        if (zombiePrefabs.Length == 0)
        {
            Debug.LogWarning("좀비 프리팹이 할당되어 있지 않습니다.");
            return;
        }

        if (spawnLocations.Length == 0)
        {
            Debug.LogWarning("위치 프리팹이 할당되어 있지 않습니다.");
            return;
        }

        // 랜덤한 좀비 프리팹 선택
        GameObject selectedZombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

        // 랜덤한 위치 선택
        Transform selectedSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];

        // 좀비 생성
        Instantiate(selectedZombiePrefab, selectedSpawnLocation.position, Quaternion.identity);
    }
}