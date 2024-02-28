using UnityEngine;

public class AI_Animation : MonoBehaviour
{
    //Animator//
    private Animator _animator;
    private static readonly int State = Animator.StringToHash("state");
    private enum CharacterState {idle, walk, punch, hurt, dead}

    //Reference Script//
    private AI_Movement aI_Movement;
    private AI_Attack aI_Attack;
    private AI_DamageHandler aI_DamageHandler;

    void Start()
    {
        _animator =GetComponent<Animator>();
        aI_Movement = GetComponent<AI_Movement>();
        aI_Attack = GetComponent<AI_Attack>();
        aI_DamageHandler = GetComponent<AI_DamageHandler>();
    }

    void Update()
    {
        CharacterState  state;
        if(aI_DamageHandler.IsDead())
        {
            state = CharacterState.dead;
        }
        else if(aI_DamageHandler.IsHit())
        {
            state = CharacterState.hurt;
        }
        else if(aI_Attack.IsPunching())
        {
            state = CharacterState.punch;
        }
        else if(aI_Movement.IsWalking())
        {
            state = CharacterState.walk;
        }
        else
        {
            state = CharacterState.idle;
        }
        _animator.SetInteger(State,(int)state);
    }
}
