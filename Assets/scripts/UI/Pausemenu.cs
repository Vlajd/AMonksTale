using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void _resume()
    {
        Destroy(this.gameObject);
    }
}
