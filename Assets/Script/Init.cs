using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject endPanel;

    public int playerCount = 0;

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(playerPrefab, this.transform);
        }
    }

    private void OnEnable()
    {
        EventHandler.gameEndEvent += OnGameEndEvent;
    }

    private void OnDisable()
    {
        EventHandler.gameEndEvent -= OnGameEndEvent;
    }

    public void PlayerJoined()
    {
        playerCount++;
        //if (playerCount == 2)
        //    EventHandler.CallPlayerJoined();
    }

    private void OnGameEndEvent()
    {
        endPanel.SetActive(true);
    }
}
