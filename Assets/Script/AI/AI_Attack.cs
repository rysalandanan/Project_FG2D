using System.Collections;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{
    [Header("Attack Attributes")]
    [SerializeField] private float punchDuration;
    [SerializeField] private float cooldown;
    [SerializeField] private int enemyDamage;

    [Header("Raycast Settings")]
    [SerializeField] private float yOffSet;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask targetLayer;

    //Reference Scipt//
    private AI_DamageHandler aI_damageHandler;

    //Flag//
    private bool isAttacking = false;
    private bool isPunching = false;
    private bool canAttackAgain = true;

    private void Start()
    {
        aI_damageHandler = GetComponent<AI_DamageHandler>();
    }
    private enum AttackType
    {
       Punch
    }
    public void AI_Punch()
    {
        if(canAttackAgain)
        {
            StartCoroutine(AttackCoroutine(punchDuration, AttackType.Punch));
            PerformRaycast();
        }    
    }
    private IEnumerator AttackCoroutine(float duration, AttackType attackType)
    {
        isAttacking = true;
        canAttackAgain = false;
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
        yield return new WaitForSeconds(cooldown);
        canAttackAgain = true;
    }
    private void PerformRaycast()
    {
        var TranPos = transform.position;
        Vector2 raycastOrigin = new Vector2(TranPos.x, TranPos.y + yOffSet);
        float facingDirection = transform.localScale.x;
        Vector2 raycastDirection = new Vector2(facingDirection, 0f);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance,targetLayer);
        Debug.DrawLine(raycastOrigin, raycastOrigin + raycastDirection * raycastDistance, Color.red, 1f);
        if (hit.collider != null)
        {
            Fighter_DamageHandler fighter = hit.collider.gameObject.GetComponent<Fighter_DamageHandler>();
            if (fighter != null)
            {
                Debug.Log("Enemy hit you");
                fighter.TakeDamage(enemyDamage);
            }
        }
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
