using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseBullet
{
    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall")) PoolManager.Instance.ReturnByType(this);
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().DecreaseHealth(1);
            PoolManager.Instance.ReturnByType(this);
        }
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().DecreaseHealth(1);
            PoolManager.Instance.ReturnByType(this);
        }
    }
}
