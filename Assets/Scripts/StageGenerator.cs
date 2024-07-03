using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [System.Serializable]
    class EnemyOptions
    {
        public float minSpawnStartTime = 0;
        public float maxSpawnStartTime = 1;
        public float minSpawnInterval = 0.5f;
        public float maxSpawnInterval = 1;
        public int minSpawnCount = 1;
        public int maxSpawnCount = 3;
    }

    [SerializeField] private int stageCount = 100;
    [SerializeField] private EnemyOptions enemy1;
    [SerializeField] private EnemyOptions enemy2;

    void Start()
    {
        CreateStageData();
    }

    void CreateStageData()
    {
        List<StageData> stageDataList = new();

        for (int i = 0; i < stageCount; i++)
        {
            int stageId = i + 1;
            StageData stageData = new()
            {
                stageId = stageId,
                enemies = new()
            };

            float spawStartTime = Random.Range(enemy1.minSpawnStartTime, enemy1.maxSpawnStartTime);
            float spawnInterval = Random.Range(enemy1.minSpawnInterval, enemy1.maxSpawnInterval);
            int spawnCount = Random.Range(enemy1.minSpawnCount, enemy1.maxSpawnCount);
            stageData.enemies.Add(new StageEnemyData()
            {
                stageId = stageId,
                enemyId = 1,
                spawnStartTime = spawStartTime,
                spawnInterval = spawnInterval,
                spawnCount = spawnCount
            });

            float enemy2SpawnStartTime = Random.Range(enemy2.minSpawnStartTime, enemy2.maxSpawnStartTime);
            float enemy2spawnInterval = Random.Range(enemy2.minSpawnInterval, enemy2.maxSpawnInterval);
            int enemy2SpawnCount = Random.Range(enemy2.minSpawnCount, enemy2.maxSpawnCount);
            stageData.enemies.Add(new StageEnemyData()
            {
                stageId = stageId,
                enemyId = 2,
                spawnStartTime = enemy2SpawnStartTime,
                spawnInterval = enemy2spawnInterval,
                spawnCount = enemy2SpawnCount
            });

            stageData.boss = new StageBossData()
            {
                stageId = stageId,
                patternId = Random.Range(0, 2)
            };

            stageDataList.Add(stageData);
        }

        CSVManager.Instance.SaveStageData(stageDataList);
    }
}
