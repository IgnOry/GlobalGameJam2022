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
            case Perks.AddLife:

                if (c.perks.TryGetValue(perk, out aux))
                {
                    if (aux < 5)
                    {
                        c.perks.Remove(perk);
                        c.perks.Add(perk, aux++);
                        c.maxHealth += 5;
                    }
                }
                else
                {
                    c.maxHealth += 5;
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.AddMovement:
                aux = -1;

                if (c.perks.TryGetValue(perk, out aux))
                {
                    if (c.movement < 3)
                    {
                        c.perks.Remove(perk);
                        c.perks.Add(perk, aux++);
                        c.movement++;
                    }
                    //Not else for movement == 2 because it already caps at 3
                }
                else
                {
                    c.movement ++;
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.AddAtack:
                if (c.perks.TryGetValue(perk, out aux))
                {
                        c.perks.Remove(perk);
                        c.perks.Add(perk, aux++);
                        c.weapon.attack++;
                    //Not else for movement == 2 because it already caps at 3
                }
                else
                {
                    c.weapon.attack++;
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.AddDefense:
                if (c.perks.TryGetValue(perk, out aux))
                {
                    c.perks.Remove(perk);
                    c.perks.Add(perk, aux++);
                    c.weapon.defense++;
                    //Not else for movement == 2 because it already caps at 3
                }
                else
                {
                    c.weapon.defense++;
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.AddRange:
                aux = -1;

                if (c.perks.TryGetValue(perk, out aux))
                {
                    if (c.weapon.range < 3)
                    {
                        c.perks.Remove(perk);
                        c.perks.Add(perk, aux++);
                        c.weapon.range++;
                    }
                    //Not else for movement == 2 because it already caps at 3
                }
                else
                {
                    c.weapon.range++;
                    c.perks.Add(perk, 1);
                }
                break;
            case Perks.ReduceEnemyDefense:
                lm.room.defenseModifier--;
                break;
        }

        lm.chosen = true;

    }
}
