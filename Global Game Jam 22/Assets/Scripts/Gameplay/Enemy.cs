using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyClass { Pawn, Horse, Tower, Bishop, Queen, King };

public class Enemy : MonoBehaviour
{
    public KeyValuePair<int, int> position; //X, Y
    public EnemyClass enemyClass;
    public bool dead = false;

    public int maxHealth;
    public int currentHealth;

    public int attack;
    public int defense;

    public int accuracy;
    // Start is called before the first frame update
    
    public void setUpEnemy(int defMod, int AttMod)
    {
         switch (enemyClass)
        {
            case EnemyClass.Pawn:
                maxHealth = 45;
                attack = 30 + AttMod;
                defense = 5 + defMod;
                accuracy = 70;
                break;
            case EnemyClass.Tower:
                maxHealth = 75;
                attack = 40 + AttMod;
                defense = 10 + defMod;
                accuracy = 90;
                break;
            case EnemyClass.Horse:
                maxHealth = 50;
                attack = 35 + AttMod;
                defense = 7 + defMod;
                accuracy = 80;
                break;
            case EnemyClass.Bishop:
                maxHealth = 35;
                attack = 50 + AttMod;
                defense = 5 + defMod;
                accuracy = 95;
                break;
            case EnemyClass.Queen:
                maxHealth = 40;
                attack = 60 + AttMod;
                defense = 5 + defMod;
                accuracy = 100;
                break;
            case EnemyClass.King:
                maxHealth = 80;
                attack = 55 + AttMod;
                defense = 7 + defMod;
                accuracy = 95;
                break;
        }

        currentHealth = maxHealth;
        GetComponentInChildren<Slider>().maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        dead = true;
        GetComponent<Renderer>().enabled = false;
    }
}
