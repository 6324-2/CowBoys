using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class End : MonoBehaviour
{
    public GameObject endPanel;
    private Player[] players = new Player[2];
    public TMP_Text player0Score;
    public TMP_Text player1Score;

    private void OnEnable()
    {
        EventHandler.gameEndEvent += OnGameEndEvent;
    }

    private void OnDisable()
    {
        EventHandler.gameEndEvent -= OnGameEndEvent;
    }

    public void Restart()
    {
        //SceneManager.UnloadSceneAsync("PersistentScene");
        //SceneManager.LoadScene("SampleScene");
        Eternal.Instance.time = 0.1f;
        SceneManager.LoadScene("PersistentScene");    
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnGameEndEvent()
    {
        endPanel.SetActive(true);
        players[0] = GameObject.FindGameObjectWithTag("player0").GetComponent<Player>();
        players[1] = GameObject.FindGameObjectWithTag("player1").GetComponent<Player>();

        player0Score.text = players[0].score.ToString();
        player1Score.text = players[1].score.ToString();
    }
}
