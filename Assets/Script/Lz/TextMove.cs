using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextMove : MonoBehaviour
{
    public float scrollSpeed = 10f;
    public string nextSceneName;
    // Start is called before the first frame update
    private RectTransform textRectTransform;
    private Canvas canvas;
    
    private void Start()
    {
        textRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }
    
    private void Update()
    {
        // 向上滚动文本
        textRectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        
        // 如果文本滚动到顶部，切换到下一个场景
        if (textRectTransform.anchoredPosition.y >= canvas.pixelRect.height)
        {
            LoadNextScene();
        }
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
