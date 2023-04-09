using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject stairPrefab;

    [SerializeField]
    private GameObject terrainPrefab;

    private float stairSpawnInterval = 4.26f;

    private float stairLifetime = 60.0f;

    private Vector3 stairSpawnPosition = new Vector3(0, 41, 113);

    private float terrainSpawnInterval = 9f;

    private float terrainLifetime = 60.0f;

    private Vector3 terrainSpawnPosition = new Vector3(-50, 10, 90);

    void Start()
    {
        StartCoroutine(SpawnStairs());
        StartCoroutine(SpawnTerrain());
    }

    IEnumerator SpawnStairs()
    {
        while (true)
        {
            GameObject spawnedStairs =
                Instantiate(stairPrefab,
                stairSpawnPosition,
                Quaternion.Euler(0, 180, 0));
            spawnedStairs.name = "SpawnedStairs";

            Destroy (spawnedStairs, stairLifetime);

            yield return new WaitForSeconds(stairSpawnInterval);
        }
    }

    IEnumerator SpawnTerrain()
    {
        while (true)
        {
            GameObject spawnedTerrain =
                Instantiate(terrainPrefab,
                terrainSpawnPosition,
                Quaternion.Euler(0, 0, 0));
            spawnedTerrain.name = "SpawnedTerrain";

            Destroy (spawnedTerrain, terrainLifetime);
            yield return new WaitForSeconds(terrainSpawnInterval);
        }
    }
}
