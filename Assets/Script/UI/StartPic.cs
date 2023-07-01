using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPic : MonoBehaviour
{
    public GameObject startPanel;
    //public GameObject mangaPanel;

    public void ClickStartButton()
    {
        //EventHandler.CallGameStart();
        startPanel.SetActive(false);
        //mangaPanel.SetActive(true);
        //EventHandler.CallMangaBeginEvent();

        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        EventHandler.CallGameStart();
    }
}
