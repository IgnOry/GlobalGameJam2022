using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { BattleStart, PlayerTurnMove, PlayerTurnAttack, EnemyTurn, Win, Loss};
public class LogicManager : MonoBehaviour
{
    public Room room;
    public AudioSource fluteSolo;
    public AudioSource orientalSolo;
    public GameState currentState;
    public GameObject myCoin;

    public RectTransform perkPanel;
    // Start is called before the first frame update
    void Awake()
    {
        room = GameObject.Find("Room").GetComponent<Room>();
        posibleMoves = new List<KeyValuePair<KeyValuePair<int, int>, int>>();
        perkPanel = GameObject.Find("Panel").GetComponent<RectTransform>();
        StartCoroutine(StartBattle());
    }

    void setUpPerks()
    {
        perkPanel.gameObject.SetActive(true);

        int perk1 = Random.Range(0, 6);
        int perk2 = perk1;

        while (perk2 == perk1)
            perk2 = Random.Range(0, 6);

        for (int i = 0; i < perkPanel.childCount - 1; i++)
        {
            perkPanel.GetChild(i).gameObject.SetActive(true);

            PerkChoice pC = perkPanel.GetChild(i).GetComponent<PerkChoice>();

            if (pC == null)
                pC = perkPanel.GetChild(i).gameObject.AddComponent<PerkChoice>();

            if (i == 0)
                pC.setPerk(perk1);
            else
                pC.setPerk(perk2);

            perkPanel.GetChild(i).GetComponent<Button>().onClick.AddListener(pC.onClick);
        }
    }

    void setUpNegativePerk()
    {
        perkPanel.gameObject.SetActive(true);

        int perk1 = Random.Range(7, 13);

        perkPanel.GetChild(2).gameObject.SetActive(true);
        PerkChoice pC = perkPanel.GetChild(2).GetComponent<PerkChoice>();

        if (pC == null)
            pC = perkPanel.GetChild(2).gameObject.AddComponent<PerkChoice>();

        pC.setPerk(perk1);

        perkPanel.GetChild(2).GetComponent<Button>().onClick.AddListener(pC.onClick);
    }

    public bool chosen = false;
    public IEnumerator StartBattle()
    {
        currentState = GameState.BattleStart;

        //Setup room battle
        room.SetUpRoom();

        posibleMoves = new List<KeyValuePair<KeyValuePair<int, int>, int>>();

        //Thow Coin, depending on result

        chosen = false;

        int coin = Random.Range(0, 100);

        myCoin = GameObject.Find("moneda");

        myCoin.GetComponent<Animation>()["CoinIntro"].time = 0.0f;
        myCoin.GetComponent<Animation>()["CoinIntro"].speed = 1.0f;
        myCoin.GetComponent<Animation>().Play("CoinIntro");

        //Animation and stuff

        //Choose perk
        if (coin > 50)
        {
            while (myCoin.GetComponent<Animation>().isPlaying)
                yield return null;
            myCoin.GetComponent<Animation>().Play("CoinHeads");
            Debug.Log("Cruz");
            while (myCoin.GetComponent<Animation>().isPlaying)
                yield return null;
            setUpNegativePerk();
            //Desventaja

            while (!chosen)
                yield return null;

            for (int i = 0; i < perkPanel.childCount; i++)
            {
                perkPanel.GetChild(i).gameObject.SetActive(false);
            }

            currentState = GameState.EnemyTurn;
            StartCoroutine(processEnemiesTurn());
        }
        else
        {
            while (myCoin.GetComponent<Animation>().isPlaying)
                yield return null;
            myCoin.GetComponent<Animation>().Play("CoinTails");
            Debug.Log("Cara");
            while (myCoin.GetComponent<Animation>().isPlaying)
                yield return null;
            setUpPerks();

            while (!chosen)
                yield return null;

            for (int i = 0; i < perkPanel.childCount; i++)
            {
                perkPanel.GetChild(i).gameObject.SetActive(false);
            }

            currentState = GameState.PlayerTurnMove;
            StartCoroutine(processYourTurn());
        }
    }

