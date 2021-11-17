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
    
    void Start () {

        canvas.SetActive(true);
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
}
