using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsButtonBehaviour : MonoBehaviour
{
    private bool isVisible = false;
    private bool CR_running = false;

    public GameObject myMuteText;
    public GameObject myAudioSource;

    public void ChangeVisibility()
    {
        if (!isVisible && !CR_running)
        {
            StartCoroutine(FadeTo(1.0f, 0.5f));
            isVisible = true;
            gameObject.GetComponent<Button>().enabled = false;
            Debug.Log("Ahora es visible");
        }

        else
        {
            StartCoroutine(FadeTo(0.0f, 0.5f));
            gameObject.GetComponent<Button>().enabled = false;
            isVisible = false;
        }
    }

    public void goToMainScreen()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void muteGame()
    {
        if (myMuteText.GetComponent<Text>().text == "mute music")
        {
            myMuteText.GetComponent<Text>().text = "unmute music";
            myAudioSource.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            myMuteText.GetComponent<Text>().text = "mute music";
            myAudioSource.GetComponent<AudioSource>().volume = 1;
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        CR_running = true;

        float alpha = gameObject.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(alpha, aValue, t);
            Debug.Log("entramo de noche " + t);
            yield return null;
        }

        CR_running = false;
        gameObject.GetComponent<Button>().enabled = true;
    }
}
