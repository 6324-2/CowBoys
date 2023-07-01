using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;

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
