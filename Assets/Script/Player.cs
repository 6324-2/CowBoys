using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int gameCount = 3;
    public int playrID;
    public int action;
    public int propCount;
    public int score;
    public bool inputDisable;

    public TMP_Text scoreString;

    private void Start()
    {
        scoreString.text = score.ToString();
    }

    private void Update()
    {
        if(!inputDisable && playrID == 0)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                action = 1;
                EventHandler.CallPlayerInputEvent(playrID, action);
            }
            else if (Input.GetKeyDown(KeyCode.K) && propCount > 0)
            {
                action = 2;
                EventHandler.CallPlayerInputEvent(playrID, action);
                propCount--;
            }
        }
        else if(!inputDisable && playrID == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                action = 1;
                EventHandler.CallPlayerInputEvent(playrID, action);
            }
            else if (Input.GetMouseButtonDown(1) && propCount > 0)
            {
                action = 2;
                EventHandler.CallPlayerInputEvent(playrID, action);
                propCount--;
            }
        }
    }

    public void Win(int score)
    {
        this.score += score;
        gameCount--;
        scoreString.text = this.score.ToString();
    }

    public void Dead(int score)
    {
        Debug.Log("Player" + playrID + " is dead");
        this.score += score;
        gameCount--;
        scoreString.text = this.score.ToString();
        if (gameCount > 0)
        {
            GameController.Instance.Reload();
            SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("SampleScene");
        }
    }
}
