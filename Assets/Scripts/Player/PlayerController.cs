using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void DecreaseHealth(int damage)
    {
        Debug.Log("PlayerController : DecreaseHealth");
        GameManager.Instance.GameOver();
    }
}
