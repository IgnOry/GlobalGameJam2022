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
    [SerializeField]
    LogicManager lm;
    [SerializeField]
    Image lifeslider;
    [SerializeField]
    Image timeSlider;
    [SerializeField]
    Text timerText;
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
            lifeslider.fillAmount = player.currentHealth / player.maxHealth;

            timeSlider.fillAmount = lm.auxTime / lm.turnTime;

            timerText.text = Mathf.Round(lm.auxTime).ToString() + " / " + lm.turnTime.ToString();
        }
    }
}
