using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manga : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.textEvent += OnText;
    }

    private void OnDisable()
    {
        EventHandler.textEvent -= OnText;
    }

    private void OnText()
    {
        StartCoroutine(WaitToNextScene());
    }

    private IEnumerator WaitToNextScene()
    {
        yield return new WaitForSeconds(Eternal.Instance.time);
        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        EventHandler.CallGameStart();
        this.gameObject.SetActive(false);
    }
}
