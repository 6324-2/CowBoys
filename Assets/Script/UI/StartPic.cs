using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPic : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject mangaPanel;

    public void ClickStartButton()
    {
        //EventHandler.CallGameStart();

        //mangaPanel.SetActive(true);
        //EventHandler.CallMangaBeginEvent();

        //StartCoroutine(LoadScene());

        mangaPanel.SetActive(true);
        EventHandler.CallTextEvent();
        startPanel.SetActive(false);
    }

    //private IEnumerator LoadScene()
    //{
    //    //yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);

    //    mangaPanel.SetActive(true);
    //    EventHandler.CallTextEvent();
    //    startPanel.SetActive(false);
    //    //EventHandler.CallGameStart();
    //}
}
