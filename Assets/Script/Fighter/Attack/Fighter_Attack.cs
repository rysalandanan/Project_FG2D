using System.Collections;
using UnityEngine;

public class Fighter_Attack : MonoBehaviour
{
    [Header("Attack Attributes")]
    [SerializeField] private float jabDuration;
    [SerializeField] private float kickDuration;
    [SerializeField] private float punchDuration;
    [SerializeField] private int fighterDamage;

    [Header("Raycast Settings")]
    [SerializeField] private float yOffSet;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask targetLayer;

    //reference script//
    private Fighter_Health fighter_Health;

    //Flag//
    private bool isAttacking = false;
    private bool isJabbing = false;
    private bool isKicking = false;
    private bool isPunching = false;

    private enum AttackType
    {
        Jab,
        Kick,
        Punch
    }
    private void Start()
    {
        fighter_Health = GetComponent<Fighter_Health>();
    }

    private void Update()
    {
        if (!isAttacking && !fighter_Health.IsHit())
        {
            HandleAttackInput();
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(AttackRoutine(jabDuration, AttackType.Jab));
            PerformRaycast();
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(AttackRoutine(kickDuration, AttackType.Kick));
            PerformRaycast();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(AttackRoutine(punchDuration, AttackType.Punch));
            PerformRaycast();
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
            case AttackType.Punch:
                isPunching = false;
                break;
        }
        isAttacking = false;
    }
    private void PerformRaycast()
    {
        var TranPos = transform.position;
        Vector2 raycastOrigin = new Vector2(TranPos.x, TranPos.y + yOffSet);
        float facingDirection = transform.localScale.x;
        Vector2 raycastDirection = new Vector2(facingDirection, 0f);
        
        RaycastHit2D hit  = Physics2D.Raycast(raycastOrigin,raycastDirection,raycastDistance, targetLayer);
        //Debug.DrawLine(raycastOrigin, raycastOrigin + raycastDirection * raycastDistance, Color.red, 1f);
        if (hit.collider !=null)
        {
           Enemy_Health enemy_Health = hit.collider.gameObject.GetComponent<Enemy_Health>();
           if(enemy_Health != null)
           {
                Debug.Log("You hit an enemy");
                enemy_Health.TakeDamage(fighterDamage);
           }
        }
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
}
