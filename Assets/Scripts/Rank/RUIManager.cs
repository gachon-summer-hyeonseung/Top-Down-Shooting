using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform itemPrefab;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private int itemCount = 10;
    private int page = 1;

    private bool addable = true;
    private bool waitOneFrame = false;

    void Start()
    {

    }

    void Update()
    {
        if (addable && scrollRect.verticalNormalizedPosition < 0.01f)
        {
            if (waitOneFrame)
            {
                waitOneFrame = false;
                return;
            }
            AddItem();
            waitOneFrame = true;
        }
    }

    void AddItem()
    {
        addable = false;
        RankManager.Instance.GetRank(page, itemCount, (dataList) =>
        {
            if (dataList.Count == 0)
            {
                addable = false;
                return;
            }

            foreach (var item in dataList)
            {
                RectTransform itemInstance = Instantiate(itemPrefab, content);
                itemInstance.GetComponent<Item>().SetData(item);
            }

            page++;
            addable = true;
        });
    }

    public void ToLobby()
    {
        GameManager.Instance.GoLobby();
    }
}
