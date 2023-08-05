using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CardAnimation : MonoBehaviour
{
    Card card = new Card(4,3,1,3, 1, 0, "Bite Bug", "l1BiteBug");
    string cardBackPath = "Cards/back";
    float flipSpeed = 0.5f;
    public string title;
    float hoverDistance;
    GameEngine gameEngine;
    CardDrag cardDrag;
    RectTransform rect;
    void Start()
    {
        gameEngine = GameObject.Find("GameEngine").GetComponent<GameEngine>();
        cardDrag = gameObject.GetComponent<CardDrag>();
        rect = GetComponent<RectTransform>();
        hoverDistance = transform.localScale.x * 0.25f;
    }

    void Update()
    {
        if (Mathf.Abs(transform.eulerAngles.y)%360 >= 90 && Mathf.Abs(transform.eulerAngles.y)%360 < 270)
        {
            SetImage(cardBackPath);
        }
        else 
        {
            SetImage(card.imagePath);
        }
    }
    
    public void SetCard(Card card)
    {
        this.card = card;
        this.card.SetCard(card);
        this.title = card.title;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(card.imagePath); 
    } 

    public void SetImage(string imagePath){
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(imagePath);
    }

    public void RotateTop()
    {
        transform.DORotate(new Vector3(360,0,0), flipSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(1);
    }

    public void RotateLeft()
    {
        transform.DORotate(new Vector3(0,360,0), flipSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(1);
    }

    public void SetPlayer1()
    {
        gameObject.GetComponent<Image>().color = new Color32(200,255,200,255);
    }

    public void SetPlayer2()
    {
        gameObject.GetComponent<Image>().color = new Color32(255,200,200,255);
    }

    public void Hover()
    {
        if (transform.parent.tag != "DropZone")
        {
            if (gameEngine.player1Turn)
            {
                rect.anchoredPosition = new Vector3(rect.anchoredPosition.x + hoverDistance, rect.anchoredPosition.y, 0);
            }
            else
            {
                rect.anchoredPosition = new Vector3(rect.anchoredPosition.x - hoverDistance, rect.anchoredPosition.y, 0);
            }
            gameEngine.ActivateInfoHighlight(title);
        }
    }

    public void Unhover()
    {
        if (transform.parent.tag != "DropZone" && !cardDrag.fixOffset)
        {
            if (gameEngine.player1Turn)
            {
                rect.anchoredPosition = new Vector3(rect.anchoredPosition.x - hoverDistance, rect.anchoredPosition.y, 0);
            }
            else 
            {
                rect.anchoredPosition = new Vector3(rect.anchoredPosition.x + hoverDistance, rect.anchoredPosition.y, 0);
            }
            
            gameEngine.DeactivateInfoHighlight();
        }

        cardDrag.fixOffset = false;
    }
}