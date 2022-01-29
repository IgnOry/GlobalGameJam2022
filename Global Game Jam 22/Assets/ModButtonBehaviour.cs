using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModButtonBehaviour : MonoBehaviour
{
    private bool isVisible = false;
    private bool CR_running = false;

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

        /*
        Transform myListTransform = gameObject.transform.GetChild(0).gameObject.transform;

        if (myListTransform.childCount > 0 && !isVisible)
        {
            //gameObject.transform.GetChild(0).gameObject.SetActive(false);

            for (int i = 0; i < myListTransform.childCount; i++)
            {
                float myAlphaValue = 0;

                while (myAlphaValue < 255)
                {
                    myListTransform.GetChild(i).gameObject.GetComponent<Image>().color =
                        new Color(89, 89, 89,
                                  myAlphaValue);
                    myAlphaValue += 0.1f * Time.deltaTime;
                }

                Debug.Log(myListTransform.GetChild(i).gameObject.GetComponent<Image>().color.a);

                myAlphaValue = 0;

                while(myAlphaValue < 255)
                {
                    myListTransform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Text>().color =
                            new Color(89, 89, 89,
                                  myAlphaValue);
                    myAlphaValue += 0.1f * Time.deltaTime;
                }

            }

            isVisible = true;

        }

        else
        {
            for (int i = 0; i < myListTransform.childCount; i++)
            {
                float myAlphaValue = 0;

                myListTransform.GetChild(i).gameObject.GetComponent<Image>().color =
                    new Color(89, 89, 89,
                              myAlphaValue);

                myListTransform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Text>().color =
                        new Color(89, 89, 89,
                              myAlphaValue);

            }

            isVisible = false;

        }

        //gameObject.transform.GetChild(0).gameObject.SetActive(false);
        */
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
