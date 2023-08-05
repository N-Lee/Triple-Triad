using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardValues : MonoBehaviour
{

    public Card card;
    string title;
    public int index;
    public int gameStartOwner;
    public int row;
    public int col;

    void Update()
    {
        row = card.row;
        col = card.col;
        gameStartOwner = card.gameStartOwner;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetCard(Card card)
    {
        this.card = card;
        title = card.title;
    }
}
