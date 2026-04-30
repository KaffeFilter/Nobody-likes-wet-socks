using System;
using UnityEngine;

public class EnemyAttackDetectiion : MonoBehaviour
{
    [SerializeField] private RatEnemy mainEnemyAI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainEnemyAI.currentState = RatEnemy.EnemyState.Attackingstate;
        }
    }
}
