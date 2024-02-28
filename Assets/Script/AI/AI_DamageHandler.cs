using System.Collections;
using UnityEngine;

public class AI_DamageHandler : MonoBehaviour, IDamageable
{
    [Header("General Attributes")]
    //private int damageAmount;
    [SerializeField] private int maxHealth;
    [SerializeField] private float stunDuration;

    //Reference Script//

    [SerializeField]private GameScore gameScore;

    //Flag//
    private bool isHit = false;
    private bool isDead = false;

    //
    private AudioSource hitSFX;

    //Health//
    protected int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        hitSFX = GetComponent<AudioSource>();
    }
    public void TakeDamage(int damageAmount)
    {
        hitSFX.Play();
        currentHealth -= damageAmount;
        HandleDamageAftermath();
        Debug.Log(currentHealth);
    }

    public void GetStun(float stunDuration)
    {
        StartCoroutine(Stun(stunDuration));
    }

    private void HandleDamageAftermath()
    {
        GetStun(stunDuration);
        if (currentHealth <= 0)
        {
            isDead = true;
            gameScore.AddScore();     
        }
    }

    private IEnumerator Stun(float duration)
    {
        isHit = true;
        yield return new WaitForSeconds(duration);
        isHit = false;
    }

    public bool IsHit()
    {
        return isHit;
        // To Communicate with AI_Animation script that this enemy got hit and should play the hit animation//
    }

    public bool IsDead()
    {
        return isDead;
    }
}
       
