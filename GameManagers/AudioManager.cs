using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultAmbience, bossFight, combat;
    public static AudioManager instance;
    private AudioSource ambienceMusic, combatMusic;
    private bool isPlayingAmbienceMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        ambienceMusic = gameObject.AddComponent<AudioSource>();
        combatMusic= gameObject.AddComponent<AudioSource>();
        SwapTrack(defaultAmbience);
    }


    //used to toggle general combat
    private void SwapTrack(AudioClip newClip)
    {
        if (isPlayingAmbienceMusic)
        {
            combatMusic.clip = newClip;
            combatMusic.Play();
            ambienceMusic.Stop();

        }
        else
        {
            ambienceMusic.clip = newClip;
            ambienceMusic.Play();
            combatMusic.Stop();
        }
        isPlayingAmbienceMusic = !isPlayingAmbienceMusic;
    }




}
