using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;

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
        menuPanel.SetActive(true);
        Debug.Log("11111");
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        StartCoroutine(Loading());
        GameController.Instance.GameStart();
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
