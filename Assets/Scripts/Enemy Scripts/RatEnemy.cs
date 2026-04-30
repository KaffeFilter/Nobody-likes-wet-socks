using System.Collections;
using Unity.Mathematics;
using UnityEngine;



public class RatEnemy : EnemyParentsClass
{
    private bool attackHasAlreadyStartet;
    [SerializeField] Animator animator;
    public enum EnemyState
    {
        Patrolingstate,
        Attackingstate,
        
    }

    [SerializeField] public EnemyState currentState = EnemyState.Patrolingstate;


    private void Awake()
    {
       animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    { 
        switch (currentState)
         {
            case EnemyState.Patrolingstate:
                StartPatrol();
                break;
            case EnemyState.Attackingstate:
                StartAttack();
                break;
         }
    }

    


    #region EnemyControls internal

    private void StartPatrol()
    {
        if (!mustSwapPoints)
        {
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, patrolEndPosition, movementSpeed),quaternion.identity);
            if (transform.position == patrolEndPosition)
            {
                mustSwapPoints = true;
                animator.SetBool("LookingLeft" , true);
            }
        }
        else
        {
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, patrolStartPostion, movementSpeed),quaternion.identity);
            
            if (transform.position == patrolStartPostion)
            {
                mustSwapPoints = false;
                animator.SetBool("LookingLeft" , false);
            }
        }
    }
    private void StartAttack()
    {
        if (!attackHasAlreadyStartet)
        {
            StartCoroutine(AttackCorutine());
        }
    }
    #endregion


    IEnumerator AttackCorutine()
    {
      
            attackHasAlreadyStartet = true;
            animator.SetBool("IsAttacking", true);
            Debug.Log("In attack corutine");
            yield return new  WaitForSeconds(1f);
            Debug.Log("First wait");
            attackHitbox.GetComponent<EnemyAttack>().AttackStart();
            yield return new WaitForSeconds(0.2f);
            attackHitbox.GetComponent<EnemyAttack>().AttackEnd();

            Debug.Log("Last wait");
            currentState = EnemyState.Patrolingstate;
            attackHasAlreadyStartet = false;
            animator.SetBool("IsAttacking", false);
    }
}
