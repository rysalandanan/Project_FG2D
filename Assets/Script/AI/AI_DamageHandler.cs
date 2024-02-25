using UnityEngine;

public class AI_DamageHandler : BaseDamageHandler
{
    private const int damageAmount = 15;
    // Start is called before the first frame update
    private  void Update()
    {
        if(Input.GetKeyDown (KeyCode.H))
        {
            TakeDamage(damageAmount);
        }
    }
    protected override void HandleDamagAftermath()
    {
        Debug.Log("AI");
        Debug.Log(currentHealth);
    }
}