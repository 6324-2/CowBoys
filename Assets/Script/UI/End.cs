using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        //SceneManager.UnloadSceneAsync("PersistentScene");
        SceneManager.LoadScene("PersistentScene", LoadSceneMode.Additive);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
