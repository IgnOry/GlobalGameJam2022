using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Character player;
    [SerializeField]
    Text lifeText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player(Clone)").GetComponentInChildren<Character>();
        else
            lifeText.text = (player.currentHealth.ToString() + " / " + player.maxHealth.ToString());
    }
}
