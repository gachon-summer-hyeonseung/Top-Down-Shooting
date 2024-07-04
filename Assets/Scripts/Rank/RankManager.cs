using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public struct RankData
{
    public string name;
    public int score;
    public int stage;
    public int playTime;
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
        string json = RankDataToJson(rankData);
        var req = new UnityWebRequest("http://localhost:3000/rank", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error While Sending: " + req.error);
        }
        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
            callback();
        }
    }

    string RankDataToJson(RankData rankData)
    {
        return JsonUtility.ToJson(rankData);
    }
}
