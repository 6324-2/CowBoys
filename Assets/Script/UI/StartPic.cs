using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPic : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject mangaPanel;

    public void ClickStartButton()
    {
        //EventHandler.CallGameStart();
        Debug.Log("adsfsafasdf");
        startPanel.SetActive(false);
        mangaPanel.SetActive(true);
        EventHandler.CallMangaBeginEvent();
    }
}
