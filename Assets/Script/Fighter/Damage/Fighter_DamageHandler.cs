using System.Collections;
using UnityEngine;

public class Fighter_DamageHandler :MonoBehaviour, IDamageable
{
    [Header("General Attributes")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float stunDuration;

    [Header("Defeat Screen")]
    [SerializeField] private GameObject defeatedScreen;

    //Flag//
    private bool isHit = false;
    private bool isDead = false;

    //Health//
    protected int currentHealth;

    private AudioSource hitSFX;
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
            defeatedScreen.SetActive(true);
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
