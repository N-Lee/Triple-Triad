using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMenu : MonoBehaviour
{
    public GameObject panelPrefab, cardButtonPrefabPC, cardButtonPrefabMobile, cardListObj, pageObj;
    public List<GameObject> cardButtonList = new List<GameObject>();
    GameObject cardButtonPrefab;
    List<Card> cardList;
    List<GameObject> panelList = new List<GameObject>();
    int cardButtonX = 27;
    int cardButtonY = 314;
    int cardButtonHeight = 75;
    int currentPage = 1;
    int pageIndex = 1;
    float menuWidth;
    // Start is called before the first frame update
    void Start()
    {
        menuWidth = gameObject.GetComponent<RectTransform>().rect.width;
    #if (UNITY_IOS || UNITY_ANDROID)
        cardButtonPrefab = cardButtonPrefabMobile;
    #else
        cardButtonPrefab = cardButtonPrefabPC;
    #endif
    }

    public void SetCardList(List<Card> cardList)
    {
        this.cardList = cardList;
    }

    public void FillMenu()
    {
        GameObject panel = new GameObject();
        int pageCardSize = 10;
        int panelCount = 0;
        pageIndex = 1;
        currentPage = 1;

        for (int i = 0; i < cardList.Count; i++)
        {
            
            if (i % pageCardSize == 0)
            {
                panel = Instantiate(panelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                panel.transform.SetParent(cardListObj.transform, false);
                RectTransform panelRect = panel.GetComponent<RectTransform>();

                if (cardList.Count - i < pageCardSize)
                {
                    panelRect.anchoredPosition = new Vector2(panelList[0].GetComponent<RectTransform>().anchoredPosition.x - menuWidth, 0);
                    panelRect.transform.localScale = new Vector2(1, 1);
                    panelCount++;
                    panelList.Insert(0, panel);
                }
                else
                {
                    panelRect.anchoredPosition = new Vector2(panelCount * menuWidth, 0);
                    panelRect.transform.localScale = new Vector2(1, 1);
                    panelCount++;
                    panelList.Add(panel);
                }
            }

            GameObject newCardButton = Instantiate(cardButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCardButton.GetComponent<CardButton>().index = i;
            newCardButton.transform.SetParent(panel.transform, false);
            newCardButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(cardButtonX, cardButtonY - (i%pageCardSize * cardButtonHeight));
            newCardButton.GetComponent<RectTransform>().transform.localScale = new Vector2(1, 1);
            newCardButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = cardList[i].title;
            cardButtonList.Add(newCardButton);
        }
    }

    public void ChangePage(int difference)
    {
        currentPage += difference;
        pageIndex += difference;
        if (currentPage == panelList.Count+1)
        {
            currentPage = 1;
        } 
        else if (currentPage == 0)
        {
            currentPage = panelList.Count;
        }
        
        if (pageIndex == panelList.Count - 1)
        {
            GameObject tempPanel = panelList[0];
            tempPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(panelList[panelList.Count - 1].GetComponent<RectTransform>().anchoredPosition.x + menuWidth, 0);
            panelList.RemoveAt(0);
            panelList.Add(tempPanel);
            pageIndex = panelList.Count - 2;
        }
        else if (pageIndex == 0)
        {
            GameObject tempPanel = panelList[panelList.Count - 1];
            tempPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(panelList[0].GetComponent<RectTransform>().anchoredPosition.x - menuWidth, 0);
            panelList.RemoveAt(panelList.Count - 1);
            panelList.Insert(0, tempPanel);
            pageIndex = 1;
        }
        pageObj.GetComponent<TextMeshProUGUI>().text = currentPage.ToString();
    }
}
