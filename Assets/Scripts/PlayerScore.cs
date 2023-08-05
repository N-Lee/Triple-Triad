using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

    const int nameOffset = 68;
    const string imageNameStart = "cardstext_13-edit_";

    void UpdateScore(int score)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageNameStart + (nameOffset+score).ToString());
    }
}
