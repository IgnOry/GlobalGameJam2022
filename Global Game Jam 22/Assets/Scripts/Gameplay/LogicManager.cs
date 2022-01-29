using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { BattleStart, PlayerTurnMove, PlayerTurnAttack, EnemyTurn, Win, Loss};
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

    public bool waitingMove = false;
    public bool waitingAttack = false;
    public bool canAttack = false;
    IEnumerator processYourTurn()
    {
        //Turn

        //Change text to show the player it their turn

        //Show posible moves
        waitingMove = true;
        playerAdjacencyColor();

        while (waitingMove)
            yield return null;
        //Attack if possible
        //If, checkWinCondition again
        resetPlayerAdjacencyColor();

        canAttack = playerAttackAdjacencyColor();

        if (canAttack)
        {
            waitingAttack = true;
            currentState = GameState.PlayerTurnAttack;

            while (waitingAttack)
                yield return null;
        }
        resetPlayerAdjacencyColor();

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
            EnemyMovement(room.enemiesArray[i].GetComponent<Enemy>());
            yield return new WaitForSeconds(0.5f);
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
            currentState = GameState.PlayerTurnMove;
            StartCoroutine(processYourTurn());
        }
    }

    public void resetPlayerAdjacencyColor()
    {
        for (int i = 0; i < room.size; i++)
        {
            for (int j = 0; j < room.size; j++)
            {
                room.boardGameObjects[i, j].GetComponent<SquareMouseInteraction>().resetColor();
            }
        }
    }

    public bool playerAttackAdjacencyColor()
    {
        bool ret = false;

        Character c = room.player.GetComponent<Character>();

        Color col = new Color(251, 68, 132);

        //Base directions
        if (c.position.Key < (room.size - 1))
        {
            if (room.board[c.position.Key + 1, c.position.Value])
            {
                room.boardGameObjects[c.position.Key + 1, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key + 1, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }
        if (c.position.Key > 0)
        {
            if (room.board[c.position.Key - 1, c.position.Value])
            {
                room.boardGameObjects[c.position.Key - 1, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key - 1, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }
        if (c.position.Value < (room.size - 1))
        {
            if (room.board[c.position.Key, c.position.Value + 1])
            {
                room.boardGameObjects[c.position.Key, c.position.Value + 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key, c.position.Value + 1].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }
        if (c.position.Value > 0)
        {
            if (room.board[c.position.Key, c.position.Value - 1])
            {
                room.boardGameObjects[c.position.Key, c.position.Value - 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key, c.position.Value - 1].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }

        //Diagonal directions
        if (c.position.Key < (room.size - 1) && c.position.Value < (room.size - 1))
        {
            if (room.board[c.position.Key + 1, c.position.Value + 1])
            {
                room.boardGameObjects[c.position.Key + 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key + 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }
        if (c.position.Key > 0 && c.position.Value < (room.size - 1))
        {
            if (room.board[c.position.Key - 1, c.position.Value + 1])
            {
                room.boardGameObjects[c.position.Key - 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key - 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }
        if (c.position.Key > 0 && c.position.Value > 0)
        {
            if (room.board[c.position.Key - 1, c.position.Value - 1])
            {
                room.boardGameObjects[c.position.Key - 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key - 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }
        if (c.position.Key < (room.size - 1) && c.position.Value > 0)
        {
            if (room.board[c.position.Key + 1, c.position.Value - 1])
            {
                room.boardGameObjects[c.position.Key + 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key + 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().selectable = true;
                ret = true;
            }
        }

        return ret;
    }
    public void playerAdjacencyColor()
    {
        Character c = room.player.GetComponent<Character>();
        
        Color col = new Color(254, 153, 0);

        //Base directions
        if (c.position.Key < (room.size-1))
        {
            if (!room.board[c.position.Key + 1, c.position.Value])
            {
                room.boardGameObjects[c.position.Key + 1, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key + 1, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }
        if (c.position.Key > 0)
        {
            if (!room.board[c.position.Key - 1, c.position.Value])
            {
                room.boardGameObjects[c.position.Key - 1, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key - 1, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }
        if (c.position.Value < (room.size - 1))
        {
            if (!room.board[c.position.Key, c.position.Value + 1])
            {
                room.boardGameObjects[c.position.Key, c.position.Value + 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key, c.position.Value + 1].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }
        if (c.position.Value > 0)
        {
            if (!room.board[c.position.Key, c.position.Value - 1])
            {
                room.boardGameObjects[c.position.Key, c.position.Value - 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key, c.position.Value - 1].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }

        //Diagonal directions
        if (c.position.Key < (room.size - 1) && c.position.Value < (room.size - 1))
        {
            if (!room.board[c.position.Key + 1, c.position.Value + 1])
            {
                room.boardGameObjects[c.position.Key + 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key + 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }
        if (c.position.Key > 0 && c.position.Value < (room.size -1))
        {
            if (!room.board[c.position.Key - 1, c.position.Value + 1])
            {
                room.boardGameObjects[c.position.Key - 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key - 1, c.position.Value + 1].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }
        if (c.position.Key > 0 && c.position.Value > 0)
        {
            if (!room.board[c.position.Key - 1, c.position.Value - 1])
            {
                room.boardGameObjects[c.position.Key - 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key - 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }
        if (c.position.Key < (room.size - 1) && c.position.Value > 0)
        {
            if (!room.board[c.position.Key + 1, c.position.Value - 1])
            {
                room.boardGameObjects[c.position.Key + 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().changeColor(col);
                room.boardGameObjects[c.position.Key + 1, c.position.Value - 1].GetComponent<SquareMouseInteraction>().selectable = true;
            }
        }        
    }

    public void EnemyMovement (Enemy c)
    {
        int minDistanceToEdgeX;
        int minDistanceToEdgeY;
        int aux = room.size - 1;

        if (aux - c.position.Key > c.position.Key)
            minDistanceToEdgeX = c.position.Key;
        else
            minDistanceToEdgeX = aux - c.position.Key;

        if (aux - c.position.Value > c.position.Value)
            minDistanceToEdgeY = c.position.Value;
        else
            minDistanceToEdgeY = aux - c.position.Value;

        room.board[c.position.Key, c.position.Value] = false; //Set current position to false


        switch (c.enemyClass)
        {
            case EnemyClass.Pawn:
                //If player is diagonally forward -> Attack

                if (c.position.Value < (room.size -1))
                {
                    if (!room.board[c.position.Key, c.position.Value + 1])
                    c.position = new KeyValuePair<int, int>(c.position.Key, c.position.Value + 1);
                }
                //Else, move forward

                //If reach the board's end, change to Queen
                break;
            case EnemyClass.Tower:
                //Move horizontal o vertical

                int hor = Random.Range(0, 1);
                //Vertical
                if (hor == 0)
                {
                    if (c.position.Value < (room.size - 1))
                    {
                        if (!room.board[c.position.Key, room.size - 1])
                            c.position = new KeyValuePair<int, int>(c.position.Key, room.size - 1);
                    }
                    else
                    {
                        if (!room.board[c.position.Key, 0])
                            c.position = new KeyValuePair<int, int>(c.position.Key, 0);
                    }
                }
                else
                {
                    //Horizontal
                    if (c.position.Key < (room.size - 1))
                    {
                        if (!room.board[room.size - 1, c.position.Value])
                            c.position = new KeyValuePair<int, int>(room.size - 1, c.position.Value);
                    }
                    else
                    {
                        if (!room.board[0, c.position.Value])
                            c.position = new KeyValuePair<int, int>(0, c.position.Value);
                    }
                }
                break;
            case EnemyClass.Horse:
                break;
            case EnemyClass.Bishop:

                if (c.position.Value < (room.size-1) && c.position.Key < (room.size-1))
                {
                    if (!room.board[c.position.Key + 1, c.position.Value + 1])
                        c.position = new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value + 1);
                }
                else if (c.position.Value > 0 && c.position.Key > 0)
                {
                    if (!room.board[c.position.Key - 1, c.position.Value - 1])
                        c.position = new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value - 1);
                }
                //Check diagonal -> Get to the player's closest square 
                break;
            case EnemyClass.Queen:
                //Any direction, as long as wanted
                break;
            case EnemyClass.King:
                //1 square in any direction
                int rand = Random.Range(0, 7);

                switch (rand)
                {
                    case 0:
                        if (c.position.Value > 0 && !room.board[c.position.Key, c.position.Value - 1])
                            c.position = new KeyValuePair<int, int>(c.position.Key, c.position.Value - 1);
                        break;
                    case 1:
                        if (c.position.Value < (room.size -1) && !room.board[c.position.Key, c.position.Value + 1])
                            c.position = new KeyValuePair<int, int>(c.position.Key, c.position.Value + 1);
                        break;
                    case 2:
                        if (c.position.Key < (room.size - 1) && !room.board[c.position.Key + 1, c.position.Value])
                            c.position = new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value);
                        break;
                    case 3:
                        if (c.position.Key > 0 && !room.board[c.position.Key - 1, c.position.Value])
                            c.position = new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value);
                        break;
                    case 4:
                        if (c.position.Key < (room.size - 1) && c.position.Value < (room.size - 1) && !room.board[c.position.Key + 1, c.position.Value + 1])
                            c.position = new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value + 1);
                        break;
                    case 5:
                        if (c.position.Key > 0 && c.position.Value < (room.size - 1) && !room.board[c.position.Key - 1, c.position.Value + 1])
                            c.position = new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value + 1);
                        break;
                    case 6:
                        if (c.position.Key > 0 && c.position.Value > 0 && !room.board[c.position.Key - 1, c.position.Value - 1])
                            c.position = new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value - 1);
                        break;
                    case 7:
                        if (c.position.Key < (room.size - 1) && c.position.Value > 0 && !room.board[c.position.Key + 1, c.position.Value - 1])
                            c.position = new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value - 1);
                        break;
                }
                break;
        }

        Vector3 pos = room.boardGameObjects[c.position.Key, c.position.Value].transform.position;
        pos.y += 0.75f;
        room.board[c.position.Key, c.position.Value] = true;
        c.transform.position = pos;
    }
}
