using System;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;
    private DamageInterface idamageInterface;
    public void AttackStart()
    {
        hitbox.enabled = true;
    }

    public void AttackEnd()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            idamageInterface = other.gameObject.GetComponent<DamageInterface>();
            Debug.Log("PlayerHit");
            idamageInterface.TakeDamage(1);
        }
    }
}
