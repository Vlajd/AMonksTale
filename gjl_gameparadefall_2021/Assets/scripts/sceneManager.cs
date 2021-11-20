using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public string playScene;
    public string aboutScene;
    public string settingsScene;
    public string mainMenuScene;

    public void startPlay () {

        SceneManager.LoadScene(playScene);
    }

    public void toMainMenu () {

        SceneManager.LoadScene(mainMenuScene);
    }

    public void exitGame () {

        Application.Quit();
    }
}
