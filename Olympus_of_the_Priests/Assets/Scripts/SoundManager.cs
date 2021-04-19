using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSorce;
    //����� �������� �����: ������, ���, �������, ����, ���������, �������;
    public AudioClip jumpSound, runSound, tucksSound, hitSound, hillSound, woundSound;
    //����� ��������: ����, �����, ������, �������, ����������, �����;
    public AudioClip heckSound, demonSound, hellDogSound, ghostSound, lostSound, sinnerSound;
    //����� ���������: �������������, �������, ����, ����, ����, ��������������, �����������������;
    public AudioClip soulSound, pendulumSound, spikeSound, sawSound, lavaSound, cellSound, platformSound;
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
    //����� ���������
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
  
