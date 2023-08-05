using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButton : MonoBehaviour
{
    public int index;
    GameObject pointerObj, cardPreview;
    GameEngine gameEngine;
    AudioManager audioManager;
    public bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        pointerObj = GameObject.Find("Pointer");
        audioManager = GameObject.Find("SFX").GetComponent<AudioManager>();
        gameEngine = GameObject.Find("GameEngine").GetComponent<GameEngine>();
    }

    public void Hover()
    {
        pointerObj.GetComponent<Image>().enabled = true;
        pointerObj.transform.position = new Vector3 (pointerObj.transform.position.x, gameObject.transform.position.y, 0);
        audioManager.PlayCursorSFX();
        cardPreview = gameEngine.CardButtonHovered(index);
    }

    public void Unhover()
    {
        pointerObj.GetComponent<Image>().enabled = false;
        Destroy(cardPreview);
    }

    public void Deselect()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        clicked = false; 
    }

    public void Select()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(150, 150, 150, 255);
        clicked = true;
    }

    public void OnClick()
    {
        StartCoroutine(gameEngine.CardButtonClicked(index));

        if (clicked && !gameEngine.cardOwnedByPlayer1)
        {
            Deselect();
        }
        else
        {
            Select();
        }

    }
}
