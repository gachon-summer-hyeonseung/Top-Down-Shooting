using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CSVManager : MonoBehaviour
{
    public static CSVManager Instance { get; private set; }

    private Dictionary<string, List<string[]>> cache = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public List<string[]> Read(string path, bool clean)
    {
        if (!clean && cache.TryGetValue(path, out List<string[]> value))
            return value;

        List<string[]> list = new();

        using (StreamReader sr = new(Path.Combine(Application.streamingAssetsPath, path)))
        {
            sr.ReadLine().Split(',');

            while (!sr.EndOfStream)
                list.Add(sr.ReadLine().Split(','));
        };

        cache.Add(path, list);

        return list;
    }

    public void Save(string path, List<string[]> data)
    {
        using (StreamWriter sw = new(Path.Combine(Application.streamingAssetsPath, path)))
        {
            foreach (string[] line in data)
                sw.WriteLine(string.Join(",", line));
        }
    }

    public List<StageData> GetStageList()
    {
        List<string[]> rawStageData = Read("stage.csv", false);
        List<string[]> rawEnemyData = Read("stage_enemy.csv", false);
        List<string[]> rawBossData = Read("stage_boss.csv", false);

        List<StageData> stageList = new();
        List<StageEnemyData> enemyList = new();
        List<StageBossData> bossList = new();


        foreach (string[] line in rawStageData)
        {
            stageList.Add(new()
            {
                stageId = int.Parse(line[0]),
            });
        }

        foreach (string[] line in rawEnemyData)
        {
            enemyList.Add(new()
            {
                stageId = int.Parse(line[0]),
                enemyId = int.Parse(line[1]),
                spawnStartTime = float.Parse(line[2]),
                spawnInterval = float.Parse(line[3]),
                spawnCount = int.Parse(line[4]),
            });
        }

        foreach (string[] line in rawBossData)
        {
            bossList.Add(new()
            {
                stageId = int.Parse(line[0]),
                patternId = int.Parse(line[1]),
            });
        }

        return (from stage in stageList
                join enemy in enemyList on stage.stageId equals enemy.stageId into enemies
                join boss in bossList on stage.stageId equals boss.stageId into bosses
                select new StageData
                {
                    stageId = stage.stageId,
                    enemies = enemies.ToList(),
                    boss = bosses.FirstOrDefault(),
                }).ToList();
    }

    public void SaveStageData(List<StageData> stageDatas)
    {
        List<string[]> rawStageData = new()
        {
            new string[] { "stageId" }
        };
        List<string[]> rawEnemyData = new(){
            new string[] { "stageId", "enemyId", "spawnStartTime", "spawnInterval", "spawnCount" }
        };
        List<string[]> rawBossData = new() {
            new string[] { "stageId", "patternId" }
        };

        foreach (StageData stageData in stageDatas)
        {
            rawStageData.Add(new string[] { stageData.stageId.ToString() });

            foreach (StageEnemyData enemyData in stageData.enemies)
            {
                rawEnemyData.Add(new string[] { stageData.stageId.ToString(), enemyData.enemyId.ToString(), enemyData.spawnStartTime.ToString("F1"), enemyData.spawnInterval.ToString("F1"), enemyData.spawnCount.ToString() });
            }

            if (stageData.boss != null)
            {
                rawBossData.Add(new string[] { stageData.stageId.ToString(), stageData.boss.patternId.ToString() });
            }
        }

        Save("stage.csv", rawStageData);
        Save("stage_enemy.csv", rawEnemyData);
        Save("stage_boss.csv", rawBossData);
    }
}
