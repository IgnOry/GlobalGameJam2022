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
            string t = "";

            switch (p.Key)
            {
                case Perks.AddLife:
                    t = "Max HP increased by " + 5 * p.Value;
                    auxGO = Instantiate(mod1, transform.GetChild(0));
                    auxGO.GetComponentInChildren<Text>().text = t;
                    break;
                case Perks.AddMovement:
                    t = "Movement increased by " + p.Value;
                    auxGO = Instantiate(mod1, transform.GetChild(0));
                    auxGO.GetComponentInChildren<Text>().text = t;
                    break;
                case Perks.AddAtack:
                    t = "Attack increased by " + p.Value;
                    auxGO = Instantiate(mod1, transform.GetChild(0));
                    auxGO.GetComponentInChildren<Text>().text = t;
                    break;
                case Perks.AddDefense:
                    if (p.Value > 0)
                    {
                        t = "Defense increased by " + p.Value;
                        auxGO = Instantiate(mod1, transform.GetChild(0));
                        auxGO.GetComponentInChildren<Text>().text = t;
                    }
                    else if (p.Value < 0)
                    {
                        t = "Defense reduced by " + (-p.Value);
                        auxGO = Instantiate(mod2, transform.GetChild(0));
                        auxGO.GetComponentInChildren<Text>().text = t;
                    }
                    break;
                case Perks.AddRange:
                    t = "Range increased by " + p.Value;
                    auxGO = Instantiate(mod1, transform.GetChild(0));
                    auxGO.GetComponentInChildren<Text>().text = t;
                    break;
                case Perks.ReduceEnemyDefense:
                    if (p.Value > 0)
                    { 
                        t = "Enemy defense reduced by " + p.Value;
                        auxGO = Instantiate(mod1, transform.GetChild(0));
                        auxGO.GetComponentInChildren<Text>().text = t;
                    }
                    else if (p.Value < 0)
                    {
                        t = "Enemy defense increased by " + (-p.Value);
                        auxGO = Instantiate(mod2, transform.GetChild(0));
                        auxGO.GetComponentInChildren<Text>().text = t;
                    }
                    break;
                case Perks.ReduceEnemyAttack:
                    if (p.Value > 0)
                    {
                        t = "Enemy attack reduced by " + p.Value;
                        auxGO = Instantiate(mod1, transform.GetChild(0));
                        auxGO.GetComponentInChildren<Text>().text = t;
                    }
                    else if (p.Value < 0)
                    {
                        t = "Enemy attack increased by " + (-p.Value);
                        auxGO = Instantiate(mod2, transform.GetChild(0));
                        auxGO.GetComponentInChildren<Text>().text = t;
                    }
                    break;
                case Perks.HalfLife:
                    t = "Max HP halved";
                    auxGO = Instantiate(mod1, transform.GetChild(0));
                    auxGO.GetComponentInChildren<Text>().text = t;
                    break;
                case Perks.LessRange:
                    t = "Range reduced by " + p.Value;
                    auxGO = Instantiate(mod1, transform.GetChild(0));
                    auxGO.GetComponentInChildren<Text>().text = t;
                    break;
                
                case Perks.SwapAttackDefense:
                    t = "Attack and defense swaped";
                    auxGO = Instantiate(mod1, transform.GetChild(0));
                    auxGO.GetComponentInChildren<Text>().text = t;
                    break;
            }
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
