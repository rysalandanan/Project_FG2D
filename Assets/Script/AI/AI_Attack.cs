using System.Collections;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{
    private bool isAttacking;
    private bool isPunching;
    [SerializeField] private float PunchDuration;
    private enum AttackType
    {
       Punch
    }
    public void AI_Punch()
    {
        StartCoroutine(AttackCoroutine(PunchDuration,AttackType.Punch));
    }
    private IEnumerator AttackCoroutine(float duration, AttackType attackType)
    {
        isAttacking = true;
        switch (attackType)
        {
            case AttackType.Punch:
            isPunching =true;
            break;
        }
        yield return new WaitForSeconds(duration);

        switch (attackType)
        {
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
    public bool IsPunching()
    {
        return isPunching;
    }
}
