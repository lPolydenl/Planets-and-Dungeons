using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour
{
    public float duration;
    public int damage;
    public float poisonCooldown;

    private Color poisonedColor = Color.green;
    private SpriteRenderer sprite;
    private Health health;

    private bool canTakeDamage;
    private bool canCausePoison;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if(TryGetComponent(out Player player))
        {
            foreach (SpriteRenderer spriteRenderer in player.allSprites)
            {
                spriteRenderer.color = poisonedColor;
            }
        }
        sprite.color = poisonedColor;
        if(TryGetComponent(out Health hp))
        {
            health = hp;
        }
        canCausePoison = true;
    }

    private void Update()
    {
        if(canTakeDamage)
        {
            health.TakeDamage(damage, false, true);
            canTakeDamage = false;
        }
        else
        {
            if(canCausePoison)
            {
                Invoke(nameof(CausePoison), poisonCooldown);
                canCausePoison = false;
            }
        }
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            sprite.color = Color.white;
            if (TryGetComponent(out Player player))
            {
                foreach (SpriteRenderer spriteRenderer in player.allSprites)
                {
                    spriteRenderer.color = Color.white;
                }
            }
            Destroy(this);
        }
    }

    private void CausePoison()
    {
        canTakeDamage = true;
        canCausePoison = true;
    }
}
