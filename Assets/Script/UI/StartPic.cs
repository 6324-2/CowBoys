using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPic : MonoBehaviour
{
    public GameObject startPanel;

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            EventHandler.CallGameStart();
            startPanel.SetActive(false);
        }
    }
}
