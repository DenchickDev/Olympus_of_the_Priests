using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSorce;
    //����� �������� �����: ������, ���, �������, ����, ���������, �������;
    public AudioClip jumpSound, runSound, tucksSound, hitSound, hillSound, woundSound;
    //����� ��������: ����, �����, ������, �������, ����������, �����;
    //public AudioClip heckSound, demonSound, hellDogSound, ghostSound, lostSound, sinnerSound;
    //����� ���������: �������������, �������, ����, ����, ����, ��������������, �����������������;
    public AudioClip takeItemsSound,  platformSound, cellSound; // sawSound, spikeSound, lavaSound,  pendulumSound;
    //����� ������
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
    //����� ��������
  /*  public void PlayHeckSound()
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
    }*/
    //����� ���������
    public void PlayTakeItemsSound()
    {
        audioSorce.PlayOneShot(takeItemsSound);
    }
    
    
     public void PlayCellSound()
    {
        audioSorce.PlayOneShot(cellSound);
    }
    public void PlayPlatformSound()
    {
        audioSorce.PlayOneShot(platformSound);
    }/*
    public void PlayPendulumSound()
    {
        audioSorce.PlayOneShot(pendulumSound);
    }
    public void PlaySawSound()
    {
        audioSorce.PlayOneShot(sawSound);
    }
    public void PlaySpikeSound()
    {
        audioSorce.PlayOneShot(spikeSound);
    }
    public void PlayLavaSound()
    {
        audioSorce.PlayOneShot(lavaSound);
    }*/
}
  
