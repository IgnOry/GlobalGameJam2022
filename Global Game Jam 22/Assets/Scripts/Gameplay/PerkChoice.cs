using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkChoice : MonoBehaviour
{
    Perks perk;
    public GameObject myCoin;
    // Start is called before the first frame update

    public void setPerk(int j)
    {
        perk = (Perks)j;

        string aux = "";

        switch (perk)
        {
            case Perks.AddLife:
                aux = "Increases Max HP by 5. Stackable";
                break;
            case Perks.AddMovement:
                aux = "Increases Max movement by 1. Not Stackable";
                break;
            case Perks.AddAtack:
                aux = "Increases Attack by 1. Stackable";
                break;
            case Perks.AddDefense:
                aux = "Increases Defense by 1. Stackable";
                break;
            case Perks.AddRange:
                aux = "Increases Range by 1. Not Stackable";
                break;
            case Perks.ReduceEnemyDefense:
                aux = "Reduces Enemy Defense by 1. Stackable";
                break;
            case Perks.ReduceEnemyAttack:
                aux = "Reduces Enemy Attack by 1. Stackable";
                break;
            case Perks.HalfLife:
                aux = "Halves Max HP. Stackable";
                break;
            case Perks.Movement1:
                aux = "Reduces Max Movement. Not Stackable";
                break;
            case Perks.LessDefense:
                aux = "Reduces Defense by 1. Stackable";
                break;
            case Perks.LessRange:
                aux = "Reduces Range by 1. Not Stackable";
                break;
            case Perks.IncreaseEnemyDefense:
                aux = "Increases Enemy Defense by 1. Stackable";
                break;
            case Perks.IncreaseEnemyAttack:
                aux = "Increases Enemy Attack by 1. Stackable";
                break;
            case Perks.SwapAttackDefense:
                aux = "Swaps your Attack and Defense";
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
                if (c.perks.ContainsKey(perk)) //Done
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
                c.maxHealth += 5;
                break;
            case Perks.AddMovement: //Done
                if (c.perks.ContainsKey(perk))
                {
                    //Not stackable
                }
                else
                {
                    c.perks.Add(perk, 1);
                    c.movement++;
                }
                break;
            case Perks.AddAtack: //Done
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                    c.weapon.attack++;
                }
                else
                {
                    c.perks.Add(perk, 1);
                    c.weapon.attack++;
                }
                break;
            case Perks.AddDefense: //Done
                if (c.perks.ContainsKey(perk))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux++;
                    c.perks.Add(perk, aux);
                    c.weapon.defense++;
                }
                else
                {
                    c.perks.Add(perk, 1);
                    c.weapon.defense++;
                }
                break;
            case Perks.AddRange: //Done
                if (c.perks.ContainsKey(perk))
                {
                    //Not stackable
                }
                else
                {
                    c.perks.Add(perk, 1);
                    c.weapon.range++;
                }
                break;
            case Perks.ReduceEnemyDefense: //Done
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
                lm.room.defenseModifier--;
                lm.room.updateEnemyStats();
                break;
            case Perks.ReduceEnemyAttack: //Done
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
                lm.room.attackModifier--;
                lm.room.updateEnemyStats();
                break;
            //Disadvantage
            case Perks.HalfLife: //Done
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
                c.maxHealth = c.maxHealth / 2;
                break;
            case Perks.Movement1: //Done
                if (c.perks.ContainsKey(Perks.AddMovement))
                {
                    c.perks.Remove(perk);
                    c.movement--;
                }
                else
                {
                    
                }
                break;
            case Perks.LessDefense: //Done
                if (c.perks.ContainsKey(Perks.AddDefense))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux--;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(Perks.AddDefense, -1);
                }
                c.weapon.defense--;
                break;
            case Perks.LessRange: //Done
                if (c.perks.ContainsKey(perk))
                {
                    c.perks.Remove(perk);
                    c.weapon.range--;
                }
                else
                {
                }
                break;
            case Perks.IncreaseEnemyDefense: //Done
                if (c.perks.ContainsKey(Perks.ReduceEnemyDefense))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux--;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, -1);
                }
                lm.room.defenseModifier++;
                lm.room.updateEnemyStats();
                break;
            case Perks.IncreaseEnemyAttack: //Done
                if (c.perks.ContainsKey(Perks.ReduceEnemyAttack))
                {
                    aux = c.perks[perk];
                    c.perks.Remove(perk);
                    aux--;
                    c.perks.Add(perk, aux);
                }
                else
                {
                    c.perks.Add(perk, -1);
                }
                lm.room.attackModifier++;
                lm.room.updateEnemyStats();
                break;
            case Perks.SwapAttackDefense: //Done
                if (c.perks.ContainsKey(perk))
                {                   
                    c.perks.Remove(perk);
                }
                else
                {
                    c.perks.Add(perk, 1);
                }

                aux = c.weapon.attack;
                c.weapon.attack = c.weapon.defense;
                c.weapon.defense = aux;
                break;
        }

        lm.chosen = true;

        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

        myCoin = GameObject.Find("moneda");

        myCoin.GetComponent<Animation>()["CoinIntro"].time = myCoin.GetComponent<Animation>()["CoinIntro"].length;
        myCoin.GetComponent<Animation>()["CoinIntro"].speed = -1.0f;
        myCoin.GetComponent<Animation>().Play("CoinIntro");
    }
}
