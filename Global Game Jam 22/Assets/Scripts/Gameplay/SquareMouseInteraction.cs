using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //Needed for events
using UnityEngine.UI;

public class SquareMouseInteraction : MonoBehaviour
{
    //[SerializeField] UnityEvent anEvent; // puts an easy to setup event in the inspector and anEvent references it so you can .Invoke() it
    LogicManager lM;
    Character c;
    public Enemy en = null;
    public Color ogColor;

    public bool selectable = false;
    int x; int y;
    private void OnEnable()
    {
        ogColor = GetComponent<Renderer>().material.GetColor("_Color");
        lM = GameObject.Find("LogicManager").GetComponent<LogicManager>();
        c = lM.room.player.GetComponent<Character>();
    }

    public void setPosition(int x_, int y_)
    {
        x = x_;
        y = y_;
    }

    private void OnMouseDown()
    {
        //print("You clicked the cube!");
        //anEvent.Invoke(); // Triggers the events you have setup in the inspector

        if (lM.currentState == GameState.PlayerTurnMove && lM.waitingMove && selectable)
        {
            lM.waitingMove = false;

            //Player move
            lM.room.board[c.position.Key, c.position.Value] = false; //Set previous position to false
            c.position = new KeyValuePair<int, int>(x, y);

            Vector3 pos = transform.position;
            //pos.y += 0.75f;
            lM.room.board[c.position.Key, c.position.Value] = true;

            StartCoroutine(lM.lerpPosition(c.transform, pos));
        }
        else if (lM.currentState == GameState.PlayerTurnAttack && lM.waitingAttack && selectable)
        {
            lM.waitingAttack = false;
            Debug.Log("ATAQUE");

            if (c.weapon.type == WeaponEnum.Axe) c.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Axe Hit");
            else if (c.weapon.type == WeaponEnum.Shurikens) c.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Boomerang Hit");
            else if (c.weapon.type == WeaponEnum.Gauntlets) c.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Gauntlet Hit");
            else if (c.weapon.type == WeaponEnum.Boomerang) c.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Shuriken Hit");
            c.gameObject.GetComponent<AudioSource>().Play();

            try
            {
                //c.currentHealth -= (en.attack - c.weapon.defense) * .25f;
                en.currentHealth -= (c.weapon.attack - en.defense);
                en.GetComponentInChildren<Slider>().value = en.currentHealth;

                if (en.currentHealth <= 0)
                {
                    en.currentHealth = 0;
                    en.Die();
                    lM.room.board[en.position.Key, en.position.Value] = false;
                }
            }
            catch
            {
                Debug.Log("a");
            }
        }
        else
        {
            //Debug.Log("estate quieto");
        }
    }

    public void changeColor(Color col)
    {
        Color c = new Color(col.r / 255f, col.g / 255f, col.b / 255f);

        GetComponent<Renderer>().material.SetColor("_Color", c);
    }
    public void resetColor()
    {
        GetComponent<Renderer>().material.SetColor("_Color", ogColor);
        selectable = false;
    }
}
