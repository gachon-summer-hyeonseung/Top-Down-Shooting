using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private List<StageData> stageDatas;

    private StageData currentStageData;
    private int currentEnemyCount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartStage();
    }

    public void StartStage()
    {
        stageDatas = CSVManager.Instance.GetStageList();

        StartStage(1);
    }

    public void StartStage(int stageId)
    {
        StageData stageData = stageDatas.Find(data => data.stageId == stageId);
        currentStageData = stageData;
        currentEnemyCount = stageData.enemies.Select(e => e.spawnCount).Sum();

        foreach (StageEnemyData enemyData in stageData.enemies)
        {
            EnemyManager.Instance.StartSpawnEnemy(enemyData);
        }
    }

    public void ReduceEnemyCount()
    {
        currentEnemyCount--;

        if (currentEnemyCount <= 0) EnemyManager.Instance.SpawnBoss(currentStageData.boss);
    }

    public void BossDead()
    {
        GameManager.Instance.AddPoint(1000);
        if (currentStageData.stageId == 100) GameManager.Instance.GameClear();
        else
            StartStage(currentStageData.stageId + 1);
    }

    public StageData GetCurrentStage()
    {
        return currentStageData;
    }
}
