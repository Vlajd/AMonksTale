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
        Offset = this.transform.position;
    }

    private void FixedUpdate()
    {
        targetPosition = _playerController._characterController[_playerController._currentPlayerIndex].transform.position + Offset;
        this.transform.position = Vector3.Lerp(this.transform.position,
                                               new Vector3(targetPosition.x, Mathf.Clamp(targetPosition.y, LowestY, HighestY), targetPosition.z),
                                               Smoothing * Time.fixedDeltaTime);
    }
}
