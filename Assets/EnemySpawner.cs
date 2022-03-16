using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private HealthController enemyPrefab;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private float spawnRate = 3.0f;

    private float spawnRateTimer = 0.0f;

    private void CreateEnemyAtRandomSpawnPosition()
    {
        var randomIndex = Random.Range(0, spawnPositions.Count); // If count is 10, the random range will be 0 to 9 (including 0 and 9)

        var spawnPoint = spawnPositions[randomIndex]; // gets first position of the List

        var enemyInstance = Instantiate(enemyPrefab);

        enemyInstance.transform.position = spawnPoint.position;
    }

    void Update()
    {
        // Timer code
        if (spawnRateTimer > 0.0f) {
            spawnRateTimer -= Time.deltaTime;
            return;
        }
        spawnRateTimer = spawnRate;

        // Exhausted timer behaviour
        CreateEnemyAtRandomSpawnPosition();
    }
}
