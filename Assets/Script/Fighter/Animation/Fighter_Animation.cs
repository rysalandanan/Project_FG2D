using UnityEngine;

public class Fighter_Animation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int State = Animator.StringToHash("state");
    //reference scipts//
    private Fighter_Movement fighter_Movement;
    private Fighter_Attack fighter_Attack;
    private Fighter_DamageHandler fighter_DamageHandler;

    private enum CharacterState
    {
        idle,
        walk,
        jab,
        kick,
        punch,
        hurt
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        fighter_Movement = GetComponent<Fighter_Movement>();
        fighter_Attack = GetComponent<Fighter_Attack>();
        fighter_DamageHandler = GetComponent<Fighter_DamageHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterState state = DetermineCharacterState();
        _animator.SetInteger(State, (int)state);
    }
    private CharacterState DetermineCharacterState()
    {
        if (fighter_DamageHandler.IsHit())
        {
            return CharacterState.hurt;
        }
        if (fighter_Movement.IsWalking())
        {
            return CharacterState.walk;
        }
        if (fighter_Attack.IsJabbing())
        {
            return CharacterState.jab;
        }
        if (fighter_Attack.IsKicking())
        {
            return CharacterState.kick;
        }
        if (fighter_Attack.IsPunching())
        {
            return CharacterState.punch;
        }
        return  CharacterState.idle;
    }
}