    bool checkWinCondition()
    {
        bool ret = true;

        for (int i = 0; i < room.enemiesArray.Length; i++)
        {
            if (!room.enemiesArray[i].GetComponent<Enemy>().dead)
            {
                ret = false;
            }
        }

        return ret;
    }

    bool checkLostCondition()
    {
        if (room.player.GetComponent<Character>().currentHealth <= 0)
            return true;
        else
            return false;
    }

    public bool waitingMove = false;
    public bool waitingAttack = false;
    public bool canAttack = false;
    IEnumerator processYourTurn()
    {
        //Turn

        //Change text to show the player it their turn

        //Show posible moves
        waitingMove = true;
        playerAdjacencyColor();

        while (waitingMove)
            yield return null;
        //Attack if possible
        //If, checkWinCondition again
        resetPlayerAdjacencyColor();

        canAttack = playerAttackAdjacencyColor();

        if (canAttack)
        {
            waitingAttack = true;
            currentState = GameState.PlayerTurnAttack;

            while (waitingAttack)
                yield return null;
        }
        resetPlayerAdjacencyColor();

        yield return new WaitForSeconds(1.0f);
        currentState = GameState.EnemyTurn;
        StartCoroutine(processEnemiesTurn());

        fluteSolo.volume = 1;
        orientalSolo.volume = 0;
    }

