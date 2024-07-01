using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerSkils : MonoBehaviour
{
    void OnDefaultAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Bullet bullet = PoolManager.Instance.GetByType<Bullet>();
            bullet.SetPosition(transform.position);
            bullet.Shoot(Vector2.up);
        }
    }
}
