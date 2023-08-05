using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameEngine : MonoBehaviour
{

#region variables
    GameObject[] player1Cards = new GameObject[5];
    GameObject[] player2Cards = new GameObject[5];
    GameObject[,] defaultBoard = new GameObject[5,5];
    GameObject[,] board = new GameObject[5,5];
    GameObject placedCard, p1Area, p2Area;
    public GameObject gameBlocker, p1Blocker, p2Blocker, p1Score, p2Score,ruleLabel, endLabel, cardPrefab, musicManager, sfxManager, infoHighlight, infoHighlightText, canvas,mainMenuObj, selectMenuObj, confirmationMenuObj, rematchMenuObj, cardPreviewObj, creditsMenuObj, rulesMenuObj;
    List<Card> cardList, flipCards;
    List<GameObject> flipChallengeCards = new List<GameObject>();
    List<GameObject> flipSameCards = new List<GameObject>();
    List<GameObject> flipPlusCards = new List<GameObject>();
    List<GameObject> flipComboCards = new List<GameObject>();
    List<GameObject> dropZones = new List<GameObject>();
    CardDatabase db;
    Settings settings;
    int turnCount = 1;
    int p1Count, p2Count, winner;
    List<int> cardIndexGenerated = new List<int>();
    float labelPause = 0.5f;
    float endGamePause = 1f;
    string scoreImgPath = "Score/";
    bool confirmation = false;
    bool rematch = false;
    public bool player1Turn = true;
    public bool cardOwnedByPlayer1 = false;
#endregion

    public static GameEngine Instance {get; private set;} 

#region Unity

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize cards and settings
        db = GetComponent<CardDatabase>();
        cardList = db.getCardList();
        settings = GetComponent<Settings>();
        p1Area = GameObject.Find("Player1Area");
        p2Area = GameObject.Find("Player2Area");

        // Initialize the card list menu
        SelectMenu menu = selectMenuObj.GetComponent<SelectMenu>();
        menu.SetCardList(cardList);
        menu.FillMenu();
        selectMenuObj.transform.parent.gameObject.SetActive(false);

        // Initial board set up
        getDropZones();
        StartBoard();
        infoHighlight.SetActive(false);
        ClickMainMenu();
    }

#endregion

