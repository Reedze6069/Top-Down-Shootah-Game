using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float speed;
    
    public int health;

    public float invincibilityDuration = 1.5f;
    protected bool isInvincible = false;
    public virtual void TakeDamage(int amount)
    {
        if (isInvincible) return;

        health -= amount;

        if (health <= 0)
        {
            HandleDeath();
        }
    }
    
    public abstract void HandleDeath();
}