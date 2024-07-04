using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BaseBullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall")) PoolManager.Instance.ReturnByType(this);
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DecreaseHealth(1);
            PoolManager.Instance.ReturnByType(this);
        }
    }
}
