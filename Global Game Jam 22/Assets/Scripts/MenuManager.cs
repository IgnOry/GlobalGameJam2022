using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public int actualPhase = 0;

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
        if (actualPhase == 2) GameManager.GetInstance().activateLoadingScreen("CreditsScene");
        else goSelectionVanguard();
    }
    public void boomerangIconBehaviour()
    {
        if (actualPhase == 2) GameManager.GetInstance().activateLoadingScreen("CreditsScene");
        else goSelectionRetaguard();
    }

    public void goSelectionScreen1()
    {
        playButton.GetComponent<Button>().enabled = false;
        settingsButton.GetComponent<Button>().enabled = false;
        creditsButton.GetComponent<Button>().enabled = false;

        mainLogo.GetComponent<Animation>().Play("SwipeUpLogo");
        playButton.GetComponent<Animation>().Play("SwipeDown");
        creditsButton.GetComponent<Animation>().Play("SwipeDown");
        settingsButton.GetComponent<Animation>().Play("SwipeDown");

        vanguardAxelIcon.GetComponent<Animation>().Play("SwipeWeaponsIcon");
        retaguardBoomerangIcon.GetComponent<Animation>().Play("SwipeWeaponsIcon");
        vanguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");
        retaguardButton.GetComponent<Animation>().Play("SwipeWeaponsText");

        actualPhase = 1;
    }

    public void goSelectionVanguard()
    {
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
        mainLogo.GetComponent<Animation>().Play("SwipeUpLogo");
        playButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        playButton.GetComponent<Animation>().Play("SwipeDown");
        creditsButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        creditsButton.GetComponent<Animation>().Play("SwipeDown");
        settingsButton.GetComponent<Animation>()["SwipeDown"].time = 0.0f;
        settingsButton.GetComponent<Animation>().Play("SwipeDown");

        foreach (Transform child in creditsScreen.transform)
        {
            if(child.name != "BackToMainButton")
            {
                child.gameObject.GetComponent<Animation>()[child.gameObject.GetComponent<Animation>().name + "Credits"].time = 0.0f;
                child.gameObject.GetComponent<Animation>().Play();
                child.GetChild(0).GetComponent<Animation>()[child.GetComponent<Animation>().name + "Credits"].time = 0.0f;
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
