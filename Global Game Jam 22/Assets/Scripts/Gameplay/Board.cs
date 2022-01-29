using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int size;
    public bool[,] board;
    public GameObject[] enemies;
    public GameObject player;
    public GameObject[,] boardGameObjects;

    public GameObject whiteBoardObjectPrefab;
    public GameObject blackBoardObjectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        board = new bool[size, size];

        boardGameObjects = new GameObject[size, size];
        int aux = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (aux % 2 == 0)
                    boardGameObjects[i, j] = Instantiate(whiteBoardObjectPrefab);
                else
                    boardGameObjects[i, j] = Instantiate(blackBoardObjectPrefab);

                boardGameObjects[i, j].transform.position = new Vector3(i, 0, j);
                aux++;
            }
        }
    }

    public void SpawnEnemies ()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy en = enemies[i].GetComponent<Enemy>();
            int xPos = Random.Range(0, size);

            if (en.enemyClass == EnemyClass.Pawn)
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

            Vector3 pos = boardGameObjects[en.position.Key, en.position.Value].transform.position;
            pos.y += 0.75f;
            enemies[i].transform.position = pos;
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
