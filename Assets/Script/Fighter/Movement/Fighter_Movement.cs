using UnityEngine;

public class Fighter_Movement : BaseMovement
{
    private Fighter_Attack fighter_Attack;
    private void Start()
    {
        fighter_Attack = GetComponent<Fighter_Attack>();
    }
    private void Update()
    {
        if(!fighter_Attack.IsAttacking())
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