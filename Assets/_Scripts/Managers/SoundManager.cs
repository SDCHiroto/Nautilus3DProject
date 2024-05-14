using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    // Mixer audio per regolare i volumi
    public AudioMixer mixer;

    // AudioSource per le clip audio del menu e del gioco principale
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

    // Imposta il volume della musica
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        // Se il volume è impostato su -50, viene impostato a -80 per il silenzio
        if (volume == -50)
        {
            mixer.SetFloat("MusicVolume", -80);
        }
    }

    // Imposta il volume degli effetti sonori
    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
        // Se il volume è impostato su -50, viene impostato a -80 per il silenzio
        if (volume == -50)
        {
            mixer.SetFloat("MusicVolume", -80);
        }
    }

    // Funzione per il crossfade tra le due tracce audio
    public void Crossfade()
    {
        StartCoroutine(CrossfadeCoroutine(clipMenu, clipMain, 2f));
    }

    // Coroutine per il crossfade
    IEnumerator CrossfadeCoroutine(AudioSource fadeOutSource, AudioSource fadeInSource, float fadeDuration)
    {
        // Imposta i volumi iniziali
        float startVolumeFadeOut = fadeOutSource.volume;
        float startVolumeFadeIn = fadeInSource.volume;

        // Avvia clipMain se non è già in riproduzione
        if (!fadeInSource.isPlaying)
            fadeInSource.Play();

        // Esegue il fade-out
        float timer = 0f;
        while (timer < fadeDuration)
        {
            fadeOutSource.volume = Mathf.Lerp(startVolumeFadeOut, 0f, timer / fadeDuration);
            fadeInSource.volume = Mathf.Lerp(startVolumeFadeIn, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Assicura che il volume sia esattamente 0 per il fade-out
        fadeOutSource.volume = 0f;

        // Imposta il volume al valore finale per il fade-in
        fadeInSource.volume = 1f;

        // Disattiva clipMenu
        fadeOutSource.enabled = true;
    }
}
