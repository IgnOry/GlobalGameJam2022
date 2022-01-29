using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //Needed for events

public class SquareMouseInteraction : MonoBehaviour
{
    //[SerializeField] UnityEvent anEvent; // puts an easy to setup event in the inspector and anEvent references it so you can .Invoke() it
    LogicManager lM;

    public Color ogColor;

    public bool selectable = false;
    int x; int y;
    private void OnEnable()
    {
        ogColor = GetComponent<Renderer>().material.GetColor("_Color");
        lM = GameObject.Find("LogicManager").GetComponent<LogicManager>();
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
            Character c = lM.room.player.GetComponent<Character>();
            lM.room.board[c.position.Key, c.position.Value] = false; //Set previous position to false
            c.position = new KeyValuePair<int, int>(x, y);

            Vector3 pos = transform.position;
            pos.y += 0.75f;
            lM.room.board[c.position.Key, c.position.Value] = true;
            c.transform.position = pos;
        }
        else if (lM.currentState == GameState.PlayerTurnAttack && lM.waitingAttack && selectable)
        {
            lM.waitingAttack = false;
            Debug.Log("ATAQUE");
            ////Player move
            //Character c = lM.room.player.GetComponent<Character>();
            //lM.room.board[c.position.Key, c.position.Value] = false; //Set previous position to false
            //c.position = new KeyValuePair<int, int>(x, y);

            //Vector3 pos = transform.position;
            //pos.y += 0.75f;
            //lM.room.board[c.position.Key, c.position.Value] = true;
            //c.transform.position = pos;
        }
        else
        {
            Debug.Log("estate quieto");
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