#region Board
    void getDropZones()
    {
        GameObject cardPosition = GameObject.Find("CardPositions");
        foreach (Transform child in cardPosition.transform){
            dropZones.Add(child.gameObject);
        }
    }

    GameObject GetWallCard()
    {
        GameObject wallCard = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
        wallCard.GetComponent<CardAnimation>().SetCard(db.getWall());
        wallCard.GetComponent<CardValues>().SetCard(db.getWall());
        wallCard.transform.SetParent(GameObject.Find("Wall").transform, false);
        return wallCard;
    }

    GameObject GetEmptyCard()
    {
        GameObject emptyCard = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
        emptyCard.GetComponent<CardAnimation>().SetCard(db.getEmpty());
        emptyCard.GetComponent<CardValues>().SetCard(db.getEmpty());
        emptyCard.transform.SetParent(GameObject.Find("Wall").transform, false);
        return emptyCard; 
    }

    void StartBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i == 0 || i == 4 || j == 0 || j == 4)
                {
                    GameObject wall = GetWallCard();
                    board[i,j] = wall;
                    defaultBoard[i,j] = wall;
                } else {
                    GameObject empty = GetEmptyCard();
                    board[i,j] = empty;
                    defaultBoard[i,j] = empty;
                }
            }
        }
    }

    void FillDropzones()
    {
        int row = 1;
        int column = 1;
        int i = 1;
        HashSet<int> elementDropzones = GetRandomNumber(1,9,3);

        foreach (GameObject obj in dropZones){
            obj.GetComponent<DropZone>().isEmpty = true;
            DropZone dropzone = obj.GetComponent<DropZone>();

            if (settings.elemental)
            {
                if (rematch)
                {
                    dropzone.SetElement(dropzone.element);
                }
                else if (elementDropzones.Contains(i))
                {
                    dropzone.SetElement(Random.Range(1,8));
                }
                else
                {
                    dropzone.SetElement(0);
                }
            }
            dropzone.SetCardObj(defaultBoard[row,column]);

            i++;
            column++;
            if (column > 3)
            {
                column = 1;
                row++;
            }
        }
    }

    HashSet<int> GetRandomNumber(int from, int to, int numberOfElement)
    {
        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < numberOfElement)
        {
            numbers.Add(Random.Range(from, to));
        }
        return numbers;
    }

    void ResetGame()
    {
        FillDropzones();

        // If not a rematch, delete cards in the players' hands
        if (!rematch)
        {
            for (int i = 0; i < player1Cards.Length; i++)
            {
                Destroy(player1Cards[i]);
                Destroy(player2Cards[i]);
            }
        }
        
        // Loop through the entire board. If not a rematch, destroy all cards. If rematch, set cards to respective owners
        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                Card onBoard = board[i,j].GetComponent<CardValues>().card;

                if (!onBoard.IsBlank() && !rematch)
                {
                    Destroy(board[i,j]);
                }
                else if (!onBoard.IsBlank() && rematch && onBoard.gameStartOwner == 1)
                {
                    board[i,j].transform.SetParent(p1Area.transform, false);
                    board[i,j].GetComponent<CardAnimation>().SetPlayer1();
                    onBoard.owner = onBoard.gameStartOwner;
                }
                else if (!onBoard.IsBlank() && rematch && onBoard.gameStartOwner == 2)
                {
                    board[i,j].transform.SetParent(p2Area.transform, false);
                    board[i,j].GetComponent<CardAnimation>().SetPlayer2();
                    onBoard.owner = onBoard.gameStartOwner;
                }
                
                board[i,j] = defaultBoard[i,j];
            }
        }

        gameBlocker.SetActive(false);
        turnCount = 1;
        player1Turn = true;
        p1Score.GetComponent<Image>().sprite = Resources.Load<Sprite>(scoreImgPath + "5"); 
        p2Score.GetComponent<Image>().sprite = Resources.Load<Sprite>(scoreImgPath + "5"); 
        endLabel.SetActive(false);
    }

    void StartGame()
    {
        turnCount = 1;
        player1Turn = turnCount%2 == 1;

        if (player1Turn)
        {
            p1Blocker.GetComponent<Image>().enabled = false;
            p2Blocker.GetComponent<Image>().enabled = true;
        }
        else 
        {
            p2Blocker.GetComponent<Image>().enabled = false;
            p1Blocker.GetComponent<Image>().enabled = true; 
        }

    }

    IEnumerator EndGame()
    {
        string lose = "label/lose";
        string win = "label/win";
        string draw = "label/draw";
        float winLoseWidth = 1075f;
        float drawWidth = 747f;
        Image endLabelImg = endLabel.GetComponent<Image>();
        RectTransform endLabelTransform = endLabel.GetComponent<RectTransform>();

        switch (winner)
        {
            case 0:
                endLabelImg.sprite = Resources.Load<Sprite>(draw);
                endLabelTransform.sizeDelta = new Vector2(drawWidth, endLabelTransform.sizeDelta.y);
                break;
            case 1:
                endLabelImg.sprite = Resources.Load<Sprite>(win);
                endLabelTransform.sizeDelta = new Vector2(winLoseWidth, endLabelTransform.sizeDelta.y);
                musicManager.GetComponent<AudioManager>().PlayWinAudio();
                break;
            case 2:
                endLabelImg.sprite = Resources.Load<Sprite>(lose);
                endLabelTransform.sizeDelta = new Vector2(winLoseWidth, endLabelTransform.sizeDelta.y);
                break;
        }

        DeactivateInfoHighlight();
        endLabel.SetActive(true);
        yield return new WaitForSecondsRealtime(endGamePause);
        endLabel.SetActive(false);
        rematchMenuObj.SetActive(true);
    }
