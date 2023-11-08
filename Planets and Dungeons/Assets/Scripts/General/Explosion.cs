using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float duration;
    public int damage;

    [SerializeField] private bool makeInvincible = true;
    [SerializeField] private bool takeDamageAnyway;
    [SerializeField] private bool damagePlayer = true;
    [SerializeField] private float stunTime;
    [SerializeField] private DestroyEffect stunEffect;

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Health health))
        {
            if(damagePlayer && collision.gameObject.TryGetComponent(out Player player))
            {
                health.TakeDamage(damage, makeInvincible, takeDamageAnyway);
            }
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                health.TakeDamage(damage, makeInvincible, takeDamageAnyway);

                if (stunTime > 0f)
                {
                    enemy.stunTime = stunTime;
                    DestroyEffect effect = Instantiate(stunEffect, enemy.transform.position, Quaternion.identity);
                    effect.lifetime = stunTime;
                    effect.transform.parent = enemy.transform;
                }
            }
        }
    }
}
