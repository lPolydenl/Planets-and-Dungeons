using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float chance;
    [SerializeField] private bool isHealing;
    [SerializeField] private int healValue;
    [SerializeField] private GameObject destroyEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            if(isHealing)
            {
                collision.gameObject.GetComponent<Health>().Heal(healValue);
            }
            if (destroyEffect)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