#endregion

#region Modes and Creation
    public void RandomMode()
    {
        ResetGame();
        for (int i = 0; i < 10; i++)
        {
            GameObject newCard;
            newCard = GetRandomCard();

            if (i < 5)
            {
                newCard.transform.SetParent(p1Area.transform, false);
                newCard.GetComponent<CardValues>().card.owner = 1;
                newCard.GetComponent<CardValues>().card.gameStartOwner = 1;
                newCard.GetComponent<CardAnimation>().SetPlayer1();
                player1Cards[i%5] = newCard;
            }
            else
            {
                newCard.transform.SetParent(p2Area.transform, false);
                newCard.GetComponent<CardValues>().card.owner = 2;
                newCard.GetComponent<CardValues>().card.gameStartOwner = 2;
                newCard.GetComponent<CardAnimation>().SetPlayer2();
                player2Cards[i%5] = newCard;
            }
        }

        mainMenuObj.SetActive(false);
        StartGame();
    }

    public void NormalMode()
    {
        mainMenuObj.SetActive(false);
        selectMenuObj.transform.parent.gameObject.SetActive(true);
        player1Turn = true;
        StartCoroutine(WaitCardSelect());
        ResetGame();
    }

    IEnumerator WaitCardSelect()
    {
        yield return new WaitUntil(() => !selectMenuObj.transform.parent.gameObject.activeSelf);
        StartGame();
    }

    void TestPlusSameMode()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newCard;
            if (i ==1){
            newCard = CreateCardFromIndex(105);}
            else if (i == 5){
                newCard = CreateCardFromIndex(104);
            } else if (i == 6){
                newCard = CreateCardFromIndex(103);
            } else {
                newCard = GetRandomCard();
            }
            if (i < 5)
            {
                newCard.transform.SetParent(p1Area.transform, false);
                newCard.GetComponent<CardValues>().card.owner = 1;
                newCard.GetComponent<CardAnimation>().SetPlayer1();
                player1Cards[i%5] = newCard;
            }
            else
            {
                newCard.transform.SetParent(p2Area.transform, false);
                newCard.GetComponent<CardValues>().card.owner = 2;
                newCard.GetComponent<CardAnimation>().SetPlayer2();
                player2Cards[i%5] = newCard;
            }
        }
    }

    void ShowAllCards()
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.GetComponent<CardAnimation>().SetCard(cardList[i]);
            if (i%2 == 0)
            {
                newCard.transform.SetParent(p1Area.transform, false);
            }
            else
            {
                newCard.transform.SetParent(p2Area.transform, false);
            }
        }
    }

    public GameObject CreateCardFromIndex(int i)
    {
        CardDatabase data = GetComponent<CardDatabase>();
        List<Card> tempCardList = data.getCardList();

        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.GetComponent<CardAnimation>().SetCard(tempCardList[i]);
        newCard.GetComponent<CardValues>().SetCard(tempCardList[i]);
        newCard.GetComponent<CardValues>().index = i;
        newCard.GetComponent<RectTransform>().transform.localScale = new Vector2(365, 365);
        return newCard;
    }

    GameObject GetRandomCard()
    {
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        int rng;

        do {
            rng = Random.Range(0, cardList.Count);
        } while (cardIndexGenerated.Contains(rng));

        cardIndexGenerated.Add(rng);
        newCard.GetComponent<CardAnimation>().SetCard(cardList[rng]);
        newCard.GetComponent<CardValues>().SetCard(cardList[rng]);
        newCard.GetComponent<RectTransform>().transform.localScale = new Vector2(365, 365);
        return newCard;
    }

    public GameObject CardButtonHovered(int index)
    {
        GameObject cardPreview = CreateCardFromIndex(index);
        cardPreview.transform.SetParent(cardPreviewObj.transform, false);
        return cardPreview;
    }

    public void CardButtonUnhovered(GameObject cardPreview)
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        CardButton cardButton = button.GetComponent<CardButton>();
        Destroy(cardPreview);
    }


    public IEnumerator CardButtonClicked(int index)
    {

        GameObject button = EventSystem.current.currentSelectedGameObject;
        CardButton cardButton = button.GetComponent<CardButton>();
        cardOwnedByPlayer1 = false;

        if (cardButton.clicked && !player1Turn)
        {
            foreach (Transform child in p1Area.transform)
            {
                if (child.gameObject.GetComponent<CardValues>().card.gameStartOwner == 1)
                {
                    cardOwnedByPlayer1 = true;
                }
            }
        }

        if (!cardOwnedByPlayer1)
        {
            if (!cardButton.clicked)
            {
                //Create card
                GameObject newCard = CreateCardFromIndex(index);

                if (player1Turn)
                {
                    newCard.transform.SetParent(p1Area.transform, false);
                    newCard.GetComponent<CardValues>().card.owner = 1;
                    newCard.GetComponent<CardValues>().card.gameStartOwner = 1;
                    newCard.GetComponent<CardAnimation>().SetPlayer1();
                    player1Cards[p1Area.transform.childCount - 1] = newCard;
                }
                else 
                {
                    newCard.transform.SetParent(p2Area.transform, false);
                    newCard.GetComponent<CardValues>().card.owner = 2;
                    newCard.GetComponent<CardValues>().card.gameStartOwner = 2;
                    newCard.GetComponent<CardAnimation>().SetPlayer2();
                    player2Cards[p2Area.transform.childCount - 1] = newCard;
                }
            }
            else
            {
                //Destroy card
                Transform playerArea = p1Area.transform;
                if (!player1Turn)
                {
                    playerArea = p2Area.transform;
                }
            
                for (int i = 0; i < playerArea.childCount; i++)
                {
                    if (playerArea.transform.GetChild(i).GetComponent<CardValues>().index == index)
                    {
                        Destroy (playerArea.transform.GetChild(i).gameObject);
                    }
                }
            }

            if (p1Area.transform.childCount == 5 && player1Turn)
            {

                confirmationMenuObj.SetActive(true);
                yield return new WaitUntil(() => !confirmationMenuObj.activeSelf);

                if (confirmation)
                {
                    player1Turn = false;
                    List<GameObject> cardButtonList = selectMenuObj.GetComponent<SelectMenu>().cardButtonList;
                    /*
                    for (int i = 0; i < p1Area.transform.childCount; i++)
                    {
                        int cardIndex = p1Area.transform.GetChild(i).GetComponent<CardValues>().index;
                        cardButtonList[cardIndex].GetComponent<CardButton>().Deselect();
                    }
                    */
                }
                else
                {
                    Destroy (p1Area.transform.GetChild(4).gameObject);
                    cardButton.Deselect();
                }
            }
            
        }

        if (p2Area.transform.childCount == 5 && !player1Turn)
        {

            confirmationMenuObj.SetActive(true);
            yield return new WaitUntil(() => !confirmationMenuObj.activeSelf);

            if (confirmation)
            {
                player1Turn = true;
                List<GameObject> cardButtonList = selectMenuObj.GetComponent<SelectMenu>().cardButtonList;

                for (int i = 0; i < p2Area.transform.childCount; i++)
                {
                    int cardIndex = p2Area.transform.GetChild(i).GetComponent<CardValues>().index;
                    cardButtonList[cardIndex].GetComponent<CardButton>().Deselect();
                    cardIndex = p1Area.transform.GetChild(i).GetComponent<CardValues>().index;
                    cardButtonList[cardIndex].GetComponent<CardButton>().Deselect();
                }

                selectMenuObj.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                Destroy (p2Area.transform.GetChild(4).gameObject);
                cardButton.Deselect();
            }
            
        }

        yield return 0;
    }

