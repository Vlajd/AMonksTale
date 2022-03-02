using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // INPUT //
    private InputControls _inputControls;
    
    private float _horizontalMovement;
    private float _verticalMovement;
    private bool _sprint = false;
    // INPUT //

    private characterController[] _regularPlayer;

    private int _currentPlayerIndex = -1;

    private void Awake()
    {
        // Get All Character Controllers
        GameObject[] _character = GameObject.FindGameObjectsWithTag("Player");
        _regularPlayer = new characterController[_character.Length];
        for (int i = 0; i < _regularPlayer.Length; i++)
        {
            characterController currentRegularPlayer = _character[i].GetComponent<characterController>();
            if (currentRegularPlayer != null)
            {
                _regularPlayer[i] = currentRegularPlayer;

                if (currentRegularPlayer.MainPlayer)
                {
                    if (_currentPlayerIndex != -1) Debug.LogWarning("Multiple Main Character!");
                    _currentPlayerIndex = i;
                }
            }
            else
            {
                ghostPlayer currentGhostPlayer = _character[i].GetComponent<ghostPlayer>();
            }
        }
        if (_currentPlayerIndex == -1)
        {
            Debug.LogWarning("No Main Character!");
            _currentPlayerIndex = 0;
        }


        // Create Input Controller Instance
        _inputControls = new InputControls();

        _inputControls.Player.Horizontal.performed += ctx => {
            _horizontalMovement = ctx.ReadValue<float>();
            _regularPlayer[_currentPlayerIndex].SwitchDir(_horizontalMovement);
        };
        _inputControls.Player.Horizontal.canceled += _ => {
            _horizontalMovement = 0.0f;
            // Makes for nicer sprinting controlls
            // **The If Statement can be improved for performance**
            if (!_inputControls.Player.Horizontal.IsPressed() && !_inputControls.Player.Vertical.IsPressed()) _sprint = false;
        };
        _inputControls.Player.Sprint.performed += _ => _sprint = true;
    }

    private void OnEnable() => _inputControls.Enable();
    private void OnDisable() => _inputControls.Disable();

    private void FixedUpdate()
    {
        _regularPlayer[_currentPlayerIndex].MoveHorizontally(_horizontalMovement, _sprint);
        if (_inputControls.Player.Jump.IsPressed()) _regularPlayer[_currentPlayerIndex].Jump();
    }
}
