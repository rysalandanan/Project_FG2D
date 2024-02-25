using UnityEngine;

public abstract class BaseDamageHandler : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth = 100;
    protected int currentHealth;
    private void Start()
    {
        currentHealth  = maxHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        HandleDamagAftermath();
    }
    protected abstract void HandleDamagAftermath();
}
