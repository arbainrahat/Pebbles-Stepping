using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    [Header("Sources")]
    public AudioSource source;
    public AudioSource backGroundMusic;
    public AudioSource a_Source;
    [Header("Clips")]
    public AudioClip mainMenu;
    public AudioClip GamePlay;
    public AudioClip jump;
    public AudioClip LevelComplete;
    public AudioClip LevelFail;
    public AudioClip jumpInWater;
    public AudioClip click;
    public AudioClip ai_radar;
    public AudioClip water;
    public AudioClip lava;
    public AudioClip ButtonClick;
    public AudioClip splash;
    public AudioClip stars;
   
public static SoundManager _instance{
    get{
        return instance;
    }

}
    private void Awake() 
    {
        if(instance!=null&&instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance=this;
        }
        PlayerPrefs.SetInt("MusicOn",PlayerPrefs.GetInt("MusicOn"));
        PlayerPrefs.SetInt("SoundOn",PlayerPrefs.GetInt("SoundOn"));
        // DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        PlaySound("splash");
        // source = GetComponent<AudioSource>();
        // SetMusic();
        // SetSound();

    }
    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "button":
                source.clip = ButtonClick;
                source.Play();
                break;
            case "levelComplete":
                source.clip = LevelComplete;
                source.Play();
                break;
            case "levelFail":
                source.clip = LevelFail;
                source.Play();
                break;
            case "jump":
                source.clip = jump;
                source.Play();
                break;
            case "click":
                source.clip = click;
                source.Play();
                break;
            case "jumpInWater":
                source.clip = jumpInWater;
                source.Play();
                break;
            case "ai_radar":
                a_Source.clip = ai_radar;
                a_Source.Play();
                break;
            case "water":
                a_Source.clip = water;
                a_Source.Play();
                break; 
            case "splash":
                source.clip = splash;
                source.Play();
                break; 
            case "lava":
                a_Source.clip = lava;
                a_Source.Play();
                break; 
            case "stars":
                source.clip = stars;
                source.Play();
                break;             
        }
    }
    public void Stop_Sound()
    {
        StopSound(a_Source);
    }
    internal void StopSound(AudioSource audio)
    {
        audio.Stop();
    }
    public void ChangeBackGroundMusic(int _clip)
    {
        switch (_clip)
        {
            case 0:
                PlaySound("button");
                backGroundMusic.enabled=true;
                backGroundMusic.clip = mainMenu;
                Debug.Log("Sounded assigned "+ backGroundMusic.clip.name);
                backGroundMusic.Play();
                break;
            case 1:
                backGroundMusic.enabled=true;
                backGroundMusic.clip = GamePlay;
                backGroundMusic.Play();
                break;
        }
    }
    
    public void SetSound(GameObject on, GameObject off)
    {
        if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            // Sound On
            // source.mute = true;
            AudioListener.volume=0;
            
            // if(on != null)
            on.SetActive(true);
            // if(off != null)
            off.SetActive(false);
            // Music
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        else
        {

            source.mute = false;
            AudioListener.volume=1;
            // if(on != null)
            on.SetActive(false);
            // if(off != null)
            off.SetActive(true);
            // Music
            PlayerPrefs.SetInt("MusicOn", 0);
        }
    }
    public void SetMusic(GameObject on, GameObject off)
    {
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            // Music On
            backGroundMusic.mute = true;
            backGroundMusic.Play();
            //  if(on != null)
            on.SetActive(true);
            // if(off != null)
            off.SetActive(false);
        }
        else
        {
            // Music Off
            backGroundMusic.mute = false;
            //  if(on != null)
            on.SetActive(false);
            // if(off != null)
            off.SetActive(true);
        }
    }

    
}
