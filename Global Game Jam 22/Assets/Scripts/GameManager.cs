using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas loadingScreenCanvas;

    //Singleton pattern
    static private GameManager _instance;
    static int maxRounds;
    static public GameManager GetInstance()
    {
        if (_instance == null)
        {
            // creamos un nuevo objeto llamado "_MiGameManager"
            GameObject go = new GameObject("GameManager");

            // anadimos el script "GameManager" al objeto
            go.AddComponent<GameManager>();
            _instance = go.GetComponent<GameManager>();
            DontDestroyOnLoad(go);
        }

        if (PlayerPrefs.HasKey("MaxRound"))
        {
            maxRounds = PlayerPrefs.GetInt("MaxRounds");
        }
        else
        {
            PlayerPrefs.SetInt("MaxRound", 0);
            maxRounds = PlayerPrefs.GetInt("MaxRounds");
        }
        // devolvemos la instancia
        // si no existia, en este punto ya la habra creado
        return _instance;
    }

    public void endGameUpdateMax(int x)
    {
        if (x > maxRounds)
        {
            PlayerPrefs.SetInt("MaxRounds", x);
        }
    }

    public int getMaxRounds()
    {
        return maxRounds;
    }

    public WeaponEnum selectedWeapon;

    // Start is called before the first frame update
    void OnEnable()
    {
        selectedWeapon = WeaponEnum.Axe;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
