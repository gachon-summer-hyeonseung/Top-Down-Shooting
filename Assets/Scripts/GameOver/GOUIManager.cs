using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GOUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dataText;
    [SerializeField] private TMP_InputField nameInput;

    // Start is called before the first frame update
    void Start()
    {
        ShowResult();
    }

    void ShowResult()
    {
        string result = GameManager.Instance.stage == 100 ? "Game Clear!" : "Game Over";
        dataText.text = $"{result}\n\nScore : {GameManager.Instance.point}\nStage : {GameManager.Instance.stage}\nPlay Time : {GameManager.Instance.playTime}s";
    }

    public void Lobby()
    {
        GameManager.Instance.GoLobby();
    }

    public void Register()
    {
        if (string.IsNullOrEmpty(nameInput.text)) return;
        GameManager.Instance.Register(nameInput.text);
    }
}
