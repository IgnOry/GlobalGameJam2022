using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyClass { Pawn, Horse, Tower, Bishop, Queen, King };

public class Enemy : MonoBehaviour
{
    public KeyValuePair<int, int> position; //X, Y
    public EnemyClass enemyClass;
    public bool dead = false;


    // Start is called before the first frame update
    void Start()
    {
        //enemyClass = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
