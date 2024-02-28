using UnityEngine;

public class Fighter_Movement : BaseMovement
{
    //Reference Scripts//
    private Fighter_Attack fighter_Attack;
    private Fighter_DamageHandler fighter_DamageHandler;
    private void Start()
    {
        fighter_Attack = GetComponent<Fighter_Attack>();
        fighter_DamageHandler = GetComponent<Fighter_DamageHandler>();
    }
    private void Update()
    {
        if(!fighter_Attack.IsAttacking() && !fighter_DamageHandler.IsHit())
        {
            Vector2 playerMovementInput = CalculateMovementInput();
            HandleMovement(playerMovementInput);
            SpriteFlip(playerMovementInput);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    protected override Vector2 CalculateMovementInput()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical"); 

        return new Vector2(xAxis,yAxis);
    }
}