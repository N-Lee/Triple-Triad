using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Settings: MonoBehaviour
{
    public bool same = false;
    public bool sameWall = false;
    public bool plus = false;
    public bool combo = false;
    public bool elemental = false;
    float musicLevels = 1f;
    float sfxLevels = 1f;
    [SerializeField] GameObject sameObj, sameWallObj, plusObj, comboObj, elementalObj, musicSilderObj, sfxSliderObj, musicObj, sfxObj;
    AudioManager musicManager, sfxManager;

    void Start()
    {
        musicManager = musicObj.GetComponent<AudioManager>();
        sfxManager = sfxObj.GetComponent<AudioManager>();
        GetPlayerPrefs();
        SettingsLaunch();
    }

    private void GetPlayerPrefs()
    {
        same = (PlayerPrefs.GetInt("Same", 0) != 0);
        sameWall = (PlayerPrefs.GetInt("SameWall", 0) != 0);
        plus = (PlayerPrefs.GetInt("Plus", 0) != 0);
        combo = (PlayerPrefs.GetInt("Combo", 0) != 0);
        elemental = (PlayerPrefs.GetInt("Elemental", 0) != 0);
        musicLevels = PlayerPrefs.GetFloat("Music", 1f);
        sfxLevels = PlayerPrefs.GetFloat("Sfx", 1f); 
    }

    private void SettingsLaunch()
    {
        sameObj.GetComponent<Toggle>().isOn = same;
        sameWallObj.GetComponent<Toggle>().isOn = sameWall;
        SameWallStatus(same);
        plusObj.GetComponent<Toggle>().isOn = plus;
        comboObj.GetComponent<Toggle>().isOn = combo;
        elementalObj.GetComponent<Toggle>().isOn = elemental;
        musicSilderObj.GetComponent<Slider>().value = musicLevels;
        musicSilderObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Volume: " + Mathf.Floor(musicLevels * 100);
        sfxSliderObj.GetComponent<Slider>().value = sfxLevels;
        sfxSliderObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "SFX: " + Mathf.Floor(sfxLevels * 100);
    }

    public void SameToggle(bool togg)
    {
        same = togg;
        PlayerPrefs.SetInt("Same", (same ? 1 : 0));

        SameWallStatus(same);
    }

    private void SameWallStatus(bool enable)
    {
        sameWallObj.GetComponent<Toggle>().enabled = enable;
        if (enable)
        {
            sameWallObj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else 
        {
            sameWall = false;
            sameWallObj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(150, 150, 150, 255);
        }
        
        if (enable && PlayerPrefs.GetInt("SameWall", 0) != 0)
        {
            sameWall = true;
            
        }
    }
    
    public void SameWallToggle(bool togg)
    {
        sameWall = togg;
        PlayerPrefs.SetInt("SameWall", (sameWall ? 1 : 0));
    }
    
    public void PlusToggle(bool togg)
    {
       plus = togg; 
       PlayerPrefs.SetInt("Plus", (plus ? 1 : 0));
    }
    
    public void ComboToggle(bool togg)
    {
       combo = togg; 
       PlayerPrefs.SetInt("Combo", (combo ? 1 : 0));
    }

    public void ElementalToggle(bool togg)
    {
       elemental = togg; 
       PlayerPrefs.SetInt("Elemental", (elemental ? 1 : 0));
    }

    public void volumeSlider(float slide)
    {
        Slider mySlide = musicSilderObj.GetComponent<Slider>();
        musicLevels = mySlide.value;
        musicManager.SetVolume(musicLevels);
        musicSilderObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Volume: " + Mathf.Floor(musicLevels * 100);
        PlayerPrefs.SetFloat("Music", musicLevels);
    }

    public void sfxSlider(float slide)
    {
        Slider mySlide = sfxSliderObj.GetComponent<Slider>();
        sfxLevels = mySlide.value;
        sfxManager.SetVolume(sfxLevels);
        sfxManager.PlayCursorSFX();
        sfxSliderObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "SFX: " + Mathf.Floor(sfxLevels * 100);
        PlayerPrefs.SetFloat("Sfx", sfxLevels);
    }
}