using UnityEngine;
using UnityEngine.Animations;

public class AI_Animation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int State = Animator.StringToHash("state");
    private enum CharacterState {idle, walk, punch, hurt}
    private AI_Movement aI_Movement;
    private AI_Attack aI_Attack;

    void Start()
    {
        _animator =GetComponent<Animator>();
        aI_Movement = GetComponent<AI_Movement>();
        aI_Attack = GetComponent<AI_Attack>();
    }

    void Update()
    {
        CharacterState  state;

        if(aI_Attack.IsPunching())
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
