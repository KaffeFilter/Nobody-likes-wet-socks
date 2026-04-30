using UnityEngine;

public class EnvirotmentHazard : MonoBehaviour
{
    private DamageInterface idamageInterface;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            idamageInterface = other.gameObject.GetComponent<DamageInterface>();
            Debug.Log("PlayerHit");
            idamageInterface.TakeDamage(1000);
        }
    }
}
