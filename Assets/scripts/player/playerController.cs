using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject GhostPrefab;

    // INPUT //
    private InputControls _inputControls;
    
    private float _horizontalMovement;
    private float _verticalMovement;
    private bool _sprint = false;
    // INPUT //

    [HideInInspector] public CharacterController[] _characterController;

    private int _ghostPlayerIndex;
    [HideInInspector] public int _currentPlayerIndex = -1;
    private bool _isGhostActive = false;

    private void Awake()
    {
        
        /// *******************************************************************************************************************************************************************
        /// *******Character Setup*********************************************************************************************************************************************
        /// ******************************************************************************************************************************************************************* 
        // Tagged with "Player" will get checked to be added to the _characterController array
        GameObject[] _character = GameObject.FindGameObjectsWithTag("Player");

        // **CAN BE IMPROVED TO USE LESS MEMORY IN CASE ONE OF THE OBJECTS TAGGED WITH "Player" IS NOT OF TYPE characterController**
        // The + 1 is for the ghostPlayer, as it shouldn't be already created when loading the scene
        _characterController = new CharacterController[_character.Length + 1];

        for (int i = 0; i < _character.Length; i++)
        {
            CharacterController currentCharacter = _character[i].GetComponent<RegularPlayer>();

            if (currentCharacter != null)
            {
                _characterController[i] = currentCharacter;

                if (currentCharacter.MainPlayer)
                {
                    if (_currentPlayerIndex != -1) Debug.LogWarning("Multiple Main Character!");
                    _currentPlayerIndex = i;
                }
            }
        }

        if (_currentPlayerIndex == -1)
        {
            Debug.LogWarning("No Main Character!");
            _currentPlayerIndex = 0;
        }

        /// *************************************************************************************
        /// *******SETUP GHOST*******************************************************************
        if (GhostPrefab == null)
        {
            Debug.LogWarning("Ghost Prefab Is Not Set In PlayerController!");
        }
        else
        {
            // - 1 because array starts at index 0, not 1
            _ghostPlayerIndex = _characterController.Length - 1;
            GhostPlayer currentGhostPlayer = GhostPrefab.GetComponent<GhostPlayer>();

            if (currentGhostPlayer == null)
            {
                Debug.LogWarning("Prefab in PlayerController GhostPrefab is not of type ghostPlayer!");
            }
            else
            {
                _characterController[_ghostPlayerIndex] = currentGhostPlayer;
            }
        }



        /// *******************************************************************************************************************************************************************
        /// *******Input System************************************************************************************************************************************************
        /// *******************************************************************************************************************************************************************
        // Create Input Controller Instance
        _inputControls = new InputControls();

        // Horizontal Keymapping
        _inputControls.Player.Horizontal.performed += ctx =>
        {
            _horizontalMovement = ctx.ReadValue<float>();
            _characterController[_currentPlayerIndex].SwitchDir(_horizontalMovement);
        };
        _inputControls.Player.Horizontal.canceled += _ =>
        {
            _horizontalMovement = 0.0f;
            // Makes for nicer sprinting controlls
            // **The If Statement can be improved for performance**
            if (!_inputControls.Player.Horizontal.IsPressed() && !_inputControls.Player.Vertical.IsPressed()) _sprint = false;
        };

        // Vertical Keymapping
        _inputControls.Player.Vertical.performed += ctx =>
        {
            _verticalMovement = ctx.ReadValue<float>();
        };
        _inputControls.Player.Vertical.canceled += _ =>
        {
            _verticalMovement = 0.0f;
            // Makes for nicer sprinting controlls
            // **The If Statement can be improved for performance**
            if (!_inputControls.Player.Horizontal.IsPressed() && !_inputControls.Player.Vertical.IsPressed()) _sprint = false;
        };

        // Possess Keymapping
        _inputControls.Player.Possess.started += _ => ToggleGhost();
        
        _inputControls.Player.Sprint.performed += _ => _sprint = true;

    }

    /// Boilerplate Code for the Input System
    private void OnEnable() => _inputControls.Enable();
    private void OnDisable() => _inputControls.Disable();


    private void ToggleGhost()
    {
        if (!_isGhostActive)
        {
            GameObject tempGhostGameObject = Instantiate(GhostPrefab, _characterController[_currentPlayerIndex].transform.position, Quaternion.identity);
            _currentPlayerIndex = _ghostPlayerIndex;
            _characterController[_currentPlayerIndex] = tempGhostGameObject.GetComponent<GhostPlayer>();
            _isGhostActive = true;
            return;
        }

        for (int i = 0; i < _ghostPlayerIndex; i++)
        {
            if (Vector2.Distance(_characterController[_ghostPlayerIndex].transform.position, _characterController[i].transform.position) < _characterController[_ghostPlayerIndex].PossessRadius)
            {
                _currentPlayerIndex = i;
                Destroy(_characterController[_ghostPlayerIndex].gameObject);
                _isGhostActive = false;
                break;
            };
        }
    }

    private void FixedUpdate()
    {
        _characterController[_currentPlayerIndex].MoveHorizontally(_horizontalMovement, _sprint);
        _characterController[_currentPlayerIndex].MoveVertically(_verticalMovement, _sprint);
        if (_inputControls.Player.Jump.IsPressed()) _characterController[_currentPlayerIndex].Jump();
    }

}
