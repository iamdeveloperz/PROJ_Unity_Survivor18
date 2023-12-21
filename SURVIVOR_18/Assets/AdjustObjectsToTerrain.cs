using UnityEngine;

public class AdjustObjectsToTerrain : MonoBehaviour
{
    public Terrain terrain;
    public GameObject emptyObjectsParent;

    int numberOfObjects = 100; // 생성할 오브젝트 수
    float max = 40f; // x 좌표의 최대값
    float min = -40f; // z 좌표의 최대값


    void Awake()
    {
        SpawnEmptyObjects();
        AdjustObjectsToTerrainHeight();
    }

    void SpawnEmptyObjects()
    {
        for (int i = 1; i <= numberOfObjects; i++)
        {
            Vector3 randomPosition;
            // x, z 좌표를 무작위로 설정
            float randomX = Random.Range(min, max);
            float randomZ = Random.Range(min, max);
            if(randomX > -20 && randomX < 20 && randomZ > -20 && randomZ < 20 ) { continue; }
            randomPosition = new Vector3(randomX, 0f, randomZ);
            // 빈 오브젝트 생성
            GameObject emptyObject = new GameObject($"{i}");
            emptyObject.transform.position = randomPosition;

            // 생성한 빈 오브젝트를 현재 오브젝트의 하위로 설정
            emptyObject.transform.parent = transform;
        }
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
}
