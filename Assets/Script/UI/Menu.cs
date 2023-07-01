using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject transitionalPanel;
    public GameObject gamePanel;
    public List<Transform> tools;
    public List<Transform> targets;

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
        StartCoroutine(SelectTools());
    }

    private IEnumerator SelectTools()
    {
        StartCoroutine(Move());

        yield return new WaitForSeconds(4.0f);
        EventHandler.CallMangaBeginEvent();

        transitionalPanel.SetActive(false);
        gamePanel.SetActive(true);
        GameController.Instance.GameStart();
    }

    private IEnumerator Move()
    {
        Shuffle(targets);

        while(true)
        {
            for(int i = 0; i < 6; i++)
            {
                MoveToWards(tools[i].position, targets[i].position, tools[i]);
            }
            yield return null; 
        }
    }

    public List<Transform> Shuffle(List<Transform> original)
    {
        System.Random randomNum = new System.Random();
        int index = 0;
        Transform temp;
        for (int i = 0; i < original.Count; i++)
        {
            index = randomNum.Next(0, original.Count - 1);
            if (index != i)
            {
                temp = original[i];
                original[i] = original[index];
                original[index] = temp;
            }
        }
        return original;
    }

    private void MoveToWards(Vector3 from, Vector3 to, Transform tool)
    {
        Vector3 velocity = (to - from);
        tool.position = from + velocity * Time.deltaTime;
    }
}
