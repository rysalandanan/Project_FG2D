using System.Collections;
using UnityEngine;

public class Fighter_Attack : MonoBehaviour
{
    [Header("Attack Attributes")]
    [SerializeField] private float jabDuration;
    [SerializeField] private float kickDuration;
    [SerializeField] private float punchDuration;
    [SerializeField] private int fighterDamage;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float attackTravelTime;

    [Header("Raycast Settings")]
    [SerializeField] private float yOffSet;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] private HitTextPool hitTextPool;
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
            Debug.Log("JAB");
            StartCoroutine(AttackRoutine(jabDuration, AttackType.Jab));
            StartCoroutine(PerformRaycast());
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Kick");
            StartCoroutine(AttackRoutine(kickDuration, AttackType.Kick));
            StartCoroutine(PerformRaycast());
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("punch");
            StartCoroutine(AttackRoutine(punchDuration, AttackType.Punch));
            StartCoroutine(PerformRaycast());
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
            Enemy_Health enemy_Health = hit.collider.gameObject.GetComponent<Enemy_Health>();
            AI_Attack enemy_Attack = hit.collider.gameObject.GetComponent<AI_Attack>();
            if (enemy_Attack.IsAttacking())
            {
                //PARRY LOGIC HERE//
                Debug.Log("PARRY");
            }
            else if (enemy_Health != null)
            {
                //Deliver Damage//
                enemy_Health.TakeDamage(fighterDamage);
                StartCoroutine(ShowHitText(hit.collider.transform.position));
            }
        }
    }
    private IEnumerator ShowHitText(Vector2 enemyPosition)
    {
        GameObject hitText = HitTextPool.Instance.GetPooledObject();
        float xOffset = 1.0f;
        float yOffset = 1.0f; 
        Vector2 hitTextPosition = new Vector2(enemyPosition.x  + xOffset, enemyPosition.y + yOffset);
        hitText.transform.position = hitTextPosition;
        hitText.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        hitTextPool.DeactivateAllHitText();
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
