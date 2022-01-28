using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Board board;
    public GameObject player;
    int enemies;
    int roomNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        enemies = roomNumber + 2;
        board = GetComponent<Board>();
        board.player = player;

        board.enemies = new GameObject[enemies];
        
        GameObject _en;
        Enemy en;

        for (int i = 0; i < board.enemies.Length; i++)
        {
            _en = new GameObject("Enemy");
            en = _en.AddComponent<Enemy>();
            board.enemies[i] = _en;
        }

        board.SpawnEnemies();
        board.SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
