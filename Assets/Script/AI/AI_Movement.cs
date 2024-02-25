using UnityEngine;

public class AI_Movement : BaseMovement
{
    private Transform Fighter;
    private AI_Attack aI_Attack;
    [SerializeField] private float StoppingDistance;
    private void Start()
    {
        Fighter = GameObject.FindGameObjectWithTag("Player").transform;
        aI_Attack = GetComponent<AI_Attack>();
    }

   private void Update()
   {
        if(!aI_Attack.IsAttacking())
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
        Vector2 directionToPlayer = (Fighter.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position,Fighter.position);
        if(distanceToPlayer <= StoppingDistance && !aI_Attack.IsAttacking())
        {
            aI_Attack.AI_Punch();
            return Vector2.zero;
        }
        return directionToPlayer;
    }
}
