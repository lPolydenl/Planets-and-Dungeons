using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    public float duration = 1.5f;
    private float startAlphaTime = 0.1f;
    private float alphaTime;

    private SpriteRenderer[] sprites;

    public void Start()
    {
        if(TryGetComponent(out Player player))
        {
            sprites = player.allSprites;
        }
        alphaTime = startAlphaTime;
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            ChangeAlpha(1);
            Destroy(this);
            return;
        }
        alphaTime -= Time.deltaTime;
        if (alphaTime <= 0)
        {
            if (sprites[0].color.a == 0)
            {
                ChangeAlpha(1);
            }
            else
            {
                ChangeAlpha(0);
            }
        }
    }
    private void ChangeAlpha(int alphaValue)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alphaValue);
        }
        alphaTime = startAlphaTime;
    }
}
