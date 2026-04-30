using UnityEngine;
using UnityEngine.Rendering;

public abstract class EnemyParentsClass : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] public float movementSpeed;
    [SerializeField] public Vector3 patrolStartPostion;
    [SerializeField] public Vector3 patrolEndPosition;
    public bool mustSwapPoints;
    [SerializeField] public MonoBehaviour attackHitbox;
    
}
