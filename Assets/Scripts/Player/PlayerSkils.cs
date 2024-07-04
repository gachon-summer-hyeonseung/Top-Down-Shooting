using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerSkils : MonoBehaviour
{
    [SerializeField] private GameObject shield;

    void OnDefaultAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Bullet bullet = PoolManager.Instance.GetByType<Bullet>();
            bullet.SetPosition(transform.position);
            bullet.SetDirection(Vector3.up);
            bullet.Shoot();
        }
    }

    void OnBlock(InputValue value)
    {
        float input = value.Get<float>();
        if (input == 1) shield.SetActive(true);
        else shield.SetActive(false);
    }
}
