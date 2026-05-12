using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Slider volumeSlider;
    public Audio[] audios;
    public static AudioManager instance;

    float volumeValue;
    float lastVolumeValue;

    private void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/

        foreach (Audio a in audios)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.audioFile;
            a.source.volume = PlayerPrefs.GetFloat("Volume");
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }
    }

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        volumeValue = volumeSlider.value;
        Play("Musica");
    }

    void Update()
    {

        if (volumeValue != lastVolumeValue)
        {
            CambiarVolumen();
        }
        lastVolumeValue = volumeValue;

        volumeValue = volumeSlider.value;
    }

    void CambiarVolumen()
    {
        PlayerPrefs.SetFloat("Volume", volumeValue);
        PlayerPrefs.Save();

        AudioListener.volume = volumeValue;
    }

    public void Play(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);

        if (a == null)
        {
            Debug.LogWarning("El nombre del archivo " + name + " no existe");
            return;
        }

        a.source.Play();
    }
}
