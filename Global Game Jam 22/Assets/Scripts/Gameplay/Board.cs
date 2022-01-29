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


    

    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("a");
    }
}
