using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSorce;
    //звуки главного геро€: прыжок, бег, кувырок, удар, исцеление, ранение;
    public AudioClip jumpSound, runSound, tucksSound, hitSound, hillSound, woundSound;
    //«вуки монстров: черт, демон, собака, призрак, потер€нный, бегун;
    public AudioClip heckSound, demonSound, hellDogSound, ghostSound, lostSound, sinnerSound;
    //«вуки окружени€: звук—бораƒуши, ма€тник, шипы, пилы, лава, падающа€ летка, падающа€ѕлатформа;
    public AudioClip soulSound, pendulumSound, spikeSound, sawSound, lavaSound, cellSound, platformSound;
    //«вуки игрока
    public void PlayJumpSound()
    {
        audioSorce.PlayOneShot(jumpSound);
    }
    public void PlayRunSound()
    {
        audioSorce.PlayOneShot(runSound);
    }
    public void PlayTucksSound()
    {
        audioSorce.PlayOneShot(tucksSound);
    }
    public void PlayHitSound()
    {
        audioSorce.PlayOneShot(hitSound);
    }
    public void PlayHillSound()
    {
        audioSorce.PlayOneShot(hillSound);
    }
    public void PlayWoundSound()
    {
        audioSorce.PlayOneShot(woundSound);
    }
    //«вуки монстров
    public void PlayHeckSound()
    {
        audioSorce.PlayOneShot(heckSound);
    }
    public void PlayDemonSound()
    {
        audioSorce.PlayOneShot(demonSound);
    }
    public void PlayHellDogSound()
    {
        audioSorce.PlayOneShot(hellDogSound);
    }
    public void PlayGhostSound()
    {
        audioSorce.PlayOneShot(ghostSound);
    }
    public void PlayLostSound()
    {
        audioSorce.PlayOneShot(lostSound);
    }
    public void PlaySinnerSound()
    {
        audioSorce.PlayOneShot(sinnerSound);
    }
    //«вуки окружени€
    public void PlaySoulSound()
    {
        audioSorce.PlayOneShot(soulSound);
    }
    public void PlayPendulumSound()
    {
        audioSorce.PlayOneShot(pendulumSound);
    }
    public void PlaySpikeSound()
    {
        audioSorce.PlayOneShot(spikeSound);
    }
    public void PlaySawSound()
    {
        audioSorce.PlayOneShot(sawSound);
    }
    public void PlayLavaSound()
    {
        audioSorce.PlayOneShot(lavaSound);
    }
    public void PlayCellSound()
    {
        audioSorce.PlayOneShot(cellSound);
    }
    public void PlayPlatformSound()
    {
        audioSorce.PlayOneShot(platformSound);
    }
}
  
