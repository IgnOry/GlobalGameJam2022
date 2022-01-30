using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModButtonBehaviour : MonoBehaviour
{
    private bool isVisible = false;
    private bool CR_running = false;

    public GameObject mod1;
    public GameObject mod2;
    public void setModifiers()
    {
        Dictionary<Perks, int> aux = GameObject.Find("Player").GetComponent<Character>().perks;

        GameObject auxGO;

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(0).gameObject);
        }

        foreach (KeyValuePair<Perks, int> p in aux)
        {
            auxGO = Instantiate(mod1, transform.GetChild(0));

            string t = "";

            switch (p.Key)
            {
                case Perks.AddLife:
                    t = "Max HP increased by " + 5 * p.Value;
                    break;
                case Perks.AddMovement:
                    t = "Movement increased by " + p.Value;
                    break;
                case Perks.AddAtack:
                    t = "Attack increased by " + p.Value;
                    break;
                case Perks.AddDefense:
                    t = "Defense increased by " + p.Value;
                    break;
                case Perks.AddRange:
                    t = "Range increased by " + p.Value;
                    break;
                case Perks.ReduceEnemyDefense:
                    t = "Enemy defense reduced by " + p.Value;
                    break;
                case Perks.ReduceEnemyAttack:
                    t = "Enemy attack reduced by " + p.Value;
                    break;
                case Perks.HalfLife:
                    t = "Max HP halved";
                    break;
                case Perks.Movement1:
                    t = "Movement reduced";
                    break;
                case Perks.LessDefense:
                    t = "Defense reduced by " + p.Value;
                    break;
                case Perks.LessRange:
                    t = "Range reduced by " + p.Value;
                    break;
                case Perks.IncreaseEnemyDefense:
                    t = "Enemy defense increased by " + p.Value;
                    break;
                case Perks.IncreaseEnemyAttack:
                    t = "Enemy attack increased by " + p.Value;
                    break;
                case Perks.SwapAttackDefense:
                    t = "Attack and defense swaped";
                    break;
            }

            auxGO.GetComponentInChildren<Text>().text = t;
        }
    }

    public void ChangeVisibility()
    {
        if (!isVisible && !CR_running)
        {
            setModifiers();
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
