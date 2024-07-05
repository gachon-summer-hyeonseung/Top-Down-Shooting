using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IGUIManager : MonoBehaviour
{
    public static IGUIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score : {score}";
    }
}
