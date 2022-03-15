using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject GhostPrefab;
    [SerializeField] private GameObject MainCanvas;
    [SerializeField] private string CurrentSceneName = "MainScene";


    // INPUT //
    private InputControls _inputControls;
    
    private float _horizontalMovement;
    private float _verticalMovement;
    private bool _sprint = false;
    // INPUT //


    private OverlayController _mainCanvasOverlayController;
    private bool _isGamePaused = false;

    [HideInInspector] public Vector2 currentCheckpoint = new Vector2(-1.11f, -1.11f);

    [HideInInspector] public PlayerCharacterController[] characterController;
    [HideInInspector] public PlayerCamera playerCamera;
    [HideInInspector] public int currentPlayerIndex = -1;
    private int _ghostPlayerIndex;
    private bool _isGhostActive = false;

    private void Awake()
    {
        // InitReferences();
        InitInput();
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == CurrentSceneName)
        {
            InitReferences();
            Time.timeScale = 1.0f;
        }
        else Destroy(this.gameObject);
    }


    private void OnEnable()
    {
        _inputControls.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        _inputControls.Disable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void ToggleGhost()
    {
        if (!_isGhostActive)
        {
            // create instance of ghost and summon that bad boy
            GameObject tempGhostGameObject = Instantiate(GhostPrefab, characterController[currentPlayerIndex].transform.position, Quaternion.identity);
            currentPlayerIndex = _ghostPlayerIndex;
            characterController[currentPlayerIndex] = tempGhostGameObject.GetComponent<GhostPlayer>();
            _isGhostActive = true;
            return;
        }

        // everything before the _ghostPlayerIndex in the characterController[] get checked for the 
        for (int i = 0; i < _ghostPlayerIndex; i++)
        {
            if (Vector2.Distance(characterController[_ghostPlayerIndex].transform.position, characterController[i].transform.position) < characterController[_ghostPlayerIndex].PossessRadius)
            {
                currentPlayerIndex = i;
                Destroy(characterController[_ghostPlayerIndex].gameObject);
                _isGhostActive = false;
                break;
            };
        }
    }


    // toggles the pausemenu on Esc or when resume gets pressed
    public void TogglePausemenu()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused) _mainCanvasOverlayController.InitPausemenu();
        else _mainCanvasOverlayController.ResumePausemenu();
    }



    private void FixedUpdate()
    {
        // in case the shift key is still pressed after ToggleGhost() got called
        _sprint = _sprint || _inputControls.Player.Sprint.IsPressed();

        // movement
        characterController[ currentPlayerIndex].MoveHorizontally(_horizontalMovement, _sprint);
        characterController[ currentPlayerIndex].MoveVertically(_verticalMovement, _sprint);
        if (_inputControls.Player.Jump.IsPressed()) characterController[ currentPlayerIndex].Jump();
    }










////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////// SETUP STUFF ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void InitReferences()
    {
        /// *******************************************************************************************************************************************************************
        /// *******RESET VARIABLES*********************************************************************************************************************************************
        /// *******************************************************************************************************************************************************************
        _sprint = false;
        _isGamePaused = false;
        // currentCheckpoint has to stay at changed val
        currentPlayerIndex = -1;
        _isGhostActive = false;


        /// *******************************************************************************************************************************************************************
        /// *******CAMERA SETUP************************************************************************************************************************************************
        /// *******************************************************************************************************************************************************************
        GameObject tempPlayerCamera = GameObject.FindGameObjectWithTag("Camera");
        if (tempPlayerCamera != null)
        {
            playerCamera = tempPlayerCamera.GetComponent<PlayerCamera>();
            // deactivate camera renderer
            if (playerCamera.CameraRenderer != null)
            {
                playerCamera.CameraRenderer.enabled = false;
            }
            else Debug.LogWarning("PlayerCamera CameraRenderer Not Jet Referenced!");
        }
        else Debug.LogWarning("No PlayerCamera Found");
        


        /// *******************************************************************************************************************************************************************
        /// *******Character Setup*********************************************************************************************************************************************
        /// ******************************************************************************************************************************************************************* 
        // Tagged with "Player" will get checked to be added to the characterController array
        GameObject[] _character = GameObject.FindGameObjectsWithTag("Player");

        // **CAN BE IMPROVED TO USE LESS MEMORY IN CASE ONE OF THE OBJECTS TAGGED WITH "Player" IS NOT OF TYPE characterController**
        // The + 1 is for the ghostPlayer, as it shouldn't be already created when loading the scene
        characterController = new PlayerCharacterController[_character.Length + 1];

        for (int i = 0; i < _character.Length; i++)
        {
            PlayerCharacterController currentCharacter = _character[i].GetComponent<RegularPlayer>();

            if (currentCharacter != null)
            {
                characterController[i] = currentCharacter;

                if (currentCharacter.MainPlayer)
                {
                    if (currentPlayerIndex != -1) Debug.LogWarning("Multiple Main Character!");

                    currentPlayerIndex = i;
                    if(currentCheckpoint == new Vector2(-1.11f, -1.11f))
                    {
                        currentCheckpoint = characterController[currentPlayerIndex].rigidBody.position;
                    }

                    // setup positions
                    characterController[currentPlayerIndex].transform.position = new Vector3(currentCheckpoint.x, currentCheckpoint.y, 0.0f);
                    playerCamera.SetOffsetPositioning(new Vector3(currentCheckpoint.x, currentCheckpoint.y, -1.0f));
                }
            }
            else Debug.Log("Character Missing characterController or wrongly tagged GameObject with \"Player\"");
        }

        if (currentPlayerIndex == -1)
        {
            Debug.LogWarning("No Main Character!");

            currentPlayerIndex = 0;
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
            _ghostPlayerIndex = characterController.Length - 1;
            GhostPlayer currentGhostPlayer = GhostPrefab.GetComponent<GhostPlayer>();

            if (currentGhostPlayer == null) Debug.LogWarning("Prefab in PlayerController GhostPrefab is not of type ghostPlayer!");
            else 
                characterController[_ghostPlayerIndex] = currentGhostPlayer;
        }



        /// *******************************************************************************************
        /// ******* SETUP CANVAS **********************************************************************
        GameObject tempMainCanvas = Instantiate(MainCanvas, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        _mainCanvasOverlayController = tempMainCanvas.GetComponent<OverlayController>();



        // activate camera renderer again
        playerCamera.CameraRenderer.enabled = true;
    }

    void InitInput()
    {
        /// *******************************************************************************************************************************************************************
        /// *******Input System************************************************************************************************************************************************
        /// *******************************************************************************************************************************************************************
        // Create Input Controller Instance
        _inputControls = new InputControls();

        // Horizontal Keymapping
        _inputControls.Player.Horizontal.performed += ctx =>
        {
            _horizontalMovement = ctx.ReadValue<float>();
            if(!_isGamePaused) characterController[currentPlayerIndex].SwitchDir(_horizontalMovement);
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

        // Sprint Keymapping
        _inputControls.Player.Sprint.performed += _ => _sprint = true;

        // Esc Keymapping
        _inputControls.Player.Esc.started += _ => TogglePausemenu();
    }
}
