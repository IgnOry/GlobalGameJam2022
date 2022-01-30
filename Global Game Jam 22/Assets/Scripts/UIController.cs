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
    [SerializeField]
    Text roundCounter;
    [SerializeField]
    Text AttDef;
    [SerializeField]
    Text Range;
    [SerializeField]
    Room r;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponentInChildren<Character>();
        else
        {

            lifeText.text = (player.currentHealth.ToString() + " / " + player.maxHealth.ToString());
            roundCounter.text = "ROUND " + r.round;
            AttDef.text = "Defense: " + player.weapon.defense;
            Range.text = "Attack: " + player.weapon.attack;
        }
    }
}
