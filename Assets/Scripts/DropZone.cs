using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropZone : MonoBehaviour
{
    public int row;
    public int column;
    public int element = 0; // 0 = None | 1 = Earth | 2 = Fire | 3 = Water | 4 = Poison | 5 = Holy | 6 = Lightning | 7 = Wind | 8 Ice
    GameObject cardObj;
    public bool isEmpty = true;
    GameEngine gameEngine;
    
    [SerializeField] GameObject elementObj;
    Animator anim;
    Image elementImage;
    [SerializeField] Sprite plusOne, minusOne;
    private List<Sprite> sprites;

    public GameObject GetCardObj()
    {
        return cardObj;
    }

    public void SetCardObj(GameObject cardObj)
    {
        this.cardObj = cardObj;
    }

    public void PlaceCardObj(GameObject cardObj)
    {
        this.cardObj = cardObj;
        StartCoroutine(gameEngine.PlaceCard(this));
        elementModifier(cardObj);
    }

    public void SetElement(int element)
    {
        this.element = element;

        if (element == 0)
        {
            anim.enabled = false;
            elementImage.enabled = false;
        }
        else
        {
            anim.enabled = true;
            elementImage.enabled = true;
            switch (element)
            {
                case 1:
                    anim.Play("Earth", 0);
                    break;
                case 2:
                    anim.Play("Fire", 0);
                    break;
                case 3:
                    anim.Play("Water", 0);
                    break;
                case 4:
                    anim.Play("Poison", 0);
                    break;
                case 5:
                    anim.Play("Holy", 0);
                    break;
                case 6:
                    anim.Play("Lightning", 0);
                    break;
                case 7:
                    anim.Play("Wind", 0);
                    break;
                case 8:
                    anim.Play("Ice", 0);
                    break;
            }
        }
       
    }

    void elementModifier(GameObject cardObj)
    {
        anim.enabled = false;
        Card card = cardObj.GetComponent<CardValues>().card;
        if (card.element == element && element != 0)
        {
            elementImage.sprite = plusOne;
        }
        else if (card.element != element && element != 0)
        {
            elementImage.sprite = minusOne;
        }
    }

    void Start()
    {
        gameEngine = GameObject.Find("GameEngine").GetComponent<GameEngine>();
        anim = elementObj.GetComponent<Animator>();
        elementImage = elementObj.GetComponent<Image>();
    }

    void Update()
    {
    
    }
}
