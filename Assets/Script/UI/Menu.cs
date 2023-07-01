using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject transitionalPanel;
    public GameObject gamePanel;
    public List<Transform> tools;
    public List<Transform> targets;

    public List<Transform> mangas;
    public List<Transform> mangasTargets;
    public List<Transform> mangasSources;

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
        

        yield return StartCoroutine(Move());
        EventHandler.CallMangaBeginEvent();

        
        yield return StartCoroutine(MangaMove());

        GameController.Instance.GameStart();
    }

    private IEnumerator MangaMove()
    {
        float timer = 0;
        while (timer < 2.5f)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < 2; i++)
            {
                MoveToWards(mangas[i].position, mangasTargets[i].position, mangas[i]);
            }
            yield return null;
        }

        transitionalPanel.SetActive(false);
        gamePanel.SetActive(true);

        timer = 0;
        while (timer < 2.5f)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < 2; i++)
            {
                MoveToWards(mangas[i].position, mangasSources[i].position, mangas[i]);
            }
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        Shuffle(targets);

        float timer = 0;
        while (timer < 4.0f)
        {
            timer += Time.deltaTime;
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
        Vector3 velocity = (to - from) + (to - from).normalized * 200;
        tool.position = from + velocity * Time.deltaTime;
    }
}
