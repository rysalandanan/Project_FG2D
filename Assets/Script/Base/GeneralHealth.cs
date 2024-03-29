using System.Collections;
using UnityEngine;

public abstract class GeneralHealth : MonoBehaviour, IDamageable
{
    [Header("General Health Attributes")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float stunDuration;
    [SerializeField] protected float fadeDuration;

    [Header("Camera Shake Settings")]
    [SerializeField] protected float shakeIntensity;
    [SerializeField] protected float shakeDuration;

    protected bool isHit = false; //for animation//
    protected bool isDead = false;

    [SerializeField] private AudioSource hitSFX;
    protected Collider2D entityCollider;
    protected Renderer entityRenderer;

    protected int currentHealth;

    protected virtual void Start()
    {
        entityCollider = GetComponent<Collider2D>();
        entityRenderer = GetComponent<Renderer>();
        currentHealth = maxHealth;
    }
    public virtual void TakeDamage(int damageAmount)
    { 
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        PlayHitSFX();
        HandleDamageAftermath();
        GetStun(stunDuration);
    }
    public virtual void GetStun(float stunDuration)
    {
        StartCoroutine(Stun(stunDuration));
    }
    private IEnumerator Stun(float stunDuration)
    {
        isHit = true;
        ShakeCamera(shakeIntensity, shakeDuration);
        yield return new WaitForSeconds(stunDuration);
        isHit = false;
    }
    protected virtual void HandleDamageAftermath()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            StartCoroutine(FadeOutAndDeactivate());
        }
    }
    protected virtual IEnumerator FadeOutAndDeactivate()
    {
        entityCollider.enabled = false;
        while (entityRenderer.material.color.a > 0)
        {
            Color currentColor = entityRenderer.material.color;
            float newAlpha = Mathf.Max(0, currentColor.a - fadeDuration * Time.deltaTime);
            entityRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    protected virtual void PlayHitSFX()
    {
        hitSFX.Play();
    }
    public bool IsHit()
    {
        return isHit;
    }
    public bool IsDead()
    {
        return isDead;
    }
    public virtual void ShakeCamera(float intensity, float shakeDuration)
    {
        CameraShake.Instance.ShakeCamera(intensity, shakeDuration);
    }
}
