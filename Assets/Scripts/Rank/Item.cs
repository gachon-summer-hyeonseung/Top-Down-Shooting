using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI playTimeText;

    public void SetData(RankData rankData)
    {
        nameText.text = $"이름 : {rankData.name}";
        scoreText.text = $"점수 : {rankData.score}";
        stageText.text = $"스테이지 : {rankData.stage}";
        playTimeText.text = $"플레이 시간 : {rankData.playTime}s";
    }
}
