using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;


public class RatEnemy : EnemyParentsClass
{

    public enum EnemyState
    {
        Patrolingstate,
        Attackingstate,
        Chasingstate
    }

    [SerializeField] private EnemyState currentState = EnemyState.Patrolingstate;

    

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
            case EnemyState.Chasingstate:
                StartChasing();
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
            }
        }
        else
        {
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, patrolStartPostion, movementSpeed),quaternion.identity);
            currentState = EnemyState.Attackingstate;
            if (transform.position == patrolStartPostion)
            {
                mustSwapPoints = false;
            }
        }
    }
    private void StartAttack()
    {
        StartCoroutine(AttackCorutine());
    }
    
    private void StartChasing()
    {
        throw new NotImplementedException();
    }
    #endregion


    IEnumerator AttackCorutine()
    {
        Debug.Log("In attack corutine");
        yield return new  WaitForSeconds(2);
        Debug.Log("First wait");
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(1);
        attackHitbox.enabled = false;
        Debug.Log("Last wait");
        currentState = EnemyState.Patrolingstate;
    }
}