#endregion
    
#region Place Card
    public IEnumerator PlaceCard(DropZone dropzone)
    {
        gameBlocker.SetActive(true);
        int row = dropzone.row;
        int column = dropzone.column;
        GameObject cardObj = dropzone.GetCardObj();
        Card placeCardVal = cardObj.GetComponent<CardValues>().card;
        placeCardVal.row = row;
        placeCardVal.col = column;
        placeCardVal.SetElementModifier(dropzone.element);
        board[row, column] = cardObj;
        placedCard = cardObj;

        Battle(dropzone);

        for (int i = 0; i < 4; i++)
        {
            Coroutine swap = StartCoroutine(SwapOwnership(i));
            yield return swap;
        }

        if (player1Turn)
        {
            p1Blocker.GetComponent<Image>().enabled = true;
            p2Blocker.GetComponent<Image>().enabled = false;

        } else {
            p1Blocker.GetComponent<Image>().enabled = false;
            p2Blocker.GetComponent<Image>().enabled = true;
        }

        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                board[i,j].GetComponent<CardValues>().card.flippedThisTurn = false;
            }
        }

        flipChallengeCards.Clear();
        flipSameCards.Clear();
        flipPlusCards.Clear();
        flipComboCards.Clear();

        turnCount++;
        player1Turn = !player1Turn;
        UpdateScore();

        if (turnCount >= 10)
        {
            StartCoroutine(EndGame());
        }
        else 
        {
            gameBlocker.SetActive(false);
        }
    }

    void Battle(DropZone dropzone)
    {
        dropzone.isEmpty = false;

        if (settings.same){
            SameWall(dropzone.GetCardObj(), settings.sameWall, settings.combo);
        }
        
        if (settings.plus)
        {
            Plus(dropzone.GetCardObj(), settings.combo);
        }
        Challenge(dropzone.GetCardObj());
        
    }

    CardValues[] GetSurroundingCards(GameObject cardObj)
    {
        int row = cardObj.GetComponent<CardValues>().card.row;
        int column = cardObj.GetComponent<CardValues>().card.col;
        Debug.Log(cardObj.GetComponent<CardValues>().card.title + " " + row + " " + column);
        CardValues topCard = board[row-1,column].GetComponent<CardValues>();
        CardValues rightCard = board[row,column+1].GetComponent<CardValues>();
        CardValues bottomCard = board[row+1,column].GetComponent<CardValues>();
        CardValues leftCard = board[row,column-1].GetComponent<CardValues>();
        topCard.card.defenseDirection= "up";
        rightCard.card.defenseDirection= "right";
        bottomCard.card.defenseDirection= "down";
        leftCard.card.defenseDirection= "left";
        CardValues[] surroundingCards = {topCard, rightCard, bottomCard, leftCard};

        return surroundingCards;
    }

    void Challenge(GameObject cardObj)
    {
        CardValues attackCard = cardObj.GetComponent<CardValues>();
        CardValues[] defense = GetSurroundingCards(cardObj);

        foreach (CardValues defendCard in defense)
        {

            if (!defendCard.card.IsWall() 
            && attackCard.card.owner != defendCard.card.owner 
            && defendCard.card.owner != 0
            && AttackWon(attackCard.card.GetAttackPower(defendCard.card.defenseDirection), defendCard.card.GetDefensePower())
            && !defendCard.card.flippedThisTurn)
            {
                flipChallengeCards.Add(defendCard.gameObject);
            } 
        }
    }

    bool AttackWon (int attacker, int defender)
    {
        return (attacker > defender) ? true : false;
    }

    void SameWall(GameObject cardObj, bool wall, bool combo)
    {
        CardValues attackCard = cardObj.GetComponent<CardValues>();

        CardValues[] defense = GetSurroundingCards(cardObj); 

        for (int i = 0; i < defense.Length-1; i++)
        {
            for (int j = i+1; j < defense.Length; j++)
            {
                if (wall 
                && attackCard.card.GetAttackPower(defense[i].card.defenseDirection) == defense[i].card.GetDefensePower() 
                && attackCard.card.GetAttackPower(defense[j].card.defenseDirection) == defense[j].card.GetDefensePower())
                {
                    if (attackCard.card.owner != defense[i].card.owner && !defense[i].card.IsWall())
                    {
                        flipSameCards.Add(defense[i].gameObject);
                        if (combo)
                        {                            
                            ComboChallenge(defense[i].gameObject);
                        }
                    }
                    if (attackCard.card.owner != defense[j].card.owner && !defense[j].card.IsWall())
                    {
                        flipSameCards.Add(defense[j].gameObject);
                        if (combo)
                        {
                            ComboChallenge(defense[j].gameObject);
                        }
                    }
                }
                else if (!wall 
                && attackCard.card.GetAttackPower(defense[i].card.defenseDirection) == defense[i].card.GetDefensePower() 
                && attackCard.card.GetAttackPower(defense[j].card.defenseDirection) == defense[j].card.GetDefensePower()
                && !defense[i].card.IsWall()
                && !defense[j].card.IsWall()
                && !defense[i].card.IsBlank()
                && !defense[j].card.IsBlank())
               {
                    if (attackCard.card.owner != defense[i].card.owner)
                    {
                        flipSameCards.Add(defense[i].gameObject);
                        if (combo)
                        {                           
                            ComboChallenge(defense[i].gameObject);
                        }
                    }
                    if (attackCard.card.owner != defense[j].card.owner)
                    {
                        flipSameCards.Add(defense[j].gameObject);
                        if (combo)
                        {
                            ComboChallenge(defense[j].gameObject);
                        }
                    }
                }
            }
        }
    }

    void Plus(GameObject cardObj, bool combo)
    {
        CardValues attackCard = cardObj.GetComponent<CardValues>();
        CardValues[] defense = GetSurroundingCards(cardObj); 

        for (int i = 0; i < defense.Length-1; i++)
        {
            for (int j = i+1; j < defense.Length; j++)
            {
                if (attackCard.card.GetAttackPower(defense[i].card.defenseDirection) + defense[i].card.GetDefensePower() == attackCard.card.GetAttackPower(defense[j].card.defenseDirection) + defense[j].card.GetDefensePower()
                && (attackCard.card.owner != defense[i].card.owner
                || attackCard.card.owner != defense[j].card.owner)
                && !defense[i].card.IsWall()
                && !defense[j].card.IsWall()
                && !defense[i].card.IsBlank()
                && !defense[j].card.IsBlank())
                {
                    if (attackCard.card.owner != defense[i].card.owner)
                    {
                        flipPlusCards.Add(defense[i].gameObject);
                        if (combo)
                        {
                            ComboChallenge(defense[i].gameObject);
                        }
                    }
                    if (attackCard.card.owner != defense[j].card.owner)
                    {
                        flipPlusCards.Add(defense[j].gameObject);
                        if (combo)
                        {
                            ComboChallenge(defense[j].gameObject);
                        }
                    }
                }
            }
        }
    }

    void ComboChallenge(GameObject cardObj)
    {
        CardValues attackCard = cardObj.GetComponent<CardValues>();
        CardValues[] defense = GetSurroundingCards(cardObj);
        foreach (CardValues defendCard in defense)
        {

            if (!defendCard.card.IsWall() 
            && !defendCard.card.IsBlank()
            && defendCard.card.owner != 0
            && AttackWon(attackCard.card.GetAttackPower(defendCard.card.defenseDirection), defendCard.card.GetDefensePower())
            && !defendCard.card.flippedThisTurn)
            {
                flipComboCards.Add(defendCard.gameObject);
            } 
        }
    }

    IEnumerator SwapOwnership(int order)
    {
        bool flipped = false;
        List<GameObject> toFlip = new List<GameObject>();
        Coroutine wait;

        if (order == 0 && flipChallengeCards.Count != 0)
        {
            toFlip = flipChallengeCards;
        }
        else if (order == 1 && flipSameCards.Count != 0)
        {
            toFlip = flipSameCards;
            wait = StartCoroutine(ShowRuleLabel("label/same"));
            yield return wait;
        }
        else if (order == 2 && flipPlusCards.Count != 0)
        {
            toFlip = flipPlusCards;
            wait = StartCoroutine(ShowRuleLabel("label/Plus"));
            yield return wait;
        }
        else if (order == 3 && flipComboCards.Count != 0)
        {
            toFlip = flipComboCards;
            wait = StartCoroutine(ShowRuleLabel("label/combo"));
            yield return wait;
        }

        foreach (GameObject cardObj in toFlip)
        {
            CardAnimation cardAnim = cardObj.GetComponent<CardAnimation>();
            Card card = cardObj.GetComponent<CardValues>().card;
            card.flippedThisTurn = true;

            placedCard.GetComponent<CardValues>().card.DebugLog();
            card.DebugLog();

            if (card.defenseDirection.Equals("left") || card.defenseDirection.Equals("right"))
            {
                cardAnim.RotateLeft();
            } 
            else 
            {
                cardAnim.RotateTop();
            }

            card.owner = placedCard.GetComponent<CardValues>().card.owner;

            if (card.owner == 1)
            {
                cardAnim.SetPlayer1();
            } 
            else 
            {
                cardAnim.SetPlayer2();
            }

            flipped = true;
        }

        if (flipped)
        {
            sfxManager.GetComponent<AudioManager>().PlayFlipSFX();
            yield return new WaitForSecondsRealtime(labelPause);
        }
    }
