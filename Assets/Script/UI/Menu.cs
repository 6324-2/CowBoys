using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject transitionalPanel;
    public GameObject gamePanel;

    private void OnEnable()
    {
        EventHandler.gameStartEvent += OnGameStart;
    }

    private void OnDisable()
    {
        EventHandler.gameStartEvent -= OnGameStart;
    }

    private void OnGameStart()
    {
        transitionalPanel.SetActive(true);
        Debug.Log("11111");
    }

    public void StartGame()
    {
        transitionalPanel.SetActive(false);
        StartCoroutine(Loading());
        gamePanel.SetActive(true);
        GameController.Instance.GameStart();
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
