
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour, DamageInterface 
{
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private Animator animator;

    #region PlayerStats
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float maxHealth;
    private float currentHealth;
    #endregion
    
    
    #region InputActionDefinitions
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference changeGravity;
    [SerializeField] private InputActionReference jump;
    #endregion

    #region Variables

    private bool isJumping;
    private bool hasChangedGravity;
    private float inputdirektion;


    #endregion


    #region GeneralSetUP

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        movement.action.Enable();
        attack.action.Enable();
        changeGravity.action.Enable();
        jump.action.Enable();
        movement.action.performed += Walking;
        movement.action.canceled += StopMovement;
        jump.action.performed += JumpAction;
        currentHealth = maxHealth;
        //Getting Needed Components

    }

    #endregion

    #region Movment/Controles
    private void Walking(InputAction.CallbackContext ctx)
    {
        inputdirektion = ctx.ReadValue<Vector2>().x;
        if (inputdirektion != 0)
        {
            animator.SetBool("IsRunning", true);
            if (inputdirektion <= 0.1)
            {
                animator.SetBool("LookingLeft", true);
            }
            else
            {
                animator.SetBool("LookingLeft", false);
            }
        }
      
    }

    private void StopMovement(InputAction.CallbackContext ctx)
    {
        inputdirektion = 0;
        animator.SetBool("IsRunning", false);
    }

    private void JumpAction(InputAction.CallbackContext ctx)
    {
        if (!isJumping)
        {
            rigidbody2d.AddForce(new Vector2(rigidbody2d.linearVelocity.x, (jumpHeight / 2) * rigidbody2d.gravityScale));
            isJumping = true;
            StartCoroutine(AddAirDrag());

        }
        else if(!hasChangedGravity)
        {
            rigidbody2d.gravityScale *= -1;
            hasChangedGravity = true;
        }
    }
    
    #endregion


    #region StatusChanges

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameMainManager.Instance.Gameover();
        }
    }

    #endregion
    private void Update()
    {
        rigidbody2d.linearVelocity = new Vector2(inputdirektion * speed ,rigidbody2d.linearVelocity.y);
        animator.SetFloat("RunningInput", rigidbody2d.linearVelocity.x);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
            if (isJumping & other.gameObject.CompareTag("Floor"))
            {
                isJumping = false;
                hasChangedGravity = false;
                rigidbody2d.linearDamping = 0f;
            }
    }

    #region Corutines

    IEnumerator AddAirDrag()
    {
        yield return new WaitForNextFrameUnit();
        rigidbody2d.linearDamping = 3f;
    }

    #endregion
}
