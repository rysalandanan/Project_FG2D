using System.Collections;
using UnityEngine;

public class Fighter_Attack : MonoBehaviour
{
    private bool isAttacking = false;
    private bool isJabbing = false;
    private bool isKicking = false;
    private bool isPunching = false;
    private bool isDiveKicking = false;

    private Fighter_Movement fighter_Movement;

    [SerializeField] private float JabDuration;
    [SerializeField] private float KickDuration;
    [SerializeField] private float PunchDuration;
    [SerializeField] private float DiveKickDuration;

    private void Start()
    {
        fighter_Movement = GetComponent<Fighter_Movement>();
    }
    private enum AttackType
    {
        Jab,
        Kick,
        DiveKick,
        Punch
    }

    private void Update()
    {
        if (!isAttacking)
        {
            HandleAttackInput();
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(AttackRoutine(JabDuration, AttackType.Jab));
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(AttackRoutine(KickDuration, AttackType.Kick));
        }
        else if (Input.GetKeyDown(KeyCode.P) && fighter_Movement.IsWalking())
        {
            StartCoroutine(AttackRoutine(DiveKickDuration, AttackType.DiveKick));
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(AttackRoutine(PunchDuration, AttackType.Punch));
        }
    }

    private IEnumerator AttackRoutine(float duration, AttackType attackType)
    {
        isAttacking = true;

        switch (attackType)
        {
            case AttackType.Jab:
                isJabbing = true;
                break;
            case AttackType.Kick:
                isKicking = true;
                break;
            case AttackType.DiveKick:
                isDiveKicking = true;
                break;
            case AttackType.Punch:
                isPunching = true;
                break;
        }

        yield return new WaitForSeconds(duration);

        switch (attackType)
        {
            case AttackType.Jab:
                isJabbing = false;
                break;
            case AttackType.Kick:
                isKicking = false;
                break;
            case AttackType.DiveKick:
                isDiveKicking = false;
                break;
            case AttackType.Punch:
                isPunching = false;
                break;
        }

        isAttacking = false;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsJabbing()
    {
        return isJabbing;
    }

    public bool IsKicking()
    {
        return isKicking;
    }

    public bool IsPunching()
    {
        return isPunching;
    }

    public bool IsDiveKicking()
    {
        return isDiveKicking;
    }
}
