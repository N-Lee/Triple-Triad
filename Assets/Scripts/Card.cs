using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card 
{
    #region PUBLIC
    public int powerLeft = 0;
    public int powerRight = 0;
    public int powerUp = 0;
    public int powerDown = 0;
    public int level;
    
    public int element; // 0 = None | 1 = Earth | 2 = Fire | 3 = Water | 4 = Poison | 5 = Holy | 6 = Lightning | 7 = Wind | 8 Ice
    int elementModifier = 0;
    public string title;
    public string imagePath;
    public int owner = 0; // 0 = Neutral | 1 = Player 1 | 2 = Player 2
    public int gameStartOwner = 0;
    public int row = -1;
    public int col = -1;
    public string defenseDirection;
    public bool flippedThisTurn = false;
    public Vector3 position = new Vector3();
    #endregion

    #region PRIVATE
   
    Vector3 posTopLeft = new Vector3(-320f,414f,0);
    Vector3 posTopCenter= new Vector3(0,414f,0);
    Vector3 posTopRight = new Vector3(320f,414f,0);
    Vector3 posCenterLeft = new Vector3(-320f,0,0);
    Vector3 posCenter = new Vector3(0,0,0);
    Vector3 posCenterRight = new Vector3(0,414f,0);
    Vector3 posBottomLeft = new Vector3(-320f,-414f,0);
    Vector3 posBottomCenter = new Vector3(0,-414f,0);
    Vector3 posBottomRight = new Vector3(320f,-414f,0);
    

    #endregion

    public Card(int powerLeft, 
    int powerRight, 
    int powerUp, 
    int powerDown, 
    int level, 
    int element,
    string title, 
    string imageTitle)
    {
        
        this.powerLeft = powerLeft;
        this.powerRight = powerRight;
        this.powerUp = powerUp;
        this.powerDown = powerDown;
        this.level = level;
        this.element = element;
        this.title = title;
        this.imagePath = "Cards/" + imageTitle;
    }

    public void SetCard(Card card)
    {
        powerLeft = card.powerLeft;
        powerRight = card.powerRight;
        powerUp = card.powerUp;
        powerDown = card.powerDown;
        level = card.level;
        element = card.element;
        title = card.title;
        imagePath = card.imagePath;
    } 

    public void SetImage(string image)
    {
        imagePath = image;
    }

    bool Equals(Card card)
    {
        if (this.powerLeft == card.powerLeft &&
        this.powerRight == card.powerRight &&
        this.powerUp == card.powerUp &&
        this.powerDown == card.powerDown &&
        this.level == card.level &&
        this.element == card.element &&
        this.title.Equals(card.title))
        {
            return true;
        }
        return false;
    }

    public bool IsBlank()
    {
        if (powerLeft == 0 &&
        powerRight == 0 &&
        powerUp == 0 &&
        powerDown == 0){
            return true;
        }
        return false;
    }

    public bool IsWall()
    {
        if (powerLeft == 10 &&
        powerRight == 10 &&
        powerUp == 10 &&
        powerDown == 10){
            return true;
        }
        return false;
    }

    int GetPowerLeft()
    {   
        return powerLeft + elementModifier;
    }

    int GetPowerRight()
    {   
        return powerRight + elementModifier;
    }

    int GetPowerUp()
    {
        return powerUp + elementModifier;
    }

    int GetPowerDown()
    {  
        return powerDown + elementModifier;
    }

    public void SetElementModifier(int dropZoneElement)
    {
        if (element == dropZoneElement && dropZoneElement != 0)
        {
            elementModifier = 1;
        }
        else if (element != dropZoneElement && dropZoneElement != 0)
        {
            elementModifier = -1;
        }
        else 
        {
            elementModifier = 0;
        }
    }

    public int GetElementModifier()
    {
        return elementModifier;
    }

    public int GetDefensePower()
    {

        switch (defenseDirection)
        {
            case "up":
                return GetPowerDown();

            case "right":
                return GetPowerLeft();
            
            case "down":
                return GetPowerUp();
            
            case "left":
                return GetPowerRight();
        }

        return 10;
    }

     public int GetAttackPower(string defenseDirection)
    {

        switch (defenseDirection)
        {
            case "up":
                return GetPowerUp();

            case "right":
                return GetPowerRight();
            
            case "down":
                return GetPowerDown();
            
            case "left":
                return GetPowerLeft();
        }

        return 10;
    }

    public void DebugLog()
    {
        Debug.Log(title + GetPowerUp() + ", " + GetPowerRight() + ", "+ GetPowerDown() + ", "+ GetPowerLeft());
    }
}
