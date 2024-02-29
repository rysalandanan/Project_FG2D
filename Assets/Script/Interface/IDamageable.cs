public interface IDamageable 
{
   void TakeDamage(int damageAmount);
   void GetStun(float stunDuration);
    bool IsHit();
    bool IsDead();
}
