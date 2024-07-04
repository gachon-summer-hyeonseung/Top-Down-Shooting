using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IPoolObject
{
    [SerializeField] private int health = 10;
    [SerializeField] private float patternCoolTime = 3.0f;
    private float currentPatternCoolTime = 0.0f;
    private int currentHealth;

    private int pattern = 1;
    private bool isPatternRunning = false;
    private bool shootable = false;

    // Update is called once per frame
    void Update()
    {
        if (shootable)
        {
            if (!isPatternRunning && currentPatternCoolTime <= 0)
            {
                StartCoroutine(RunPattern());
            }
            currentPatternCoolTime -= Time.deltaTime;
        }
    }

    public void DecreaseHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PoolManager.Instance.ReturnByType(this);
            StageManager.Instance.BossDead();
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetPattern(int pattern)
    {
        this.pattern = pattern;
    }

    IEnumerator RunPattern()
    {
        isPatternRunning = true;
        switch (pattern)
        {
            case 0:
                int random = Random.Range(4, 10);
                float fullRange = 5;
                float randomBulletCount = Random.Range(3, 5);
                float randomPosition = fullRange / randomBulletCount;
                for (int i = 0; i < random; i++)
                {
                    float position = -(fullRange / 2);
                    for (int j = 0; j < randomBulletCount + 1; j++)
                    {
                        BossBullet bullet = PoolManager.Instance.GetByType<BossBullet>();
                        bullet.SetPosition(new Vector3(position, 3.0f, 0.0f));
                        bullet.SetDirection(Vector3.down);
                        bullet.Shoot();
                        position += randomPosition;
                    }
                    yield return new WaitForSeconds(0.2f);
                }
                break;
            case 1:
                // Number of bullets to spawn in a circle
                int numberOfBullets = Random.Range(18, 25);
                // Radius of the circle
                float radius = 2f;

                for (int j = 0; j < Random.Range(5, 11); j++)
                {
                    for (int i = 0; i < numberOfBullets; i++)
                    {
                        // Calculate angle for each bullet
                        float angle = i * Mathf.PI * 2 / numberOfBullets;
                        // Calculate x and y position based on angle and radius
                        float x = Mathf.Sin(angle) * radius;
                        float y = Mathf.Cos(angle) * radius;
                        // Position for the bullet
                        Vector3 bulletPosition = transform.position + new Vector3(x, y, 0);

                        // Get a bullet from the pool and set its position and direction
                        BossBullet bullet = PoolManager.Instance.GetByType<BossBullet>();
                        bullet.SetPosition(bulletPosition);
                        // Assuming the direction is outward from the center of the circle
                        bullet.SetDirection((bulletPosition - transform.position).normalized);
                        bullet.Shoot();
                    }
                    yield return new WaitForSeconds(0.2f);
                }
                break;
        }
        isPatternRunning = false;
        currentPatternCoolTime = patternCoolTime;
    }

    public void Run()
    {
        shootable = true;
    }

    public void OnObjectCreated()
    {
    }

    public void OnObjectActivated()
    {
        currentHealth = health;
        shootable = false;
        isPatternRunning = false;
        currentPatternCoolTime = 0;
    }
}
