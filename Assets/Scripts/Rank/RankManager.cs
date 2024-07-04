using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RankData
{
    public string name;
    public int score;
    public int rank;
    public int time;
}

public class RankManager : MonoBehaviour
{
    public static RankManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void InsertRank(RankData rankData, Action callback)
    {
        Debug.Log("Insert Rank");
        StartCoroutine(IEInsertRank(rankData, callback));
        callback();
    }

    public IEnumerator IEInsertRank(RankData rankData, Action callback)
    {
        yield return null;
        Debug.Log("Insert Rank Coroutine");
        callback();
    }
}
