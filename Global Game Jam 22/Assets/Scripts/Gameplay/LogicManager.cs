using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    bool yourTurn = false;
    Room room;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (yourTurn)
        {
            processYourTurn();
        }
        else
        {
            processEnemiesTurn();
        }
    }

    bool checkWinCondition()
    {
        bool ret = true;

        for (int i = 0; i < room.board.enemies.Length; i++)
        {
            if (!room.board.enemies[i].GetComponent<Enemy>().dead)
            {
                ret = false;
            }
        }

        return ret;
    }

    void processYourTurn()
    { 
        if (!checkWinCondition())
        {
            //Turn
        }
        else
        {
            if (room.board.player.GetComponent<Character>().dead)
            {
                //Process Loss
            }
            else
            {
                //Process Win
            }
        }
    }

    void processEnemiesTurn()
    {
        if (room.board.player.GetComponent<Character>().dead)
        {
            //Process Loss
        }

        for (int i = 0; i < room.board.enemies.Length; i++)
        {
            //Enemy Turn
        }
    }
}
