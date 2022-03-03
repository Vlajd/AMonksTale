using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 Offset;
    [SerializeField] private float Smoothing;
    [SerializeField] private float LowestY;
    [SerializeField] private float HighestY;

    private PlayerController _playerController;
    private Vector3 targetPosition;

    private void Awake()
    {
        _playerController = GameObject.FindWithTag("GameController").GetComponent<PlayerController>();
        if (_playerController == null) Debug.LogWarning("No Player Controller Found In Scene!");
        Offset = new Vector3(0.0f, this.transform.position.y, this.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (_playerController == null) return;

        targetPosition = _playerController._characterController[_playerController._currentPlayerIndex].transform.position + Offset;
        this.transform.position = Vector3.Lerp(this.transform.position,
                                               new Vector3(targetPosition.x, Mathf.Clamp(targetPosition.y, LowestY, HighestY), targetPosition.z),
                                               Smoothing * Time.fixedDeltaTime);
    }
}
