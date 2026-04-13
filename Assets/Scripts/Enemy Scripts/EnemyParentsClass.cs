using UnityEngine;

public abstract class EnemyParentsClass : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Collider2D attackHitbox;
}
