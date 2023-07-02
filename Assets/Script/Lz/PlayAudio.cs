using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip cansAudio;
    public AudioClip knifeAudio;
    public AudioClip lighterAudio;
    public AudioClip bagAudio;
    public AudioClip keyAudio;
    public AudioClip chainAudio;
    public AudioClip gunAudio;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void GetCansAudio()
    {
        audioSource.clip = cansAudio;
        audioSource.Play();
    }
    public void GetKnifeAudio()
    {
        audioSource.clip = knifeAudio;
        audioSource.Play();
    }
    public void GetLighterAudio()
    {
        audioSource.clip = lighterAudio;
        audioSource.Play();
    }
    public void GetBagAudio()
    {
        audioSource.clip = bagAudio;
        audioSource.Play();
    }
    public void GetKeyAudio()
    {
        audioSource.clip = keyAudio;
        audioSource.Play();
    }
    public void GetChainAudio()
    {
        audioSource.clip = chainAudio;
        audioSource.Play();
    }
    public void GetGunAudio()
    {
        audioSource.clip = gunAudio;
        audioSource.Play();
    }
    
    
    
}
