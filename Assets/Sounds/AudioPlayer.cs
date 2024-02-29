using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Bat;
    public AudioSource Gun;
    public AudioSource Pain;
    public AudioSource Punch;
    public AudioSource Roar;

    public void PlayBat()
    {
        Bat.Play();
    }

    public void PlayGun()
    {
        Gun.Play();
    }

    public void PlayPain()
    {
        Pain.Play();
    }

    public void PlayPunch()
    {
        Punch.Play();
    }

    public void PlayRoar()
    {
        Roar.Play();
    }
}
