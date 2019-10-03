using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MenuButtonScripts : MonoBehaviour
{
    public static int Scenario; // Records the scenario we want to load into.

    public void ToScenarioOne()
    {
        SceneManager.LoadScene(1);
        Scenario = 1; // set scenario 1 as the scenario we are going to play. this will be checked in VRController.cs and possibly the scenario script
    }

    public void ToScenarioTwo()
    {
        SceneManager.LoadScene(1);
        Scenario = 2; // set scenario 2 as the scenario we are going to play. this will be checked in VRController.cs and possibly the scenario script
    }


    public void ClickExit()

    {
        Application.Quit();
    }

}
