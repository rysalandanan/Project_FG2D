using System.Collections;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{
    [Header("Attack Attributes")]
    [SerializeField] private float punchDuration;
    [SerializeField] private float cooldown;
    [SerializeField] private int enemyDamage;
    [SerializeField] private float attackTravelTime;

    [Header("Raycast Settings")]
    [SerializeField] private float yOffSet;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask targetLayer;

    //Reference Scipt//
    private Enemy_Health enemy_Health;

    //Flag//
    private bool isAttacking = false; //for movement//
    private bool isPunching = false; //for animation//
    private bool canAttackAgain = true;

    private void Start()
    {
        enemy_Health = GetComponent<Enemy_Health>();
    }
    private enum AttackType
    {
       Punch
    }
    public void AI_Punch()
    {
        if (canAttackAgain && !enemy_Health.IsHit())
        {
            StartCoroutine(AttackCoroutine(punchDuration, AttackType.Punch));
            StartCoroutine(PerformRaycast());
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
    private IEnumerator PerformRaycast()
    {
        var TranPos = transform.position;
        Vector2 raycastOrigin = new Vector2(TranPos.x, TranPos.y + yOffSet);
        float facingDirection = transform.localScale.x;
        Vector2 raycastDirection = new Vector2(facingDirection, 0f);
        yield return new WaitForSeconds(attackTravelTime);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, targetLayer);
        //Debug.DrawLine(raycastOrigin, raycastOrigin + raycastDirection * raycastDistance, Color.red, 1f);
        if (hit.collider != null)
        {
            Fighter_Health fighter_Health = hit.collider.gameObject.GetComponent<Fighter_Health>();
            Fighter_Attack fighter_Attack = hit.collider.gameObject.GetComponent<Fighter_Attack>();
            if (fighter_Attack.IsAttacking())
            {
                //PARRY LOGIC HERE//
                Debug.Log("PARRY");
            }
            else if (fighter_Health != null)
            {
                //Deliver Damage//
                fighter_Health.TakeDamage(enemyDamage);
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