    IEnumerator processEnemiesTurn()
    {
        if (checkLostCondition())
        {
            currentState = GameState.Loss;
            Debug.Log("SOS MALISIMO");
        }

        if (currentState != GameState.Loss)
        {
            for (int i = 0; i < room.enemiesArray.Length; i++)
            {
                if (!room.enemiesArray[i].GetComponent<Enemy>().dead)
                {
                    //Enemy Turn

                    //Move enemies
                    EnemyMovement(room.enemiesArray[i].GetComponent<Enemy>());
                    yield return new WaitForSeconds(0.5f);

                    if (checkLostCondition())
                    {
                        currentState = GameState.Loss;
                        Debug.Log("SOS MALISIMO");
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        if (currentState != GameState.Loss)
        {
            if (checkWinCondition())
            {
                currentState = GameState.Win;

                Debug.Log("DE PUTOS LOCOS");
                room.cleanUp();

                yield return new WaitForSeconds(0.5f);
                room.round++;
                StartCoroutine(StartBattle());
            }
            else
            {
                currentState = GameState.PlayerTurnMove;
                StartCoroutine(processYourTurn());

                fluteSolo.volume = 0;
                orientalSolo.volume = 1;
            }
        }
        else
        {
            GameObject rC = GameObject.Find("RoundCounter");
            GameObject gO = GameObject.Find("GameOver");

            rC.GetComponent<Animation>().Play("GameOverRound");

            for (int i = 0; i < gO.transform.childCount; i++)
            {
                gO.transform.GetChild(i).GetComponentInChildren<Animation>().Play("SimpleFadeIn");
            }
        }
    }

    public void resetPlayerAdjacencyColor()
    {
        for (int i = 0; i < room.size; i++)
        {
            for (int j = 0; j < room.size; j++)
            {
                room.boardGameObjects[i, j].GetComponent<SquareMouseInteraction>().resetColor();
            }
        }
    }

    public bool playerAttackAdjacencyColor()
    {
        bool ret = false;

        Character c = room.player.GetComponent<Character>();

        Color col = new Color(251, 68, 132);

        for (int i = 1; i <= c.weapon.range; i++)
        {
            //TopLeft
            if (c.position.Key-i >= 0 && c.position.Value-i >= 0)
            {
                if (room.board[c.position.Key - i, c.position.Value - i])
                {
                    room.boardGameObjects[c.position.Key - i, c.position.Value - i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key - i, c.position.Value - i].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
            //Top
            if (c.position.Value - i >= 0)
            {
                if (room.board[c.position.Key, c.position.Value - i])
                {
                    room.boardGameObjects[c.position.Key, c.position.Value - i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key, c.position.Value - i].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
            //TopRight
            if (c.position.Key + i <= (room.size-1) && c.position.Value - i >= 0)
            {
                if (room.board[c.position.Key + i, c.position.Value - i])
                {
                    room.boardGameObjects[c.position.Key + i, c.position.Value - i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key + i, c.position.Value - i].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
            //Left
            if (c.position.Key - i >= 0)
            {
                if (room.board[c.position.Key - i, c.position.Value])
                {
                    room.boardGameObjects[c.position.Key - i, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key - i, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
            //Right
            if (c.position.Key + i <= (room.size-1))
            {
                if (room.board[c.position.Key + i, c.position.Value])
                {
                    room.boardGameObjects[c.position.Key + i, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key + i, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
            //BotLeft
            if (c.position.Key - i >= 0 && c.position.Value + i <= (room.size-1))
            {
                if (room.board[c.position.Key - i, c.position.Value + i])
                {
                    room.boardGameObjects[c.position.Key - i, c.position.Value + i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key - i, c.position.Value + i].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
            //Bot
            if (c.position.Value + i <= (room.size-1))
            {
                if (room.board[c.position.Key, c.position.Value + i])
                {
                    room.boardGameObjects[c.position.Key, c.position.Value + i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key, c.position.Value + i].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
            //BotRight
            if (c.position.Key + i <= (room.size - 1) && c.position.Value + i <= (room.size-1))
            {
                if (room.board[c.position.Key + i, c.position.Value + i])
                {
                    room.boardGameObjects[c.position.Key + i, c.position.Value + i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key + i, c.position.Value + i].GetComponent<SquareMouseInteraction>().selectable = true;
                    ret = true;
                }
            }
        }

        return ret;
    }
    public void playerAdjacencyColor()
    {
        Character c = room.player.GetComponent<Character>();
        
        Color col = new Color(254, 153, 0);

        for (int i = 1; i <= c.movement; i++)
        {
            //TopLeft
            if (c.position.Key - i >= 0 && c.position.Value - i >= 0)
            {
                if (!room.board[c.position.Key - i, c.position.Value - i])
                {
                    room.boardGameObjects[c.position.Key - i, c.position.Value - i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key - i, c.position.Value - i].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
            //Top
            if (c.position.Value - i >= 0)
            {
                if (!room.board[c.position.Key, c.position.Value - i])
                {
                    room.boardGameObjects[c.position.Key, c.position.Value - i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key, c.position.Value - i].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
            //TopRight
            if (c.position.Key + i <= (room.size - 1) && c.position.Value - i >= 0)
            {
                if (!room.board[c.position.Key + i, c.position.Value - i])
                {
                    room.boardGameObjects[c.position.Key + i, c.position.Value - i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key + i, c.position.Value - i].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
            //Left
            if (c.position.Key - i >= 0)
            {
                if (!room.board[c.position.Key - i, c.position.Value])
                {
                    room.boardGameObjects[c.position.Key - i, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key - i, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
            //Right
            if (c.position.Key + i <= (room.size - 1))
            {
                if (!room.board[c.position.Key + i, c.position.Value])
                {
                    room.boardGameObjects[c.position.Key + i, c.position.Value].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key + i, c.position.Value].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
            //BotLeft
            if (c.position.Key - i >= 0 && c.position.Value + i <= (room.size - 1))
            {
                if (!room.board[c.position.Key - i, c.position.Value + i])
                {
                    room.boardGameObjects[c.position.Key - i, c.position.Value + i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key - i, c.position.Value + i].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
            //Bot
            if (c.position.Value + i <= (room.size - 1))
            {
                if (!room.board[c.position.Key, c.position.Value + i])
                {
                    room.boardGameObjects[c.position.Key, c.position.Value + i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key, c.position.Value + i].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
            //BotRight
            if (c.position.Key + i <= (room.size - 1) && c.position.Value + i <= (room.size - 1))
            {
                if (!room.board[c.position.Key + i, c.position.Value + i])
                {
                    room.boardGameObjects[c.position.Key + i, c.position.Value + i].GetComponent<SquareMouseInteraction>().changeColor(col);
                    room.boardGameObjects[c.position.Key + i, c.position.Value + i].GetComponent<SquareMouseInteraction>().selectable = true;
                }
            }
        }
    }

    static int Compare1(KeyValuePair<KeyValuePair<int, int>, int> a, KeyValuePair<KeyValuePair<int, int>, int> b)
    {
        return a.Value.CompareTo(b.Value);
    }

    List<KeyValuePair<KeyValuePair<int, int>, int>> posibleMoves;
    public void EnemyMovement (Enemy c)
    {
        bool attack = false;

        Character character = room.player.GetComponent<Character>();
        int i = 0; //aux
        int dist1X = c.position.Key;
        int dist2X = (room.size - 1) - c.position.Key;
        int dist1Y = c.position.Value;
        int dist2Y = (room.size - 1) - c.position.Value;

        room.board[c.position.Key, c.position.Value] = false; //Set current position to false
        room.boardGameObjects[c.position.Key, c.position.Value].GetComponent<SquareMouseInteraction>().en = null;

        posibleMoves.Clear();
        //List<KeyValuePair<KeyValuePair<int, int>, int> posibleMoves; //<PosX, Posy>, Distance

        switch (c.enemyClass)
        {
            case EnemyClass.Pawn:
                //If player is diagonally forward -> Attack
                if (c.position.Value + 1 == character.position.Value)
                {
                    if (c.position.Key - 1 == character.position.Key)
                    {
                        Debug.Log("Ataque Peon");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else if (c.position.Key + 1 == character.position.Key)
                    {
                        Debug.Log("Ataque Peon");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                }
                else if (c.position.Value < (room.size - 1))
                {
                    if (!room.board[c.position.Key, c.position.Value + 1])
                        c.position = new KeyValuePair<int, int>(c.position.Key, c.position.Value + 1);

                    if (c.position.Value == (room.size - 1))
                    {
                        c.enemyClass = EnemyClass.Queen;
                        c.pawnToQueen();
                        Debug.Log("Peon -> Reina"); 
                    }
                }
                break;
            case EnemyClass.Tower:
                //Move horizontal o vertical

                if (c.position.Key == character.position.Key) //Attack
                {
                    if (c.position.Value < character.position.Value)
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key, character.position.Value-1);
                    }
                    else
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key, character.position.Value + 1);
                    }

                    Debug.Log("Ataque Torre");

                    character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                    character.gameObject.GetComponent<AudioSource>().Play();

                    character.currentHealth -= (c.attack - character.weapon.defense);
                    c.currentHealth -= (character.weapon.attack - c.defense);

                    c.GetComponentInChildren<Slider>().value = c.currentHealth;

                    if (c.currentHealth <= 0)
                    {
                        c.currentHealth = 0;
                        c.Die();
                        room.board[c.position.Key, c.position.Value] = false;
                    }
                }
                else if (c.position.Value == character.position.Value) //Attack
                {
                    if (c.position.Key < character.position.Key)
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key - 1, character.position.Value);
                    }
                    else
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key + 1, character.position.Value);
                    }

                    Debug.Log("Ataque Torre");

                    character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                    character.gameObject.GetComponent<AudioSource>().Play();

                    character.currentHealth -= (c.attack - character.weapon.defense);
                    c.currentHealth -= (character.weapon.attack - c.defense);

                    c.GetComponentInChildren<Slider>().value = c.currentHealth;

                    if (c.currentHealth <= 0)
                    {
                        c.currentHealth = 0;
                        c.Die();
                        try
                        {
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                        catch
                        {
                            Debug.Log("a");
                        }
                    }
                }
                else //Get Close
                {
                    int distanceX;
                    int distanceY;
                    int aux = room.size - 1;

                    for (i = 0; i < room.size; i++) //Horizontal
                    {
                        if (i != c.position.Key && !room.board[i, c.position.Value]) //Move is mandatory
                        {
                            distanceX = Mathf.Abs(i - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value - character.position.Value);

                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(i, c.position.Value), distanceX+distanceY);

                            posibleMoves.Add(entry);
                        }
                    }
                    for (i = 0; i < room.size; i++) //Vertical
                    {
                        if (i != c.position.Value && !room.board[c.position.Key, i]) //Move is mandatory
                        {
                            distanceX = Mathf.Abs(i - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value - character.position.Value);

                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key, i), distanceX + distanceY);

                            posibleMoves.Add(entry);
                        }
                    }

                    posibleMoves.Sort(Compare1);

                    c.position = posibleMoves[0].Key;
                }           
                break;
            case EnemyClass.Horse:
                int horseX;
                int horseY;

                if (c.position.Key - 1 >= 0 && c.position.Value - 2 >= 0)
                { 
                    if (c.position.Key - 1 == character.position.Key && c.position.Value - 2 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key - 1, c.position.Value - 2])
                    {
                        horseX = Mathf.Abs((c.position.Key - 1) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value - 2) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key - 1), (c.position.Value - 2)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key + 1 <= (room.size - 1) && c.position.Value - 2 >= 0)
                {
                    if (c.position.Key + 1 == character.position.Key && c.position.Value - 2 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key + 1, c.position.Value - 2])
                    {
                        horseX = Mathf.Abs((c.position.Key + 1) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value - 2) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key + 1), (c.position.Value - 2)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key + 2 <= (room.size - 1) && c.position.Value - 1 >= 0)
                {
                    if (c.position.Key + 2 == character.position.Key && c.position.Value - 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key + 2, c.position.Value - 1])
                    {
                        horseX = Mathf.Abs((c.position.Key + 2) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value - 1) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key + 2), (c.position.Value - 1)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key + 2 <= (room.size - 1) && c.position.Value + 1 <= (room.size - 1))
                {
                    if (c.position.Key + 2 == character.position.Key && c.position.Value + 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key + 2, c.position.Value + 1])
                    {
                        horseX = Mathf.Abs((c.position.Key + 2) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value + 1) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key + 2), (c.position.Value + 1)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key + 1 <= (room.size-1) && c.position.Value + 2 <= (room.size - 1))
                {
                    if (c.position.Key + 1 == character.position.Key && c.position.Value + 2 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key + 1, c.position.Value + 2])
                    {
                        horseX = Mathf.Abs((c.position.Key + 1) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value + 2) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key + 1), (c.position.Value + 2)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key - 1 >= 0 && c.position.Value + 2 <= (room.size - 1))
                {
                    if (c.position.Key -1 == character.position.Key && c.position.Value +2 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key - 1, c.position.Value + 2])
                    {
                        horseX = Mathf.Abs((c.position.Key - 1) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value + 2) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key - 1), (c.position.Value + 2)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key - 2 >= 0 && c.position.Value + 1 <= (room.size - 1))
                {
                    if (c.position.Key - 2 == character.position.Key && c.position.Value + 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key - 2, c.position.Value + 1])
                    {
                        horseX = Mathf.Abs((c.position.Key - 2) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value + 1) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key - 2), (c.position.Value + 1)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key - 2 >= 0 && c.position.Value - 1 >= 0)
                {
                    if (c.position.Key - 2 == character.position.Key && c.position.Value - 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Caballo");
                        attack = true;
                    }
                    else if (!room.board[c.position.Key - 2, c.position.Value - 1])
                    {
                        horseX = Mathf.Abs((c.position.Key - 2) - character.position.Key);
                        horseY = Mathf.Abs((c.position.Value - 1) - character.position.Value);
                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>((c.position.Key - 2), (c.position.Value - 1)), horseX + horseY);
                        posibleMoves.Add(entry);
                    }
                }

                if (!attack)
                {
                    posibleMoves.Sort(Compare1);
                    c.position = posibleMoves[0].Key;
                }
                else
                {
                    character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                    character.gameObject.GetComponent<AudioSource>().Play();

                    character.currentHealth -= (c.attack - character.weapon.defense);
                    c.currentHealth -= (character.weapon.attack - c.defense);

                    c.GetComponentInChildren<Slider>().value = c.currentHealth;

                    if (c.currentHealth <= 0)
                    {
                        c.currentHealth = 0;
                        c.Die();
                        room.board[c.position.Key, c.position.Value] = false;
                    }
                }
                break;
            case EnemyClass.Bishop: //Review a little bit
                int auxX = 0;
                int auxY = 0;

                i = 1;
                while (!attack && i <= c.position.Value)//TopLeft
                {
                    if (c.position.Key - i >= 0 && c.position.Value - i >= 0)
                    {
                        if (c.position.Key - i == character.position.Key && c.position.Value - i == character.position.Value)
                        {
                            attack = true;
                            auxX = c.position.Key - i + 1;
                            auxY = c.position.Value - i + 1;
                        }
                        else if (!room.board[c.position.Key - i, c.position.Value - i])
                        {
                            int distanceX;
                            int distanceY;

                            distanceX = Mathf.Abs((c.position.Key - i) - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value - i - character.position.Value);
                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key - i, c.position.Value - i), distanceX + distanceY);
                            posibleMoves.Add(entry);
                        }
                    }

                    i++;
                }

                i = 1;
                while (!attack && i <= c.position.Key)//BotLeft
                {
                    if (c.position.Key - i >= 0 && c.position.Value + i < room.size)
                    {
                        if (c.position.Key - i == character.position.Key && c.position.Value + i == character.position.Value)
                        {
                            attack = true;
                            auxX = c.position.Key - i + 1;
                            auxY = c.position.Value + i - 1;
                        }

                        else if (!room.board[c.position.Key - i, c.position.Value + i])
                        {
                            int distanceX;
                            int distanceY;

                            distanceX = Mathf.Abs((c.position.Key - i) - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value + i - character.position.Value);
                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value + 1), distanceX + distanceY);
                            posibleMoves.Add(entry);
                        }
                    }

                    i++;
                }

                i = 1;
                while (!attack && i <= (room.size - 1)) //TopRight
                {
                    if (c.position.Key + i < room.size && c.position.Value - i >= 0)
                    {
                        if (c.position.Key + i == character.position.Key && c.position.Value - i == character.position.Value)
                        {
                            attack = true;
                            auxX = c.position.Key + i - 1;
                            auxY = c.position.Value - i +1;
                        }
                        else if (!room.board[c.position.Key + i, c.position.Value - i])
                        {
                            int distanceX;
                            int distanceY;

                            distanceX = Mathf.Abs((c.position.Key + i) - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value - i - character.position.Value);
                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key + i, c.position.Value - i), distanceX + distanceY);
                            posibleMoves.Add(entry);
                        }
                    }
                    i++;
                }

                i = 1;
                while (!attack && i <= (room.size - 1)) //BotRight
                {
                    if (c.position.Key + i < room.size && c.position.Value + i < room.size)
                    {
                        if (c.position.Key + i == character.position.Key && c.position.Value + i == character.position.Value)
                        {
                            attack = true;
                            auxX = c.position.Key + i - 1;
                            auxY = c.position.Value + i - 1;
                        }
                        else if (!room.board[c.position.Key + i, c.position.Value + i])
                        {
                            int distanceX;
                            int distanceY;

                            distanceX = Mathf.Abs((c.position.Key + i) - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value + i - character.position.Value);
                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key + i, c.position.Value + i), distanceX + distanceY);
                            posibleMoves.Add(entry);
                        }
                    }

                    i++;
                }

                posibleMoves.Sort(Compare1);

                if (attack)
                {
                    character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                    character.gameObject.GetComponent<AudioSource>().Play();

                    c.position = new KeyValuePair<int, int>(auxX, auxY);

                    character.currentHealth -= (c.attack - character.weapon.defense);
                    c.currentHealth -= (character.weapon.attack - c.defense);

                    c.GetComponentInChildren<Slider>().value = c.currentHealth;

                    if (c.currentHealth <= 0)
                    {
                        c.currentHealth = 0;
                        c.Die();
                        room.board[c.position.Key, c.position.Value] = false;
                    }
                }
                else
                {
                    try
                    {
                        c.position = posibleMoves[0].Key;
                    }
                    catch
                    {
                        Debug.Log("CHECK ERROR");
                    }
                }
                break;
            case EnemyClass.Queen:
                if (c.position.Key == character.position.Key) //Attack
                {
                    if (c.position.Value < character.position.Value)
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key, character.position.Value - 1);
                    }
                    else
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key, character.position.Value + 1);
                    }

                    Debug.Log("Ataque Reina");

                    character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                    character.gameObject.GetComponent<AudioSource>().Play();

                    character.currentHealth -= (c.attack - character.weapon.defense);
                    c.currentHealth -= (character.weapon.attack - c.defense);

                    c.GetComponentInChildren<Slider>().value = c.currentHealth;

                    if (c.currentHealth <= 0)
                    {
                        c.currentHealth = 0;
                        c.Die();
                        room.board[c.position.Key, c.position.Value] = false;
                    }
                }
                else if (c.position.Value == character.position.Value) //Attack
                {
                    if (c.position.Key < character.position.Key)
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key - 1, character.position.Value);
                    }
                    else
                    {
                        c.position = new KeyValuePair<int, int>(c.position.Key + 1, character.position.Value);
                    }

                    Debug.Log("Ataque Reina");

                    character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                    character.gameObject.GetComponent<AudioSource>().Play();

                    character.currentHealth -= (c.attack - character.weapon.defense);
                    c.currentHealth -= (character.weapon.attack - c.defense);

                    c.GetComponentInChildren<Slider>().value = c.currentHealth;

                    if (c.currentHealth <= 0)
                    {
                        c.currentHealth = 0;
                        c.Die();
                        room.board[c.position.Key, c.position.Value] = false;
                    }
                }
                else //Get Close
                {
                    //Check diagonal
                    

                    int distanceX;
                    int distanceY;
                    int aux = room.size - 1;

                    for (i = 0; i < room.size; i++) //Horizontal
                    {
                        if (i != c.position.Key && !room.board[i, c.position.Value]) //Move is mandatory
                        {
                            distanceX = Mathf.Abs(i - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value - character.position.Value);

                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(i, c.position.Value), distanceX + distanceY);

                            posibleMoves.Add(entry);
                        }
                    }
                    for (i = 0; i < room.size; i++) //Vertical
                    {
                        if (i != c.position.Value && !room.board[c.position.Key, i]) //Move is mandatory
                        {
                            distanceX = Mathf.Abs(i - character.position.Key);
                            distanceY = Mathf.Abs(c.position.Value - character.position.Value);

                            KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key, i), distanceX + distanceY);

                            posibleMoves.Add(entry);
                        }
                    }

                    posibleMoves.Sort(Compare1);

                    c.position = posibleMoves[0].Key;
                }
                break;
            case EnemyClass.King:
                int dX;
                int dY;

                if (c.position.Key - 1 >= 0) //Left
                {
                    if (c.position.Key - 1 == character.position.Key && c.position.Value == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs((c.position.Key - 1) - character.position.Key);
                        dY = Mathf.Abs(c.position.Value - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key + 1 < (room.size-1)) //Right
                {
                    if (c.position.Key + 1 == character.position.Key && c.position.Value == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs((c.position.Key + 1) - character.position.Key);
                        dY = Mathf.Abs(c.position.Value - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Value - 1 >= 0) //Top
                {
                    if (c.position.Key == character.position.Key && c.position.Value -1 == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs(c.position.Key - character.position.Key);
                        dY = Mathf.Abs((c.position.Value - 1) - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Value + 1 <= (room.size - 1)) //Bot
                {
                    if (c.position.Key == character.position.Key && c.position.Value + 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs(c.position.Key - character.position.Key);
                        dY = Mathf.Abs((c.position.Value + 1) - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key - 1 >= 0 && c.position.Value - 1 >= 0) //LeftTop
                {
                    if (c.position.Key - 1 == character.position.Key && c.position.Value - 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs((c.position.Key - 1) - character.position.Key);
                        dY = Mathf.Abs((c.position.Value - 1) - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value - 1), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key - 1 >= 0 && c.position.Value + 1 < (room.size-1)) //LeftBot
                {
                    if (c.position.Key - 1 == character.position.Key && c.position.Value + 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs((c.position.Key - 1) - character.position.Key);
                        dY = Mathf.Abs((c.position.Value + 1) - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key - 1, c.position.Value + 1), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key + 1 <= (room.size - 1) && c.position.Value - 1 >= 0) //RightTop
                {
                    if (c.position.Key + 1 == character.position.Key && c.position.Value - 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs((c.position.Key + 1) - character.position.Key);
                        dY = Mathf.Abs((c.position.Value - 1) - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value - 1), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }
                if (c.position.Key + 1 <= (room.size - 1) && c.position.Value + 1 < (room.size-1)) //RightBot
                {
                    if (c.position.Key + 1 == character.position.Key && c.position.Value + 1 == character.position.Value)
                    {
                        Debug.Log("Ataque Rey");

                        character.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Defense Sound");
                        character.gameObject.GetComponent<AudioSource>().Play();

                        character.currentHealth -= (c.attack - character.weapon.defense);
                        c.currentHealth -= (character.weapon.attack - c.defense);

                        c.GetComponentInChildren<Slider>().value = c.currentHealth;

                        if (c.currentHealth <= 0)
                        {
                            c.currentHealth = 0;
                            c.Die();
                            room.board[c.position.Key, c.position.Value] = false;
                        }
                    }
                    else
                    {
                        dX = Mathf.Abs((c.position.Key + 1) - character.position.Key);
                        dY = Mathf.Abs((c.position.Value + 1) - character.position.Value);

                        KeyValuePair<KeyValuePair<int, int>, int> entry = new KeyValuePair<KeyValuePair<int, int>, int>(new KeyValuePair<int, int>(c.position.Key + 1, c.position.Value + 1), dX + dY);
                        posibleMoves.Add(entry);
                    }
                }

                posibleMoves.Sort(Compare1);
                c.position = posibleMoves[0].Key;
                break;
        }

        if (!c.dead)
        {
            Vector3 pos = new Vector3();
            try
            {
                pos = room.boardGameObjects[c.position.Key, c.position.Value].transform.position;
                room.board[c.position.Key, c.position.Value] = true;
                room.boardGameObjects[c.position.Key, c.position.Value].GetComponent<SquareMouseInteraction>().en = c;
                StartCoroutine(lerpPosition(c.transform, pos));
            }
            catch
            {
                Debug.Log("a");
            }
            //pos.y += 0.75f;
            //c.transform.position = pos;

        }
    }

    public IEnumerator lerpPosition(Transform t, Vector3 endPos, float waitTime = 1.5f)
    {
        float auxTime = 0.0f;

        while (auxTime < waitTime)
        {
            if (t != null)
            {
                t.position = Vector3.Lerp(t.position, endPos, auxTime / waitTime);
            }
                auxTime += Time.deltaTime;
                yield return null;

        }
        if (t != null)
        {
            t.position = endPos;
        }
            yield return null;     
    }
}
