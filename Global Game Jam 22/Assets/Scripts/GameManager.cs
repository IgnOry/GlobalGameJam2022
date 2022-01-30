using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas loadingScreenCanvas;

    //Singleton pattern
    static private GameManager _instance;

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

        // devolvemos la instancia
        // si no existia, en este punto ya la habra creado
        return _instance;
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

    public void activateLoadingScreen(string sceneName)
    {
        StartCoroutine(loadingScreenCanvas.GetComponent<LoadingScreen>().changeScene(sceneName));
    }
}
