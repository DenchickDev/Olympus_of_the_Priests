using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public GameObject[] BGMusic;  //������ � ������� �������
    
    public void Start()
    {
       /* sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == SceneManager.LoadScene.name)*/

        DontDestroyOnLoad(BGMusic[0]);
        BGMusic = GameObject.FindGameObjectsWithTag("Music");

        if (BGMusic.Length > 1)
        {
            Destroy(BGMusic[1]);
        }
    }
    public void DestroyBGMusic()
    {
        if (SceneManager.GetActiveScene().name != ("Disclamer") & SceneManager.GetActiveScene().name != ("MainMenu"))
        {
            Destroy(BGMusic[0]);
        }
    }

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
  
