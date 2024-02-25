using UnityEngine;

public class Fighter_Animation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int State = Animator.StringToHash("state");
    private enum CharacterState {idle, walk, jab, kick, punch, dive_kick}
    private Fighter_Movement fighter_Movement;
    private Fighter_Attack fighter_Attack;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        fighter_Movement = GetComponent<Fighter_Movement>();
        fighter_Attack = GetComponent<Fighter_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterState  state;

        if(fighter_Movement.IsWalking())
        {
            state = CharacterState.walk;
        }
        else if(fighter_Attack.IsJabbing())
        {
            state = CharacterState.jab;
        }
        else if(fighter_Attack.IsKicking())
        {
            state = CharacterState.kick;
        }
        else if(fighter_Attack.IsPunching())
        {
            state = CharacterState.punch;
        }
        else if(fighter_Attack.IsDiveKicking())
        {
            state = CharacterState.dive_kick;
        }
        else
        {
            state = CharacterState.idle;
        }
        _animator.SetInteger(State,(int)state);
    }
}
