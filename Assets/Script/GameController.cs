using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameController : Singleton<GameController>
{
    private int maxStep = 7;
    private List<(int, int)> actions = new List<(int, int)>();
    private bool isInput;
    private bool isShot;
    private bool isGaming;
    //[SerializeField]private bool isWaiting;
    [SerializeField]private bool isActing;

    public int currentStep = 0;   
    public float waitingTime;
    public float actTime;
    public float reactTime;
    public float durationTime;
    public float effectExitTime;
    public float timer;
    public float reactTimer;
    public float sinceShoot;
    public int[] scores = new int[4];
    public Dictionary<int, List<int>> toolDic = new Dictionary<int, List<int>> { {0, new List<int>() }, { 1, new List<int>() } };

    private Player[] players = new Player[2];
    public TMP_Text timeString;
    public GameObject focusPanel;
    public Image progressBar;
    public Image timingBar;
    public Image Man0;
    public Image Man1;
    public Image tumbleweed;
    public Sprite onShoot;
    public Sprite idle;
    public List<Sprite> effectSources;
    public List<Image> effects;
    public List<Transform> man0Pos;
    public List<Transform> man1Pos;

    protected override void Awake()
    {
        base.Awake();
        //Time.timeScale = 0;
    }

    private void Start()
    {
        players[0] = GameObject.FindGameObjectWithTag("player0").GetComponent<Player>();
        players[1] = GameObject.FindGameObjectWithTag("player1").GetComponent<Player>();
        players[0].inputDisable = true;
        players[1].inputDisable = true;
        //isWaiting = true;

        StartCoroutine(WaitUntilLoad());
    }

    private IEnumerator WaitUntilLoad()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.SetActiveScene(this.gameObject.scene);
    }

    private void OnPlayerJoined()
    {
        players[0] = GameObject.FindGameObjectWithTag("player0").GetComponent<Player>();
        players[1] = GameObject.FindGameObjectWithTag("player1").GetComponent<Player>();
        players[0].inputDisable = true;
        players[1].inputDisable = true;
    }

    private void OnEnable()
    {
        EventHandler.playerInputEvent += OnPlayerInput;
        EventHandler.playerJoinedEvent += OnPlayerJoined;
        EventHandler.manMoveEvent += OnManMove;
    }

    private void OnDisable()
    {
        EventHandler.playerInputEvent -= OnPlayerInput;
        EventHandler.playerJoinedEvent -= OnPlayerJoined;
        EventHandler.manMoveEvent -= OnManMove;
    }

    private void Update()
    {
        if (isGaming)
        {
            timer = timer + Time.deltaTime;
            reactTimer = reactTimer + Time.deltaTime;
            sinceShoot = sinceShoot + Time.deltaTime;

            if (isShot && sinceShoot > reactTime && currentStep < maxStep)
            {
                for (int i = actions.Count - 1; i >= 0; i--)
                {
                    if (actions[i].Item2 == 1)
                    {
                        players[actions[i].Item1].Win(scores[1]);
                        players[1 - actions[i].Item1].Dead(scores[3]);
                        isGaming = false;
                        break;
                    }
                }
            }
            else if(isShot && sinceShoot > reactTime && currentStep >= maxStep)
            {
                players[actions[0].Item1].Win(scores[0]);
                players[1 - actions[0].Item1].Dead(scores[2]);
                isGaming = false;
            }
        }
    }

    public void GameStart()
    {
        isGaming = true;
        //Time.timeScale = 1;
        progressBar.fillAmount = 0;
        StartCoroutine(Waiting(waitingTime));
    }

    private IEnumerator Waiting(float time)
    {
        EventHandler.CallStartWaitingTime();

        isInput = false;
        timingBar.fillAmount = 1.0f;
        timingBar.color = Color.red;
        Vector3 tempPos = tumbleweed.rectTransform.localPosition;
        tumbleweed.rectTransform.localPosition = new Vector3(-1280, tempPos.y, tempPos.z);

        players[0].inputDisable = true;
        players[1].inputDisable = true;

        for (float i = time; i > 0.0f; i -= 0.1f)
        {
            timeString.text = i.ToString("0.0");
            timingBar.fillAmount = i / time;
            tumbleweed.rectTransform.localPosition = new Vector3(-1280 + 2760 * (time - i) / time, tempPos.y, tempPos.z);
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Acting(actTime));
    }

    private IEnumerator Acting(float time)
    {
        EventHandler.CallStartActingTime();

        currentStep++;
        actTime++;
        durationTime++;
        progressBar.fillAmount = (float)currentStep / 7;
        timer = 0;
        actions.Clear();
        timingBar.fillAmount = 1.0f;
        timingBar.color = Color.green;

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
            timingBar.fillAmount = i / time;
            timeString.text = i.ToString("0.0");
            yield return new WaitForSeconds(0.1f);
        }

        if (currentStep < maxStep - 1)
            StartCoroutine(Waiting(waitingTime));
        else if(currentStep == maxStep - 1)
        {
            StopCoroutine(Waiting(waitingTime));
            StopCoroutine(Acting(actTime));

            StartCoroutine(Focus());
            actTime = 99999;
            durationTime = actTime;
        }
    }

    private IEnumerator Focus()
    {
        focusPanel.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        StartCoroutine(Waiting(waitingTime));
    }

    private void OnManMove(float time)
    {
        StartCoroutine(ManMove(time));
    }

    private IEnumerator ManMove(float time)
    {
        float timer = 0;
        while(timer < time)
        {
            timer += Time.deltaTime;
            Man0.transform.localPosition += (man0Pos[currentStep].localPosition - Man0.transform.localPosition) / time * Time.deltaTime;
            Man1.transform.localPosition += (man1Pos[currentStep].localPosition - Man1.transform.localPosition) / time * Time.deltaTime;
            Man0.transform.localScale += (man0Pos[currentStep].localScale - Man0.transform.localScale) / time * Time.deltaTime;
            Man1.transform.localScale += (man1Pos[currentStep].localScale - Man1.transform.localScale) / time * Time.deltaTime;
            yield return null;
        }
    }

    private void OnPlayerInput(int playerID, int action)
    {
        EventHandler.CallActEvent(action, playerID);

        effects[playerID].gameObject.SetActive(true);
        effects[playerID].sprite = effectSources[Random.Range(0, 5)];
        StartCoroutine(WaitToFade(playerID));

        Debug.Log(playerID + " " + action);
        actions.Add((playerID, action));
        reactTimer = 0;
        timer = 0;
        isInput = true;

        if (action == 1)
        {
            if(playerID == 0)
            {
                Man0.sprite = onShoot;
            }
            isShot = true;
            sinceShoot = 0;
            players[playerID].inputDisable = true;
        }
    } 

    private IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(0.5f);
        Man0.sprite = idle;
    }

    private IEnumerator WaitToFade(int playerID)
    {
        yield return new WaitForSeconds(effectExitTime);

        effects[playerID].gameObject.SetActive(false);
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
