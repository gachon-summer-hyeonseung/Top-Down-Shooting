using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private float spawnRange = 2.0f;

    private GameObject player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public GameObject GetTarget()
    {
        return player;
    }

    public void StartSpawnEnemy(StageEnemyData enemyData)
    {
        StartCoroutine(IECreateEnemy(enemyData));
        // switch (enemyData.enemyId)
        // {
        //     case (int)EnemyType.Default:
        //         StartCoroutine(IECreateEnemy<Enemy>(enemyData.spawnStartTime, enemyData.spawnInterval, enemyData.spawnCount));
        //         break;
        //     case (int)EnemyType.Second:
        //         StartCoroutine(IECreateEnemy<Enemy>(enemyData.spawnStartTime, enemyData.spawnInterval, enemyData.spawnCount));
        //         break;
        // }
    }

    IEnumerator IECreateEnemy(StageEnemyData enemyData)
    {
        yield return new WaitForSeconds(enemyData.spawnStartTime);

        for (int i = 0; i < enemyData.spawnCount; i++)
        {
            Enemy enemy = PoolManager.Instance.GetByType<Enemy>(enemyData.enemyId - 1);
            enemy.SetPosition(new Vector3(Random.Range(-spawnRange, spawnRange), 6.0f, 0.0f));
            yield return new WaitForSeconds(enemyData.spawnInterval);
        }
    }

    public void EnemyDead(Enemy enemy)
    {
        PoolManager.Instance.ReturnByType(enemy.idName, enemy);
        StageManager.Instance.ReduceEnemyCount();
    }
}
