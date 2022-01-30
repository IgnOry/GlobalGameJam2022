using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backToMenu()
    {
        int x = FindObjectOfType<Room>().round;
        GameManager.GetInstance().endGameUpdateMax(x);
        SceneManager.LoadScene("MainMenu");
    }
}
