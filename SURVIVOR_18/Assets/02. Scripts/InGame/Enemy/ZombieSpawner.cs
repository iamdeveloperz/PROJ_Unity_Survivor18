using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public Transform[] spawnLocations;
    public float spawnInterval;

    public Terrain terrain;
    public GameObject emptyObjectsParent;

    void Start()
    {
        spawnInterval = 10f;
        AdjustObjectsToTerrainHeight();
        InvokeRepeating("SpawnZombie", 0, spawnInterval);
    }


    void AdjustObjectsToTerrainHeight()
    {
        if (terrain == null || emptyObjectsParent == null)
        {
            Debug.LogError("Terrain or emptyObjectsParent is not assigned!");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        foreach (Transform child in emptyObjectsParent.transform)
        {
            // Step 1: 현재 빈 오브젝트의 좌표를 얻음

            float x = child.transform.position.x;
            float z = child.transform.position.z;

            float yTerrain = terrain.SampleHeight(new Vector3(x, 0f, z));

            Vector3 newPosition = child.transform.position;
            newPosition.y = yTerrain - 12.8f;
            child.transform.position = newPosition;
        }
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