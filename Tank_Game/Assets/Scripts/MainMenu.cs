using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using TMPro;



using static System.Convert;



public class MainMenu : MonoBehaviour

{

    public static MainMenu menu;



    public List<Toggle> toggles;



    public GameManager manager;

    public Navigator navi;

    public P_Gen gen;

    public TMP_InputField pIf;

    public Camera canvasCamera;

    public bool fScreenEnabled = false;

    public bool motd = false;

    public int seedVal;



    private void Start()

    {

        menu = this;

    }



    public void Play()

    {
        canvasCamera.enabled = false;
        manager.gameplayStart = true;

        navi.Navigate("Canvas", "GamePlay");

    }



    public void Options()

    {

        navi.Navigate("MainMenu", "OptionsMenu");

    }



    public void ReturnToMainMenu()
    {

        navi.Navigate("OptionsMenu", "MainMenu");

    }



    public void Apply()

    {

        //Apply any possible changes

        gen.mapOfTheDay = motd;

        gen.genSeed = seedVal;

    }



    public void UpdateSeedValue()

    {

        seedVal = ToInt32(pIf.text);

    }



    public void ToggleFullScreen()

    {

        Toggle fSToggle = FindToggle("FullscreenToggle");

        fScreenEnabled = fSToggle.isOn;

    }



    public void ToggleMapOfTheDay()

    {

        Toggle motdToggle = FindToggle("MapOfTheDayToggle");

        motd = motdToggle.isOn;

    }



    public void Quit()

    {

        Debug.Log("Application has quit!!!");

        Application.Quit();

    }



    public Toggle FindToggle(string name)

    {

        for (int i = 0; i < toggles.Count; i++)

        {

            if (toggles[i].name == name)

            {

                return toggles[i];

            }

        }

        throw new System.NullReferenceException();

    }

}