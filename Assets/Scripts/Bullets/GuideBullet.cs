using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBullet : BaseBullet
{
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (target != null)
            {
                Vector3 direction = target.transform.position - transform.position;
                direction.Normalize();
                transform.Translate(speed * Time.deltaTime * direction);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall")) PoolManager.Instance.ReturnByType(this);
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DecreaseHealth(1);
            PoolManager.Instance.ReturnByType(this);
        }
        else if (other.CompareTag("Shield")) PoolManager.Instance.ReturnByType(this);
    }

    public override void OnObjectActivated()
    {
        base.OnObjectActivated();
        target = EnemyManager.Instance.GetTarget();
    }
}
