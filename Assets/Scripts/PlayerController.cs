
using System;
using System.Collections;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour, DamageInterface 
{
    [SerializeField] private Rigidbody2D rigidbody2d;
    private Animator animator;
    [SerializeField] private Canvas canvas;
    private TextMeshProUGUI textMeshProUGUI;

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
        changeGravity.action.Enable();
        jump.action.Enable();
        movement.action.performed += Walking;
        movement.action.canceled += StopMovement;
        jump.action.performed += JumpAction;
        currentHealth = maxHealth;
        //Getting Needed Components
        textMeshProUGUI =  (TextMeshProUGUI)canvas.GetComponentInChildren(typeof(TextMeshProUGUI));
        textMeshProUGUI.text = currentHealth.ToString() + " HP";
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
        textMeshProUGUI.text = currentHealth.ToString() + " HP";
        if (currentHealth <= 0)
        {
            movement.action.Disable();
            changeGravity.action.Disable();
            jump.action.Disable();
            GameMainManager.Instance.Gameover();
        }
    }

    #endregion
    private void Update()
    {
        rigidbody2d.linearVelocity = new Vector2(inputdirektion * speed ,rigidbody2d.linearVelocity.y);
        if (rigidbody2d.linearVelocity.x == 0)
        {
            animator.SetBool("IsRunning", false);
        }
        if (rigidbody2d.linearVelocity.x != 0)
        {
            animator.SetBool("IsRunning", true);
            if (rigidbody2d.linearVelocity.x <= 0.1)
            {
                animator.SetBool("LookingLeft", true);
            }
            else
            {
                animator.SetBool("LookingLeft", false);
            }
        }

        if (rigidbody2d.gravityScale == 2f)
        {
            animator.SetBool("GravityInverted",false);
        }
        else
        {
            animator.SetBool("GravityInverted",true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!(isJumping & other.gameObject.CompareTag("Floor"))) return;
        isJumping = false;
        hasChangedGravity = false;
        rigidbody2d.linearDamping = 0f;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
         {
             if (other.gameObject.CompareTag("Floor"))
             {
                 isJumping = true;
             }
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
