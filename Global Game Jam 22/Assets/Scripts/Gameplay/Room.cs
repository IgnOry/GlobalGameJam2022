using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool[,] board;
    public GameObject player;

    public Transform BoardParent;
    public Transform EnemiesParent;
    
    int enemies;
    int roomNumber = 1;

    public int size;
    public GameObject[,] boardGameObjects;
    public GameObject[] enemiesArray;
    public GameObject whiteBoardObjectPrefab;
    public GameObject blackBoardObjectPrefab;

    [SerializeField]
    GameObject pawn;
    [SerializeField]
    GameObject bishop;
    // Start is called before the first frame update
    public void SetUpRoom()
    {
        BoardParent = transform.GetChild(0);
        EnemiesParent = transform.GetChild(1);
            
        enemies = roomNumber + 1;
        SetupBoard();

        enemiesArray = new GameObject[enemies];
        
        GameObject _en;
        Enemy en;

        for (int i = 0; i < enemiesArray.Length; i++)
        {
            _en = Instantiate(pawn);
            en = _en.AddComponent<Enemy>();
            enemiesArray[i] = _en;

            //Assign class
            en.enemyClass = EnemyClass.Pawn;
            en.setUpEnemy();
        }

        SpawnEnemies();
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupBoard()
    {
        board = new bool[size, size];

        boardGameObjects = new GameObject[size, size];
        int aux = 0;

        GameObject auxGO;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                
                if (aux % 2 == 0)
                {
                    auxGO = Instantiate(whiteBoardObjectPrefab);
                }
                else
                {
                    auxGO = Instantiate(blackBoardObjectPrefab);
                }

                boardGameObjects[i, j] = auxGO;
                boardGameObjects[i, j].transform.parent = BoardParent;
                boardGameObjects[i, j].transform.position = new Vector3(i - size/2, 0, j - size/2);
                boardGameObjects[i, j].GetComponent<SquareMouseInteraction>().setPosition(i, j);
                aux++;
            }
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemiesArray.Length; i++)
        {
            Enemy en = enemiesArray[i].GetComponent<Enemy>();
            int xPos = Random.Range(0, size);

            if (en.enemyClass == EnemyClass.Pawn)
            {
                while (board[xPos, 1])
                    xPos = Random.Range(0, size);

                en.position = new KeyValuePair<int, int>(xPos, 1); //Always in second row (0 based)
            }
            else
            {
                while (board[xPos, 0])
                    xPos = Random.Range(0, size);

                en.position = new KeyValuePair<int, int>(xPos, 0); //Always in second row (0 based)
            }

            board[en.position.Key, en.position.Value] = true;

            Vector3 pos = boardGameObjects[en.position.Key, en.position.Value].transform.position;
            pos.y += 0.75f;
            enemiesArray[i].transform.position = pos;
            enemiesArray[i].transform.parent = EnemiesParent;
        }
    }

    public void SpawnPlayer()
    {
        int xPos = Random.Range(0, size);

        Character c = player.GetComponent<Character>();
        c.position = new KeyValuePair<int, int>(xPos, size - 1);

        Vector3 pos = boardGameObjects[c.position.Key, c.position.Value].transform.position;
        pos.y += 0.75f;
        board[c.position.Key, c.position.Value] = true;
        c.transform.position = pos;
    }

    public void cleanUp()
    {
        for (int i = 0; i < enemiesArray.Length; i++)
        {
            Destroy(enemiesArray[i]);
        }

        enemiesArray = null;

        for (int j = 0; j < size; j++)
        {
            for (int k = 0; k < size; k++)
            {
                Destroy(boardGameObjects[j, k]);
            }
        }

        boardGameObjects = null;

        player.transform.position = new Vector3(9999f, 9999f, 9999f);
    }
}
