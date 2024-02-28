using UnityEngine;

public class AI_Movement : BaseMovement
{
    private Transform fighter;
    private AI_Attack aI_Attack;
    private AI_DamageHandler aI_DamageHandler;
    [SerializeField] private float stoppingDistance;
    private void Start()
    {
        fighter = GameObject.FindGameObjectWithTag("Player").transform;
        aI_Attack = GetComponent<AI_Attack>();
        aI_DamageHandler = GetComponent<AI_DamageHandler>();
    }

   private void Update()
   {
        if(!aI_Attack.IsAttacking()&& !aI_DamageHandler.IsHit()&& !aI_DamageHandler.IsDead())
        {
            Vector2 AiMovementInput = CalculateMovementInput();
            HandleMovement(AiMovementInput);
            SpriteFlip(AiMovementInput);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
   }

   protected override Vector2 CalculateMovementInput()
    {
        Vector2 directionToPlayer = (fighter.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position,fighter.position);
        if(distanceToPlayer <= stoppingDistance && !aI_Attack.IsAttacking())
        {
            aI_Attack.AI_Punch();
            return Vector2.zero;
        }
        return directionToPlayer;
   }
}
