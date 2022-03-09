using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    private PlayerController _playerController;

    public void Awake()
    {
        _playerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();

        if (_playerController == null) Debug.LogWarning("No PlayerController Found!");
    }

    public void Resume()
    {
        _playerController.TogglePausemenu();
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void resumeInternal()
    {
        Destroy(this.gameObject);
    }
}
