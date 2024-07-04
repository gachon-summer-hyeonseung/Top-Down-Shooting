using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IPoolObject
{
    [SerializeField] protected float speed = 5.0f;
    [SerializeField] protected int damage = 1;

    protected Vector3 direction = Vector3.zero;
    protected bool moving = false;

    // Update is called once per frame
    void Update()
    {
        if (direction != Vector3.zero)
            transform.Translate(speed * Time.deltaTime * direction);
    }

    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public virtual void Shoot()
    {
        moving = true;
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);

    public virtual void OnObjectCreated()
    {
    }

    public virtual void OnObjectActivated()
    {
        direction = Vector3.zero;
        moving = false;
    }
}
