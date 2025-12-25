using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public AudioSource music;
    //public AudioSource scaryMusic;
    public AudioSource current;

    public AudioSource darkend;
    //public float soundLevel = 1;
    
    void Start()
    {
        StartCoroutine(StartFade(current, 1.5f, true));
    }

    public void setMusic(AudioSource music)
    {
        StartCoroutine(switchAudio(current, music));
        current = music;
    }

    public void setCurrent(AudioSource music)
    {
        StartCoroutine(StartFade(current, 1.5f, false));
        current = music;
    }

    public IEnumerator switchAudio(AudioSource a, AudioSource b)

    {
        StartCoroutine(StartFade(a, 1.5f, false));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(StartFade(b, 1.5f, true));
    }
    public void toSilence()
    {
        StartCoroutine(switchAudio(current, darkend));
    }

    public void leaveSilence()
    {
        StartCoroutine(switchAudio(darkend, current));
    }

    public void fade(AudioSource a, bool fadeIn)
    {
        StartCoroutine(StartFade(a, 1.5f, fadeIn));
    }

    public void fade( bool fadeIn)
    {
        StartCoroutine(StartFade(current, 1.5f, fadeIn));
    }
    public static IEnumerator StartFade(AudioSource audioSource, float duration,bool fadeIn)
    {
        float targetVolume = 0;
        if (fadeIn)
        {
            
            audioSource.volume = 0;
            audioSource.Play(0);
            targetVolume = FindObjectOfType<gameManager>().getAudioLevel();
        }


        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        if(!fadeIn)
        {
            audioSource.Stop();
        }
        yield break;
    }

    public void changeMaxVolume(float i)
    {
        current.volume = i;
    }
}
