using UnityEngine;

public class Fighter_DamageHandler : BaseDamageHandler
{
    private const int damageAmount = 10;
    private  void Update()
    {
        if(Input.GetKeyDown (KeyCode.G))
        {
            TakeDamage(damageAmount);
        }
    }
    protected override void HandleDamagAftermath()
    {
        Debug.Log("Fighter");
        Debug.Log(currentHealth);
    }
}
