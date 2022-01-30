using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        int aux = GameManager.GetInstance().getMaxRounds();

        gameObject.GetComponent<Text>().text = "Max Rounds: " + aux;
    }
}
