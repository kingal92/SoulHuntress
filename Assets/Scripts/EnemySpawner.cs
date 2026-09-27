using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 1.1f;
    public int baseEnemiesPerWave = 6;

    private int enemiesRemaining;
    private int activeEnemies;
    private bool bossWave;

    public bool IsBossWave => bossWave;

    public void StartWave(int waveNumber)
    {
        bossWave = false;
        enemiesRemaining = Mathf.Max(4, baseEnemiesPerWave + waveNumber * 2);
        activeEnemies = 0;
        StartCoroutine(SpawnRoutine());
    }

    public void StartBossWave(int waveNumber)
    {
        bossWave = true;
        activeEnemies = 0;
        SpawnBoss();
    }

    public bool IsWaveClear()
    {
        return !bossWave && activeEnemies <= 0 && enemiesRemaining <= 0;
    }

    private IEnumerator SpawnRoutine()
    {
        while (enemiesRemaining > 0)
        {
            SpawnEnemy();
            enemiesRemaining--;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
            return;

        Transform spawn = GetRandomSpawnPoint();
        GameObject newEnemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
        activeEnemies++;
        EnemyController enemy = newEnemy.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.maxHealth += GameManager.Instance != null ? GameManager.Instance.wave * 5 : 0;
            enemy.TakeDamage(-5);
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null)
            return;

        Vector3 spawnPos = Vector3.zero;
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        activeEnemies++;
    }

    private Transform GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
            return new GameObject("SpawnPoint").transform;

        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    public void RegisterEnemyDefeat()
    {
        activeEnemies = Mathf.Max(0, activeEnemies - 1);

        if (!bossWave && activeEnemies <= 0 && enemiesRemaining <= 0 && GameManager.Instance != null)
            GameManager.Instance.OnEnemyDefeated(null);
    }
}
