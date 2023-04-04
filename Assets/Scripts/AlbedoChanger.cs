using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbedoChanger : MonoBehaviour
{
    private GameObject myGOChange;

    // Start is called before the first frame update
    void Start()
    {
        myGOChange = GameObject.Find("WhiteBoardSquare");
        myGOChange.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.996f, 0.6f, 0, 1));
    }
    
}
