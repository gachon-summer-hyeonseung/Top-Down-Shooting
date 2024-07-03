using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject
{
    public string idName;
    [SerializeField] private int health = 2;

    private EnemyMovement enemyMovement;
    private int m_health;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void DecreaseHealth(int damage)
    {
        m_health -= damage;
        if (m_health <= 0) EnemyManager.Instance.EnemyDead(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DecreaseHealth(1);
            EnemyManager.Instance.EnemyDead(this);
        }
        else if (other.CompareTag("Wall")) EnemyManager.Instance.EnemyDead(this);
    }

    public void OnObjectCreated()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        transform.rotation = new Quaternion(0.0f, 0.0f, 180f, 0.0f);
    }

    public void OnObjectActivated()
    {
        m_health = health;
        enemyMovement.SetDirection(Vector3.up);
    }
}
