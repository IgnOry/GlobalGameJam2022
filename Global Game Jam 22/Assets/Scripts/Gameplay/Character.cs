using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponEnum {Axe, Shurikens, Gauntlets, Boomerang };

//0 - 6 positive, 7 - 11 negative
public enum Perks { AddLife, AddMovement, AddAtack, AddDefense, AddRange, ReduceEnemyDefense, ReduceEnemyAttack, HalfLife, Movement1, LessDefense, LessRange, IncreaseEnemyDefense, IncreaseEnemyAttack, SwapAttackDefense };
public class Character : MonoBehaviour
{
    public KeyValuePair<int, int> position; //X, Y
    public bool dead = false;
    public int maxHealth = 1;
    public float currentHealth;
    public int movement = 2;
    public Weapon weapon;

    public Dictionary<Perks, int> perks;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        weapon = gameObject.AddComponent<Weapon>();
        weapon.type = GameManager.GetInstance().selectedWeapon;
        weapon.setUpWeapon();

        perks = new Dictionary<Perks, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
