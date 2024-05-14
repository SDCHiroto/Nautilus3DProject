using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioMixer mixer;

    public AudioSource clipMenu;
    public AudioSource clipMain;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        if (volume == -50)
        {
            mixer.SetFloat("MusicVolume", -80);
        }
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
        if (volume == -50)
        {
            mixer.SetFloat("MusicVolume", -80);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.H)){
            Debug.Log("Aha");
            Crossfade();
        }
    }
    
    // Funzione per la transizione fade-out/fade-in tra le due AudioSource
    public void Crossfade()
    {
        StartCoroutine(CrossfadeCoroutine(clipMenu, clipMain, 2f));
    }

    IEnumerator CrossfadeCoroutine(AudioSource fadeOutSource, AudioSource fadeInSource, float fadeDuration)
    {
        // Imposta i volumi iniziali
        float startVolumeFadeOut = fadeOutSource.volume;
        float startVolumeFadeIn = fadeInSource.volume;

        // Avvia clipMain se non è già in riproduzione
        if (!fadeInSource.isPlaying)
            fadeInSource.Play();

        // Fai partire il fade-out
        float timer = 0f;
        while (timer < fadeDuration)
        {
            fadeOutSource.volume = Mathf.Lerp(startVolumeFadeOut, 0f, timer / fadeDuration);
            fadeInSource.volume = Mathf.Lerp(startVolumeFadeIn, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Assicurati che il volume sia esattamente 0 per il fade-out
        fadeOutSource.volume = 0f;

        // Imposta il volume al valore finale per il fade-in
        fadeInSource.volume = 1f;

        // Disattiva clipMenu
        fadeOutSource.enabled = true;
    }

}
