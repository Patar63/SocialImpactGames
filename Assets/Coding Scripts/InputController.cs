using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    //reference for the player character
    public Sprite player;

    [Header("Debug")]
    public Vector2 _movement;
    public bool attackButton;

    private PlayerController playerInput;

    void Start()
    {
        player.Init();
    }

    void Awake()
    {
        playerInput = new PlayerController();

        //handle player movement
        playerInput.Player.Movement.performed += GetMovementInput;
        playerInput.Player.Movement.canceled += GetMovementInput;

        //handle player attack if needed
        playerInput.Player.Attack.performed += GetAttackInput;
        playerInput.Player.Attack.canceled += GetAttackInput;

        //handles pausing
        playerInput.Player.Pause.performed += _ => PauseGame();
        playerInput.UI.Start.performed += _ => StartGame();
    }

    //updates the player sprite data
    void Update()
    {
        player._movement = _movement;
        player.isAttacking = attackButton;
        player.Tick();
    }
    void OnEnable()
    {
        playerInput.UI.Enable();
    }
    void OnDisable()
    {
        playerInput.UI.Disable();
        playerInput.Player.Disable();
    }

    //actually run the game
    public void StartGame()
    {
        Debug.Log("Beginning the Game.");

        //swap player out of UI and into game controls
        playerInput.UI.Disable();
        playerInput.Player.Enable();
    }

    //get button input from the player
    private void GetMovementInput(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }
    private void GetAttackInput(InputAction.CallbackContext context)
    {
        attackButton = context.ReadValue<float>() > 0f;
    }
    private void PauseGame()
    {
        //temp code
        Debug.Log("game paused.");
        playerInput.Player.Disable();
        playerInput.UI.Enable();
    }

}
