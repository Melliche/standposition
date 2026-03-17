using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{
    [System.Serializable]
    public struct EnemySpawnGroup
    {
        public GameObject enemyPrefab;
        public int count;
    }

    [System.Serializable]
    public struct WaveConfig
    {
        public string waveName;
        public EnemySpawnGroup[] enemies;
        public WaveStatScritableObject waveStats;
    }

    [Header("Configuration des Vagues")]
    public WaveConfig[] waves;
    private int currentWaveIndex = 0;
    private struct QueuedEnemy
    {
        public GameObject prefab;
    }
    private List<QueuedEnemy> spawnQueue = new List<QueuedEnemy>();

    [Header("Paramètres d'apparition")]
    private float spawnRate = 1f;
    private GameObject[] spawnPosition;
    public static List<SkeletonBehaviour> AllEnemies = new List<SkeletonBehaviour>();
    void Start()
    {
        spawnRate = 1f;
        spawnPosition = GameObject.FindGameObjectsWithTag("Spawn");
        PrepareWave();
        spawnPosition = GameObject.FindGameObjectsWithTag("Spawn");
        InvokeRepeating("SpawEnnemy", 0f, spawnRate);
        InvokeRepeating("NextWave", 10f, 10f);
        Invoke("StopSpawn", 1000.0f);
    }
    public void SpawEnnemy()
    {
        if (spawnQueue.Count == 0) return;
        int random = Random.Range(1,8);
        Vector3 randomizePosition = spawnPosition[random].transform.position;
        QueuedEnemy nextEnemy = spawnQueue[0];
        spawnQueue.RemoveAt(0);
        GameObject newEnemy = Instantiate(nextEnemy.prefab, randomizePosition, Quaternion.identity);
        SkeletonBehaviour skeleton = newEnemy.GetComponent<SkeletonBehaviour>();
        if (skeleton != null)
        {
            WaveConfig currentWave = waves[currentWaveIndex];
            skeleton.SetStats(currentWave.waveStats);            
        }
    }

    void PrepareWave()
    {
        spawnQueue.Clear();

        if (currentWaveIndex >= waves.Length) return;
        WaveConfig currentWave = waves[currentWaveIndex];
        
        foreach (EnemySpawnGroup group in currentWave.enemies)
        {
            for (int i = 0; i < group.count; i++)
            {
                spawnQueue.Add(new QueuedEnemy { prefab = group.enemyPrefab });
            }
        }
    }
    public void NextWave()
    {
        currentWaveIndex++;
        
        if (currentWaveIndex >= waves.Length)
        {
            CancelInvoke(nameof(SpawEnnemy));
            CancelInvoke(nameof(NextWave));
            return;
        }

        PrepareWave();

        spawnRate = Mathf.Max(0.2f, spawnRate - 0.2f);
        CancelInvoke(nameof(SpawEnnemy));
        InvokeRepeating(nameof(SpawEnnemy), 0f, spawnRate);
    }
    void Update()
    {
        
    }
}
