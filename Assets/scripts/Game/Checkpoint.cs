using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private ButtonController DoorButton;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();

        if (_playerController == null) Debug.LogWarning("No PlayerController Found!");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerCharacterController playerCharacterController = _playerController.characterController[_playerController.currentPlayerIndex];

        if (collider.gameObject != playerCharacterController.gameObject) return;
        if (!playerCharacterController.MainPlayer) return;

        DoorButton.enabled = false;

        _playerController.currentCheckpoint = new Vector2(this.transform.position.x, this.transform.position.y);
    }
}
