using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { BattleStart, PlayerTurn, EnemyTurn, Win, Loss};
public class LogicManager : MonoBehaviour
{
    bool yourTurn = false;
    public Room room;

    public GameState currentState;
    // Start is called before the first frame update
    void Start()
    {
        room = GameObject.Find("Room").GetComponent<Room>();

        currentState = GameState.BattleStart;

        //Setup room battle
        room.SetUpRoom();

        //Thow Coin, depending on result
        //currentState = GameState.PlayerTurn;
        if (true)
        {
            currentState = GameState.EnemyTurn;
            StartCoroutine(processEnemiesTurn());
        }
        else 
        {
            //currentState = GameState.PlayerTurn;
            //StartCoroutine(processYourTurn());
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (currentState == GameState.PlayerTurn)
        //{
        //    processYourTurn();
        //}
        //else if (currentState == GameState.EnemyTurn)
        //{
        //    processEnemiesTurn();
        //}
    }

    bool checkWinCondition()
    {
        bool ret = true;

        for (int i = 0; i < room.enemiesArray.Length; i++)
        {
            if (!room.enemiesArray[i].GetComponent<Enemy>().dead)
            {
                ret = false;
            }
        }

        return ret;
    }

    IEnumerator processYourTurn()
    {
        //Turn

        //Change text to show the player it their turn

        //Show posible moves

        //Attack if possible
        //If, checkWinCondition again

        yield return new WaitForSeconds(1.0f);
        currentState = GameState.EnemyTurn;
        StartCoroutine(processEnemiesTurn());
    }

    IEnumerator processEnemiesTurn()
    {
        if (room.player.GetComponent<Character>().dead)
        {
            //Process Loss
        }

        for (int i = 0; i < room.enemiesArray.Length; i++)
        {
            //Enemy Turn

            //Move enemies

            //
        }

        yield return new WaitForSeconds(1.0f);

        if (checkWinCondition())
        {
            currentState = GameState.Win;
            //
        }
        else
        {
            currentState = GameState.PlayerTurn;
            StartCoroutine(processYourTurn());
        }
    }

    public void EnemyMovement (EnemyClass c)
    {
        switch (c)
        {
            case EnemyClass.Pawn:
                //If player is diagonally forward -> Attack

                //Else, move forward

                //If reach the board's end, change to Queen
                break;
            case EnemyClass.Tower:
                //Move horizontal o vertical
                break;
            case EnemyClass.Horse:
                break;
            case EnemyClass.Bishop:
                //Check diagonal -> Get to the player's closest square 
                break;
            case EnemyClass.Queen:
                //Any direction, as long as wanted
                break;
            case EnemyClass.King:
                //1 square in any direction
                break;
        }
    }
}
