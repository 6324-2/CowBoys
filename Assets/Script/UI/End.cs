using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public GameObject endPanel;

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
        SceneManager.LoadScene("PersistentScene");    
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnGameEndEvent()
    {
        endPanel.SetActive(true);
    }
}
