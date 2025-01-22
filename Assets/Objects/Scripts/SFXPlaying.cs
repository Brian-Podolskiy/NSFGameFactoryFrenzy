using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public AudioSource build;
    public AudioSource beep;
    public AudioSource upgrade;

    public void PlaySoundBuild()
    {
        build.Play();
    }
    public void PlaySoundBeep()
    {
        beep.Play();
    }
    public void PlaySoundUpgrade()
    {
        upgrade.Play();
    }
}
