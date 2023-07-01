using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private float timer;

    public List<AudioClip> backEffets;
    public List<AudioClip> actEffets;
    public List<AudioClip> footEffets;
    public AudioSource backSound;
    public AudioSource footSound;
    public AudioSource propSound;
    public AudioSource gunSound;
    public AudioSource otherSound;
    public AudioSource musicSound;
    public AudioMixer audioMixer;

    public float waitTime;

    private void OnEnable()
    {
        EventHandler.startWaitingTimeEvent += OnWaitingTime;
        EventHandler.startActingTimeEvent += OnActingTime;
        EventHandler.actEvent += OnAct;
    }

    private void OnDisable()
    {
        EventHandler.startWaitingTimeEvent -= OnWaitingTime;
        EventHandler.startActingTimeEvent -= OnActingTime;
        EventHandler.actEvent -= OnAct;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void OnWaitingTime()
    {
        propSound.Stop();
        gunSound.Stop();

        audioMixer.SetFloat("BGVolume", ConvertSoundVolume(backSound.volume));
        backSound.clip = backEffets[0];
        backSound.Play();

        float length = footEffets[0].length;
        Debug.Log(GameController.Instance.waitingTime - length);
        StartCoroutine(WaitFoot(GameController.Instance.waitingTime - length));
    }

    private IEnumerator WaitFoot(float time)
    {
        yield return new WaitForSeconds(time);

        backSound.Stop();

        audioMixer.SetFloat("FootVolume", ConvertSoundVolume(footSound.volume));
        footSound.clip = footEffets[0];
        footSound.Play();
    }

    public void OnActingTime()
    {
        footSound.Stop();
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void OnAct(int action)
    {
        if((propSound.isPlaying || gunSound.isPlaying) && timer < waitTime)
        {
            StartCoroutine(Wait(waitTime - timer));
        }

        audioMixer.SetFloat("ToolVolume", ConvertSoundVolume(propSound.volume));
        audioMixer.SetFloat("GunVolume", ConvertSoundVolume(propSound.volume));

        if (action == 1)
        {
            gunSound.clip = actEffets[0];
            gunSound.Play();
        }
        else if(action == 2)
        {
            propSound.clip = actEffets[1];
            propSound.Play();
        }
        timer = 0;
    }

    private float ConvertSoundVolume(float amount)
    {
        return (amount * 100 - 80);
    }
}
