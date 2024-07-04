using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float playTime { get; private set; }
    public bool playing { get; private set; }
    public int point { get; private set; }
    public int stage { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Play()
    {
        InitData();
        SceneManager.LoadScene("SampleScene");
        playing = true;
        Debug.Log("Game Start!");
        StartCoroutine(IELogPlayTime());
    }

    void InitData()
    {
        playing = false;
        playTime = 0.0f;
        point = 0;
        stage = 1;
    }

    IEnumerator IELogPlayTime()
    {
        while (playing)
        {
            yield return new WaitForSeconds(1.0f);
            playTime += 1.0f;
        }
    }

    public void GameClear()
    {
        playing = false;
        UpdateStage();
        Debug.Log("Game Clear!");
        Debug.Log("Play Time: " + playTime);
        Debug.Log("Point: " + point);
        Debug.Log("Stage: " + stage);
        GameEnd();
    }

    public void GameOver()
    {
        playing = false;
        UpdateStage();
        Debug.Log("Play Time: " + playTime);
        Debug.Log("Point: " + point);
        Debug.Log("Stage: " + stage);
        GameEnd();
    }

    void GameEnd()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void AddPoint(int point)
    {
        if (!playing) return;
        this.point += point;
        Debug.Log("Point: " + point);
    }

    void UpdateStage()
    {
        stage = StageManager.Instance.GetCurrentStage().stageId;
    }

    public void GoRank()
    {
        SceneManager.LoadScene("RankScene");
    }

    public void GoLobby()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Register(string name)
    {
        RankManager.Instance.InsertRank(
            new RankData
            {
                name = name,
                score = point,
                rank = 0,
                time = (int)playTime
            },
            () =>
            {
                SceneManager.LoadScene("MainScene");
            }
        );
    }
}
