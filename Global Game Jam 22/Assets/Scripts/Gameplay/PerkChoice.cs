using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkChoice : MonoBehaviour
{
    Perks perk;
    // Start is called before the first frame update

    public void setPerk(int j)
    {
        perk = (Perks)j;

        string aux = "";

        switch (perk)
        {
            case Perks.AddLife:
                break;
            case Perks.AddMovement:
                break;
            case Perks.AddAtack:
                break;
            case Perks.AddDefense:
                break;
            case Perks.AddRange:
                break;
            case Perks.ReduceEnemyDefense:
                break;
        }
        gameObject.GetComponentInChildren<Text>().text = aux;
    }
    public void onClick()
    {
        LogicManager lm = GameObject.Find("LogicManager").GetComponent<LogicManager>();
        Character c = GameObject.Find("Player").GetComponent<Character>();

        int aux = -1;

        switch (perk)
        {
            //Advantage
            case Perks.AddLife:
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, -1);
                }
                break;
            case Perks.AddMovement:
                
                break;
            case Perks.AddAtack:
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.AddDefense:
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.AddRange:
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    if (aux < 2)
                    {
                        c.perks.Remove(perk);
                        aux++;
                        c.perks.Add(perk, aux);
                    }
                }
                else
                {
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.ReduceEnemyDefense: //Update enemies
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.ReduceEnemyAttack: //Update enemies
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, 1);
                }
                break;
            //Disadvantage
            case Perks.HalfLife:
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.Movement1:

                break;
            case Perks.LessDefense: //Counter
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, -1);
                }
                break;
            case Perks.LessRange: //Counter
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, -1);
                }
                break;
            case Perks.IncreaseEnemyDefense: //Counter
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, -1);
                }
                break;
            case Perks.IncreaseEnemyAttack: //Counter
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, -1);
                }
                break;
            case Perks.SwapAttackDefense:
                if (c.perks.ContainsKey(perk))
                {                   
                    c.perks.Remove(perk);
                }
                else
                {
                    c.perks.Add(perk, 1);
                }
                break;
        }

        lm.chosen = true;

        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
