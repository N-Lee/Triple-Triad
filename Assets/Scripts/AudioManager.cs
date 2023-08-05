using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    string bgm = "Audio/bgm";
    string win = "Audio/win";
    string flip = "Audio/flip";
    string rule = "Audio/rule";
    string cursor = "Audio/cursor";
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayWinAudio()
    {
        audioSource.clip = Resources.Load<AudioClip>(win);
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayBgmAudio()
    {
        audioSource.clip = Resources.Load<AudioClip>(bgm);
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayFlipSFX()
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(flip));
    }

    public void PlayRuleSFX()
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(rule));
    }

    public void PlayCursorSFX()
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(cursor));
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
