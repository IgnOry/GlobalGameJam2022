using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public int actualPhase = 0;
    public bool isVanguard = false;

    public GameObject creditsScreen;
    public GameObject muteText;
    public GameObject myCamera;
    public GameObject backToMainButton;

    public GameObject vanguardAxelIcon;
    public GameObject vanguardGauntletIcon;
    public GameObject retaguardBoomerangIcon;
    public GameObject retaguardShuriIcon;

    public GameObject vanguardButton;
    public GameObject retaguardButton;
    public GameObject offensiveButton;
    public GameObject defensiveButton;

    public GameObject mainLogo;
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject creditsButton;

    public void axeIconBehaviour()
    {
        if (actualPhase == 2)
        {
            GameManager.GetInstance().selectedWeapon = WeaponEnum.Axe;
            GameManager.GetInstance().activateLoadingScreen("GameplayScene");
        }

        else goSelectionVanguard();
    }
    public void boomerangIconBehaviour()
    {
        if (actualPhase == 2)
        {
            GameManager.GetInstance().selectedWeapon = WeaponEnum.Boomerang;
            GameManager.GetInstance().activateLoadingScreen("GameplayScene");
        }

        else goSelectionRetaguard();
    }

    public void gauntletIconBehaviour()
    {
        GameManager.GetInstance().selectedWeapon = WeaponEnum.Gauntlets;
        GameManager.GetInstance().activateLoadingScreen("GameplayScene");
    }

    public void shurikensIconBehaviour()
    {
        GameManager.GetInstance().selectedWeapon = WeaponEnum.Shurikens;
        GameManager.GetInstance().activateLoadingScreen("GameplayScene");
    }

    public void offensiveButtonBehaviour()
    {
        if (isVanguard)
        {
            GameManager.GetInstance().selectedWeapon = WeaponEnum.Axe;
            GameManager.GetInstance().activateLoadingScreen("GameplayScene");
        }
        else
        {
            GameManager.GetInstance().selectedWeapon = WeaponEnum.Shurikens;
            GameManager.GetInstance().activateLoadingScreen("GameplayScene");
        }
    }

    public void defensiveButtonBehaviour()
    {
        if (isVanguard)
        {
            GameManager.GetInstance().selectedWeapon = WeaponEnum.Gauntlets;
            GameManager.GetInstance().activateLoadingScreen("GameplayScene");
        }
        else
        {
            GameManager.GetInstance().selectedWeapon = WeaponEnum.Boomerang;
            GameManager.GetInstance().activateLoadingScreen("GameplayScene");
        }
    }

    public void goSelectionScreen1()
    {
        playButton.GetComponent<Button>().enabled = false;
        settingsButton.GetComponent<Button>().enabled = false;
        creditsButton.GetComponent<Button>().enabled = false;

        vanguardAxelIcon.GetComponent<Button>().enabled = true;
        retaguardBoomerangIcon.GetComponent<Button>().enabled = true;
        vanguardButton.GetComponent<Button>().enabled = true;
        retaguardButton.GetComponent<Button>().enabled = true;

        mainLogo.GetComponent<Animation>()["SwipeUpLogo"].time = 0.0f;
        mainLogo.GetComponent<Animation>()["SwipeUpLogo"].speed = 1.0f;
        mainLogo.GetComponent<Animation>().Play("SwipeUpLogo");
        playButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        playButton.GetComponent<Animation>()["SwipeDown"].speed = 1.0f;
        playButton.GetComponent<Animation>().Play("SwipeDown");
        creditsButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        creditsButton.GetComponent<Animation>()["SwipeDown"].speed = 1.0f;
        creditsButton.GetComponent<Animation>().Play("SwipeDown");
        settingsButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        settingsButton.GetComponent<Animation>()["SwipeDown"].speed = 1.0f;
        settingsButton.GetComponent<Animation>().Play("SwipeDown");

        vanguardAxelIcon.GetComponent<Animation>().Play("SwipeWeaponsIcon");
        retaguardBoomerangIcon.GetComponent<Animation>().Play("SwipeWeaponsIcon");
        vanguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");
        retaguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");

        actualPhase = 1;
    }

    public void goSelectionVanguard()
    {
        isVanguard = true;

        vanguardGauntletIcon.GetComponent<Button>().enabled = true;
        offensiveButton.GetComponent<Button>().enabled = true;
        defensiveButton.GetComponent<Button>().enabled = true;

        vanguardButton.GetComponent<Button>().enabled = false;
        retaguardButton.GetComponent<Button>().enabled = false;
        retaguardBoomerangIcon.GetComponent<Button>().enabled = false;

        retaguardBoomerangIcon.GetComponent<Animation>()["SwipeWeaponsIcon"].speed = -1;
        retaguardBoomerangIcon.GetComponent<Animation>()["SwipeWeaponsIcon"].time = retaguardBoomerangIcon.GetComponent<Animation>()["SwipeWeaponsIcon"].length;
        retaguardBoomerangIcon.GetComponent<Animation>().Play("SwipeWeaponsIcon");

        vanguardButton.GetComponent<Animation>()["SwipeWeaponsText"].speed = -1;
        vanguardButton.GetComponent<Animation>()["SwipeWeaponsText"].time = vanguardButton.GetComponent<Animation>()["SwipeWeaponsText"].length;
        vanguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");
        retaguardButton.GetComponent<Animation>()["SwipeWeaponsText"].speed = -1;
        retaguardButton.GetComponent<Animation>()["SwipeWeaponsText"].time = retaguardButton.GetComponent<Animation>()["SwipeWeaponsText"].length;
        retaguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");

        offensiveButton.transform.GetChild(0).gameObject.GetComponent<Animation>().Play("AppearWeaponsText");
        defensiveButton.transform.GetChild(0).gameObject.GetComponent<Animation>().Play("AppearWeaponsText");

        vanguardGauntletIcon.GetComponent<Animation>().Play();

        actualPhase = 2;
    }

    public void goSelectionRetaguard()
    {
        vanguardButton.GetComponent<Button>().enabled = false;
        vanguardAxelIcon.GetComponent<Button>().enabled = false;
        retaguardButton.GetComponent<Button>().enabled = false;

        retaguardBoomerangIcon.GetComponent<Button>().enabled = true;
        offensiveButton.GetComponent<Button>().enabled = true;
        defensiveButton.GetComponent<Button>().enabled = true;

        vanguardAxelIcon.GetComponent<Animation>()["SwipeWeaponsIcon"].speed = -1;
        vanguardAxelIcon.GetComponent<Animation>()["SwipeWeaponsIcon"].time = retaguardBoomerangIcon.GetComponent<Animation>()["SwipeWeaponsIcon"].length;
        vanguardAxelIcon.GetComponent<Animation>().Play("SwipeWeaponsIcon");

        vanguardButton.GetComponent<Animation>()["SwipeWeaponsText"].speed = -1;
        vanguardButton.GetComponent<Animation>()["SwipeWeaponsText"].time = vanguardButton.GetComponent<Animation>()["SwipeWeaponsText"].length;
        vanguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");
        retaguardButton.GetComponent<Animation>()["SwipeWeaponsText"].speed = -1;
        retaguardButton.GetComponent<Animation>()["SwipeWeaponsText"].time = retaguardButton.GetComponent<Animation>()["SwipeWeaponsText"].length;
        retaguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");

        offensiveButton.transform.GetChild(0).GetComponent<Animation>().Play("AppearWeaponsText");
        defensiveButton.transform.GetChild(0).GetComponent<Animation>().Play("AppearWeaponsText");

        retaguardShuriIcon.GetComponent<Animation>().Play();

        actualPhase = 2;
    }

    public void onCreditsButtonPressed()
    {
        playButton.GetComponent<Button>().enabled = false;
        settingsButton.GetComponent<Button>().enabled = false;
        creditsButton.GetComponent<Button>().enabled = false;

        mainLogo.GetComponent<Animation>()["SwipeUpLogo"].time = 0.0f;
        mainLogo.GetComponent<Animation>()["SwipeUpLogo"].speed = 1;
        mainLogo.GetComponent<Animation>().Play("SwipeUpLogo");

        playButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        playButton.GetComponent<Animation>()["SwipeDown"].speed = 1;
        playButton.GetComponent<Animation>().Play("SwipeDown");

        creditsButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        creditsButton.GetComponent<Animation>()["SwipeDown"].speed = 1;
        creditsButton.GetComponent<Animation>().Play("SwipeDown");

        settingsButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        settingsButton.GetComponent<Animation>()["SwipeDown"].speed = 1;
        settingsButton.GetComponent<Animation>().Play("SwipeDown");

        foreach (Transform child in creditsScreen.transform)
        {
            if(child.name != "BackToMainButton")
            {
                child.gameObject.GetComponent<Animation>()[child.gameObject.GetComponent<Animation>().name + "Credits"].time = 0.0f;
                child.gameObject.GetComponent<Animation>()[child.gameObject.GetComponent<Animation>().name + "Credits"].speed = 1.0f;
                child.gameObject.GetComponent<Animation>().Play();
                child.GetChild(0).GetComponent<Animation>()[child.GetComponent<Animation>().name + "Credits"].time = 0.0f;
                child.GetChild(0).GetComponent<Animation>()[child.GetComponent<Animation>().name + "Credits"].speed = 1.0f;
                child.GetChild(0).GetComponent<Animation>().Play();
            }
        }

        backToMainButton.GetComponent<Button>().enabled = true;

    }

    public void onBackToMainButtonPressed()
    {
        backToMainButton.GetComponent<Button>().enabled = false;

        foreach (Transform child in creditsScreen.transform)
        {
            if (child.name != "BackToMainButton")
            {
                child.gameObject.GetComponent<Animation>()[child.gameObject.GetComponent<Animation>().name + "Credits"].speed = -1;
                child.gameObject.GetComponent<Animation>()[child.gameObject.GetComponent<Animation>().name + "Credits"].time =
                    child.gameObject.GetComponent<Animation>()[child.gameObject.GetComponent<Animation>().name + "Credits"].length;
                child.gameObject.GetComponent<Animation>().Play();

                child.GetChild(0).GetComponent<Animation>()[child.GetComponent<Animation>().name + "Credits"].speed = -1;
                child.GetChild(0).GetComponent<Animation>()[child.GetComponent<Animation>().name + "Credits"].time =
                    child.GetChild(0).GetComponent<Animation>()[child.GetComponent<Animation>().name + "Credits"].length;
                child.GetChild(0).GetComponent<Animation>().Play();
            }
        }

        mainLogo.GetComponent<Animation>()["SwipeUpLogo"].speed = -1;
        mainLogo.GetComponent<Animation>()["SwipeUpLogo"].time = mainLogo.GetComponent<Animation>()["SwipeUpLogo"].length;
        mainLogo.GetComponent<Animation>().Play("SwipeUpLogo");
        playButton.GetComponent<Animation>()["SwipeDown"].speed = -1;
        playButton.GetComponent<Animation>()["SwipeDown"].time = playButton.GetComponent<Animation>()["SwipeDown"].length;
        playButton.GetComponent<Animation>().Play("SwipeDown");
        creditsButton.GetComponent<Animation>()["SwipeDown"].speed = -1;
        creditsButton.GetComponent<Animation>()["SwipeDown"].time = creditsButton.GetComponent<Animation>()["SwipeDown"].length;
        creditsButton.GetComponent<Animation>().Play("SwipeDown");
        settingsButton.GetComponent<Animation>()["SwipeDown"].speed = -1;
        settingsButton.GetComponent<Animation>()["SwipeDown"].time = settingsButton.GetComponent<Animation>()["SwipeDown"].length;
        settingsButton.GetComponent<Animation>().Play("SwipeDown");

        playButton.GetComponent<Button>().enabled = true;
        settingsButton.GetComponent<Button>().enabled = true;
        creditsButton.GetComponent<Button>().enabled = true;
    }

    public void onMuteButtonPressed()
    {
        if (muteText.GetComponent<Text>().text == "Mute")
        {
            muteText.GetComponent<Text>().text = "Unmute";
            myCamera.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            muteText.GetComponent<Text>().text = "Mute";
            myCamera.GetComponent<AudioSource>().volume = 1;
        }
    }
}
