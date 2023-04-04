using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponEnum type;

    public int attack;
    public int defense;

    public int accuracy;
    public int range;
    // Start is called before the first frame update
    public void setUpWeapon()
    {
        switch (type)
        {
            case WeaponEnum.Axe:
                attack = 40;
                defense = 10;
                accuracy = 100;
                range = 1;
                break;
            case WeaponEnum.Boomerang:
                attack = 15;
                defense = 20;
                accuracy = 90;
                range = 2;
                break;
            case WeaponEnum.Gauntlets:
                attack = 20;
                defense = 25;
                accuracy = 100;
                range = 1;
                break;
            case WeaponEnum.Shurikens:
                attack = 35;
                defense = 5;
                accuracy = 90;
                range = 2;
                break;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
