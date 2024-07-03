using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private Vector3 directon = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (directon != Vector3.zero)
            transform.Translate(speed * Time.deltaTime * directon);
    }

    public void SetDirection(Vector3 direction)
    {
        directon = direction;
    }
}
