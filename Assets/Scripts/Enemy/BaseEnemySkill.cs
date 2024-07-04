using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemySkill : MonoBehaviour, IPoolObject
{
    [SerializeField] protected float skillCoolTime = 1.0f;

    protected float currentCoolTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (currentCoolTime <= 0)
        {
            UseSkill();
            currentCoolTime = skillCoolTime;
        }
        currentCoolTime -= Time.deltaTime;
    }

    protected abstract void UseSkill();

    public virtual void OnObjectCreated()
    {
    }

    public virtual void OnObjectActivated()
    {
        currentCoolTime = 0.0f;
    }
}
