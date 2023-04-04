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

    [SerializeField]
    Mesh pawn;
    [SerializeField]
    Mesh tower;
    [SerializeField]
    Mesh horse;
    [SerializeField]
    Mesh bishop;
    [SerializeField]
    Mesh queen;
    [SerializeField]
    Mesh king;
    public void setUpEnemy(int defMod, int AttMod)
    {
         switch (enemyClass)
        {
            case EnemyClass.Pawn:
                maxHealth = 45;
                attack = 30 + AttMod;
                defense = 5 + defMod;
                accuracy = 70;
                GetComponent<MeshFilter>().mesh = pawn;
                break;
            case EnemyClass.Tower:
                maxHealth = 75;
                attack = 40 + AttMod;
                defense = 10 + defMod;
                accuracy = 90;
                GetComponent<MeshFilter>().mesh = tower;
                break;
            case EnemyClass.Horse:
                maxHealth = 50;
                attack = 35 + AttMod;
                defense = 7 + defMod;
                accuracy = 80;
                GetComponent<MeshFilter>().mesh = horse;
                break;
            case EnemyClass.Bishop:
                maxHealth = 35;
                attack = 50 + AttMod;
                defense = 5 + defMod;
                accuracy = 95;
                GetComponent<MeshFilter>().mesh = bishop;
                break;
            case EnemyClass.Queen:
                maxHealth = 40;
                attack = 60 + AttMod;
                defense = 5 + defMod;
                accuracy = 100;
                GetComponent<MeshFilter>().mesh = queen;
                break;
            case EnemyClass.King:
                maxHealth = 80;
                attack = 55 + AttMod;
                defense = 7 + defMod;
                accuracy = 95;
                GetComponent<MeshFilter>().mesh = king;
                break;
        }

        currentHealth = maxHealth;
        GetComponentInChildren<Slider>().maxValue = maxHealth;
    }
    
    public void pawnToQueen()
    {
        GetComponent<MeshFilter>().mesh = queen;
    }

    public void Die()
    {
        dead = true;
        GetComponent<Renderer>().enabled = false;
    }
}
