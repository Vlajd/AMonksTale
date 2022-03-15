  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 /// TODO: Fix Ghost Looking Not Smooth (for some reason he kinda has "framedrops" (not actual framedrops tho)) ///
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private float Smoothing;
    [SerializeField] private float LowestY;
    [SerializeField] private float HighestY;

    [HideInInspector] public Camera CameraRenderer;

    private PlayerController _playerController;
    private Vector3 _targetPosition;

    private void Awake()
    {
        CameraRenderer = this.GetComponent<Camera>();
    }

    private void Start()
    {
        _playerController = GameObject.FindWithTag("GameController").GetComponent<PlayerController>();
        if (_playerController == null) Debug.LogWarning("No Player Controller Found In Scene!");

        LowestY += this.transform.position.y;
        HighestY += this.transform.position.y;
    }

    public void SetOffsetPositioning(Vector3 externalPosition)
    {
        this.transform.position = externalPosition + Offset;
    }

    private void FixedUpdate()
    {
        _targetPosition = _playerController.characterController[_playerController.currentPlayerIndex].transform.position + Offset;

        this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, Smoothing * Time.deltaTime);


        if (this.transform.position.y < LowestY)
        {
            this.transform.position = new Vector3(this.transform.position.x, LowestY, this.transform.position.z);
        }
        else if (this.transform.position.y > HighestY)
        {
            this.transform.position = new Vector3(this.transform.position.x, HighestY, this.transform.position.z);
        }

        /// DEBUGGING (JUST UNCOMMENT)
        /// Debug.Log(_offset + "\n" + _playerController.characterController[_playerController.currentPlayerIndex].transform.position);
    }
}
