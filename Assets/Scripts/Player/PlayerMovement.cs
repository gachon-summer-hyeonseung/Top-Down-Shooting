using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IPoolObject
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveDirection = Vector2.zero;

    void Update()
    {
        if (moveDirection == Vector2.zero) return;

        transform.Translate((Vector3)moveDirection * Time.deltaTime * moveSpeed);
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        moveDirection = new Vector2(inputVector.x, inputVector.y);
    }

    public void OnObjectCreated()
    {
        Debug.Log("Object created");
    }

    public void OnObjectActivated()
    {
        Debug.Log("Object activated");
    }
}
