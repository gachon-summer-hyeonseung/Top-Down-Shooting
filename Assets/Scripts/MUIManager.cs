using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUIManager : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("MUIManager : Play");
        GameManager.Instance.Play();
    }

    public void Rank()
    {
        GameManager.Instance.GoRank();
    }
}
