using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider slider;
    AsyncOperation loadingOperation = null;
    CanvasGroup canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        DontDestroyOnLoad(gameObject);
    }

    public float minTime = 1.0f;
    public float auxTime = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (loadingOperation != null)
        {
           slider.value = Mathf.Clamp01(loadingOperation.progress * auxTime / 0.9f);
           auxTime += Time.deltaTime;

            if (loadingOperation.isDone && auxTime > minTime)
            {
                canvas.alpha = 0;
                loadingOperation = null;
                StartCoroutine(FadeLoadingScreen(1.0f));
            }
        }
    }

    public IEnumerator changeScene(string sceneName)
    {
        canvas.alpha = 1;
        auxTime = 0.0f;

        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeLoadingScreen(float duration)
    {
        float startValue = canvas.alpha;
        float time = 0;

        while (time < duration)
        {
            canvas.alpha = Mathf.Lerp(1, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = 0;
    }
}
