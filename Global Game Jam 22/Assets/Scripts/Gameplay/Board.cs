using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int size;
    public bool[,] board;
    public GameObject[] enemies;
    public GameObject player;
    //EnemySpawn
    // Start is called before the first frame update
    void Start()
    {
        board = new bool[size, size];
    }

    public void SpawnEnemies ()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy en = enemies[i].GetComponent<Enemy>();
            int xPos = Random.Range(0, size);

            if (en.enemyClass == Class.Pawn)
            {
                while (board[xPos, 1])
                    xPos = Random.Range(0, size);

                en.position = new KeyValuePair < int, int>(xPos, 1); //Always in second row (0 based)
            }
            else
            {
                while (board[xPos, 0])
                    xPos = Random.Range(0, size);

                en.position = new KeyValuePair<int, int>(xPos, 1); //Always in second row (0 based)
            }

            board[en.position.Key, en.position.Value] = true;

        }
    }

    public void SpawnPlayer()
    {
        int xPos = Random.Range(0, size);

        player.GetComponent<Character>().position = new KeyValuePair<int, int>(xPos, size-1);
        board[xPos, size-1] = true;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("a");
    }
}
