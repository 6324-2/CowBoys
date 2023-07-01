using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private float timer;

    public List<AudioClip> backEffets;
    public List<AudioClip> actEffets;
    public List<AudioClip> footEffets;
    public List<AudioClip> Music;
    public AudioSource backSound;
    public AudioSource footSound;
    public AudioSource propSound;
    public AudioSource gunSound;
    public AudioSource otherSound;
    public AudioSource musicSound;
    public AudioMixer audioMixer;

    public float musicOffset;
    public float footOffset;

    public float waitTime;

    private void Start()
    {
        audioMixer.SetFloat("BGMVolume", ConvertSoundVolume(musicSound.volume));
        musicSound.clip = Music[1];
        musicSound.Play();
    }

    private void OnEnable()
    {
        EventHandler.startWaitingTimeEvent += OnWaitingTime;
        EventHandler.startActingTimeEvent += OnActingTime;
        EventHandler.actEvent += OnAct;
        EventHandler.mangaBeginEvent += OnManga;
        EventHandler.gameStartEvent += OnGameStart;
        EventHandler.gameEndEvent += OnGameEnd;
    }

    private void OnDisable()
    {
        EventHandler.startWaitingTimeEvent -= OnWaitingTime;
        EventHandler.startActingTimeEvent -= OnActingTime;
        EventHandler.actEvent -= OnAct;
        EventHandler.mangaBeginEvent -= OnManga;
        EventHandler.gameStartEvent -= OnGameStart;
        EventHandler.gameEndEvent -= OnGameEnd;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnGameEnd()
    {
        audioMixer.SetFloat("BGMVolume", ConvertSoundVolume(musicSound.volume));
        musicSound.clip = Music[0];
        musicSound.Play();
    }

    private void OnManga()
    {
        //audioMixer.SetFloat("BGMVolume", ConvertSoundVolume(musicSound.volume + musicOffset));
        StartCoroutine(SoundFade());
    }

    private IEnumerator SoundFade()
    {
        for(float i = 10; i > 0; i--)
        {
            audioMixer.SetFloat("BGMVolume", ConvertSoundVolume(i/10));
            yield return new WaitForSeconds(0.1f);
        }
        musicSound.Stop();
    }

    private void OnGameStart()
    {
        //musicSound.Stop();
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

        audioMixer.SetFloat("FootVolume", ConvertSoundVolume(footSound.volume + footOffset));
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

    public void OnAct(int action, int playerID)
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
            List<int> temps = GameController.Instance.toolDic[playerID];
            propSound.clip = actEffets[temps[0] + 1];
            temps.Remove(0);
            propSound.Play();
        }
        timer = 0;
    }

    private float ConvertSoundVolume(float amount)
    {
        return (amount * 100 - 80);
    }
}