#endregion

#region UI
    IEnumerator ShowRuleLabel(string label)
    {
        ruleLabel.GetComponent<Image>().sprite = Resources.Load<Sprite>(label);
        ruleLabel.SetActive(true);
        sfxManager.GetComponent<AudioManager>().PlayRuleSFX();
        yield return new WaitForSecondsRealtime(labelPause);
        ruleLabel.SetActive(false);
    }

    void UpdateScore()
    {
        p1Count = 0;
        p2Count = 0;

        for (int i = 0; i < player1Cards.Length; i++)
        {
            if (player1Cards[i].GetComponent<CardValues>().card.owner == 1)
            {
                p1Count++;
            } else {
                p2Count++;
            }

            if (player2Cards[i].GetComponent<CardValues>().card.owner == 1)
            {
                p1Count++;
            } else {
                p2Count++;
            }
        }
        
        p1Score.GetComponent<Image>().sprite = Resources.Load<Sprite>(scoreImgPath + p1Count); 
        p2Score.GetComponent<Image>().sprite = Resources.Load<Sprite>(scoreImgPath + p2Count);

        if (p1Count > p2Count)
        {
            winner = 1;
        }
        else if (p1Count < p2Count)
        {
            winner = 2;
        }
        else
        {
            winner = 0;
        }
    }

    public void ActivateInfoHighlight(string text)
    {
        infoHighlight.SetActive(true);
        infoHighlightText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void DeactivateInfoHighlight()
    {
        infoHighlight.SetActive(false);
    }

    public void ClickYes()
    {
        confirmationMenuObj.SetActive(false);
        confirmation = true;
    }

    public void ClickNo()
    {
        confirmationMenuObj.SetActive(false);
        confirmation = false;
    }

    public void ClickRematch()
    {
        rematchMenuObj.SetActive(false);
        rematch = true;
        ResetGame();
        if (winner != 1)
        {
            musicManager.GetComponent<AudioManager>().PlayBgmAudio();
        }
    }

    public void ClickMainMenu()
    {
        rematchMenuObj.SetActive(false);
        rematch = false;
        mainMenuObj.SetActive(true);
        if (winner != 0)
        {
            musicManager.GetComponent<AudioManager>().PlayBgmAudio();
        }
    }

    public void ClickRules()
    {
        rulesMenuObj.SetActive(true);
    }

    public void ClickCredits()
    {
        creditsMenuObj.SetActive(true);
    }

    public void ClickCloseButton()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }
#endregion

}
