using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    void Awake(){ instance = this; }
    public AudioClip sfx_landing, sfx_collect, sfx_jumping, sfx_hurt;
    public AudioClip music_background, music_win, music_lose;
    public GameObject currentMusicObject;
    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch(sfxName)
        {
            case "landing":
                SoundObjectCreation(sfx_landing);
                break;
            case "collect":
                SoundObjectCreation(sfx_collect);
                break;
            case "jumping":
                SoundObjectCreation(sfx_jumping);
                break;
            case "hurt":
                SoundObjectCreation(sfx_hurt);
                break;
            default:
                break;

        }
    }
    void SoundObjectCreation(AudioClip clip)
    {
        GameObject newObject = Instantiate(soundObject, transform);
        
        newObject.GetComponent<AudioSource>().clip = clip;

        newObject.GetComponent<AudioSource>().Play();
    }
        public void PlayMusic(string musicName)
    {
        switch(musicName)
        {
            case "background":
                MusicObjectCreation(music_background);
                break;
            default:
                break;
        }
    }
    void MusicObjectCreation(AudioClip clip)
    {
        currentMusicObject = Instantiate(soundObject, transform);
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        currentMusicObject.GetComponent<AudioSource>().Play();
    }

    public void StopMusic(string musicName)
    {
        switch(musicName)
        {
            case "win":
                MusicObjectCreation2 (music_win);
                break;
            case "lose":
                MusicObjectCreation2 (music_lose);
                break;
            default:
                break;

        }
    }
    void MusicObjectCreation2(AudioClip clip)
    {
        if(currentMusicObject == true)
        {
            Destroy(currentMusicObject);
        }

        currentMusicObject = Instantiate(soundObject, transform);
        
        currentMusicObject.GetComponent<AudioSource>().clip = clip;

        currentMusicObject.GetComponent<AudioSource>().Play();
    }
}
