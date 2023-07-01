using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    private int maxStep = 7;
    private List<(int, int)> actions = new List<(int, int)>();
    private bool isInput;
    private bool isShot;
    [SerializeField]private bool isWaiting;
    [SerializeField]private bool isActing;

    public int currentStep = 1;   
    public float waitingTime;
    public float actTime;
    public float reactTime;
    public float durationTime;
    public float timer;
    public float reactTimer;
    public float sinceShoot;
    public int[] scores = new int[4];

    private Player[] players = new Player[2];
    public TMP_Text timeString;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0;
    }

    private void Start()
    {
        players[0] = GameObject.FindGameObjectWithTag("player0").GetComponent<Player>();
        players[1] = GameObject.FindGameObjectWithTag("player1").GetComponent<Player>();
        players[0].inputDisable = true;
        players[1].inputDisable = true;
        isWaiting = true;
    }

    private void OnEnable()
    {
        EventHandler.playerInputEvent += OnPlayerInput;
    }

    private void OnDisable()
    {
        EventHandler.playerInputEvent -= OnPlayerInput;
    }

    private void Update()
    {
        timer = timer + Time.deltaTime;
        reactTimer = reactTimer + Time.deltaTime;
        sinceShoot = sinceShoot + Time.deltaTime;

        if (isShot && sinceShoot > reactTime)
        {
            for(int i = actions.Count - 1; i >= 0; i--)
            { 
                if(actions[i].Item2 == 1)
                {                    
                    players[actions[i].Item1].Win(scores[1]);
                    players[1 - actions[i].Item1].Dead(scores[3]);

                    break;
                }
            }
        }
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        StartCoroutine(Waiting(waitingTime));
    }

    private IEnumerator Waiting(float time)
    {
        EventHandler.CallStartWaitingTime();

        isInput = false;

        players[0].inputDisable = true;
        players[1].inputDisable = true;

        for (float i = time; i > 0.0f; i -= 0.1f)
        {
            timeString.text = i.ToString("0.0");
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Acting(actTime));
    }

    private IEnumerator Acting(float time)
    {
        EventHandler.CallStartActingTime();

        currentStep++;
        timer = 0;
        actions.Clear();

        players[0].inputDisable = false;
        players[1].inputDisable = false;

        for (float i = time; i > 0.0f; i -= 0.1f)
        {
            if(timer > durationTime)
            {
                i = 0;
            }
            if(isInput && reactTimer > reactTime)
            {
                i = 0;
            }
            timeString.text = i.ToString("0.0");
            yield return new WaitForSeconds(0.1f);
        }

        if (currentStep < maxStep)
            StartCoroutine(Waiting(waitingTime));
        else
        {
            StopCoroutine(Waiting(waitingTime));
            StopCoroutine(Acting(actTime));
            //TODO
        }
    }

    private void OnPlayerInput(int playerID, int action)
    {
        EventHandler.CallActEvent(action);

        Debug.Log(playerID + " " + action);
        actions.Add((playerID, action));
        reactTimer = 0;
        timer = 0;
        isInput = true;

        if (action == 1)
        {
            isShot = true;
            sinceShoot = 0;
            players[playerID].inputDisable = true;
        }
    } 

    public void Reload()
    {
        //StopCoroutine(Waiting(waitingTime));
        //StopCoroutine(Acting(actTime));
        currentStep = 1;
        timer = 0;
        reactTimer = 0;
        sinceShoot = 0;
        isShot = false;
        isInput = false;
        actions.Clear();
        SceneManager.UnloadSceneAsync("SampleScene");
    }
}
