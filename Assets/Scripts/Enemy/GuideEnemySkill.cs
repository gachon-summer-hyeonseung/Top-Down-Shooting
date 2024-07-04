using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideEnemySkill : BaseEnemySkill
{
    protected override void UseSkill()
    {
        Debug.Log("GuideEnemySkill");
        GuideBullet bullet = PoolManager.Instance.GetByType<GuideBullet>();
        bullet.SetPosition(transform.position);
        bullet.Shoot();
    }
}
