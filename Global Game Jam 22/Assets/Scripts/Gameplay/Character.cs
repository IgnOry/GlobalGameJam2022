using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponEnum {Axe, Shurikens, Gauntlets, Boomerang };

public class Character : MonoBehaviour
{
    public KeyValuePair<int, int> position; //X, Y
    public bool dead = false;
    public int maxHealth = 100;
    public float currentHealth;
    public int movement = 2;
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        weapon = gameObject.AddComponent<Weapon>();
        weapon.type = GameManager.GetInstance().selectedWeapon;
        weapon.setUpWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
