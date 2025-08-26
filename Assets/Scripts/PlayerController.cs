using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2d;

    #region PlayerStats
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    #endregion
    
    
    #region InputActionDefinitions
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference changeGravity;
    [SerializeField] private InputActionReference jump;
    #endregion

    private float inputdirektion;

    #region GeneralSetUP

    private void OnEnable()
    {
        movement.action.Enable();
        attack.action.Enable();
        changeGravity.action.Enable();
        jump.action.Enable();
        movement.action.performed += Walking;
        movement.action.canceled += StopMovement;
        jump.action.performed += JumpAction;
        //Getting Needed Components

    }

    #endregion

    #region Movment/Controles
    private void Walking(InputAction.CallbackContext ctx)
    {
        inputdirektion = ctx.ReadValue<Vector2>().x;
    }

    private void StopMovement(InputAction.CallbackContext ctx)
    {
        inputdirektion = 0;
    }

    private void JumpAction(InputAction.CallbackContext ctx)
    {
        Debug.Log("I am jumping");
        rigidbody2d.AddForce(new Vector2(rigidbody2d.linearVelocity.x,jumpHeight));
    }
    
    #endregion

    private void Update()
    {
        rigidbody2d.linearVelocity = new Vector2(inputdirektion * speed ,rigidbody2d.linearVelocity.y) ;
    }
}
