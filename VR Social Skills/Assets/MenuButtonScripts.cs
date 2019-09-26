using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtonScripts : MonoBehaviour
{

    public void ToSceneOne()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickExit()

    {
        Application.Quit();
    }

}
