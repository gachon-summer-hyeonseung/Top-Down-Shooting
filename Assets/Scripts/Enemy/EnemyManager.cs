using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private float spawnRange = 2.0f;
    [SerializeField] private float spawnTime = 3.0f;

    private Coroutine createEnemyCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        createEnemyCoroutine = StartCoroutine(IECreateEnemy());
    }

    IEnumerator IECreateEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Enemy enemy = PoolManager.Instance.GetByType<Enemy>();
            enemy.SetPosition(new Vector3(Random.Range(-spawnRange, spawnRange), 6.0f, 0.0f));
        }
    }

    public void EnemyDead(Enemy enemy)
    {
        PoolManager.Instance.ReturnByType(enemy);
    }
}
