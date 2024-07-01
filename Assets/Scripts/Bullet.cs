using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    [SerializeField] private float speed = 5f;

    private Vector2 direction = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (direction != Vector2.zero)
            transform.Translate(speed * Time.deltaTime * direction);
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void Shoot(Vector2 direction)
    {
        this.direction = direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            PoolManager.Instance.ReturnByType(this);
        }
    }

    public void OnObjectCreated()
    {
        Debug.Log("Object created");
    }

    public void OnObjectActivated()
    {
        Debug.Log("Object activated");
        direction = Vector2.zero;
    }
}
