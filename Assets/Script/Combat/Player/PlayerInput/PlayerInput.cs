using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovementInput movementInput { get; set; }
    public PlayerMovementInput.PlayerActions playerActions { get; set; }

    private void Awake()
    {
        movementInput = new PlayerMovementInput();
        playerActions = movementInput.Player;
    }
   
    private void OnEnable()
    {
        movementInput.Enable();
    }
    private void OnDisable()
    {
        movementInput.Disable();
    }
}
