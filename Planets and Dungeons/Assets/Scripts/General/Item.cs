using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float chance;
    [SerializeField] private bool isHealing;
    [SerializeField] private int healValue;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private bool grenade;
    [SerializeField] private int grenadesAmount;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            if(isHealing)
            {
                collision.gameObject.GetComponent<Health>().Heal(healValue);
            }
            if(grenade)
            {
                collision.gameObject.GetComponent<Grenades>().AddGrenades(grenadesAmount);
            }
            if (destroyEffect)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
