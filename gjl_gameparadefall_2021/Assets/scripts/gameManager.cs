using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private string activeSceneName;
    public float minY = -10f;
    public float maxY = 50f;
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private GameObject ghostPlayer;
    [SerializeField] private GameObject canvas;
    public AudioSource[] g_audio = new AudioSource[2];
    
    void Start () {

        canvas.SetActive(true);
        g_audio[0].Play(0);
    }

    void Update () {

        // reload scene
        if (mainPlayer.transform.position.y < minY)
            SceneManager.LoadScene(activeSceneName);
    }

    public void pressRestartButton () {

        // reload scene on press
        SceneManager.LoadScene(activeSceneName);
    }

    public void firstPlay () {

        g_audio[0].Play(0);
        gameObject.GetComponent<Animator>().Play("firstPlay");
    }

    public void secondPlay () {

        g_audio[1].Play(0);
        gameObject.GetComponent<Animator>().Play("secondPlay");
    }


}
