using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject gameEngine, same, sameWall, suddenDeath, plus, combo, elemental, music, sfx, normal, random;
    Toggle sameToggle, sameWallToggle, suddenDeathToggle, plusToggle, comboToggle, elementalToggle;
    Slider musicSlider, sfxSlider;
    Button normalButton, randomButton;
    Settings settings;

    // Start is called before the first frame update
    void Start()
    {
        settings = gameEngine.GetComponent<Settings>();
        musicSlider = music.GetComponent<Slider>();
    }

    public void SameToggle(bool togg)
    {
        settings.same = togg;
    }
    
    public void SameWallToggle(bool togg)
    {
        settings.sameWall = togg;
    }
    
    public void PlusToggle(bool togg)
    {
       settings.plus = togg; 
    }
    
    public void ComboToggle(bool togg)
    {
       settings.combo = togg; 
    }

    public void ElementalToggle(bool togg)
    {
       settings.elemental = togg; 
    }
    

    
}
