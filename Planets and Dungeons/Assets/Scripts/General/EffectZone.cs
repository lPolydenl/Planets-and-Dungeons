using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectZone : MonoBehaviour
{
    [SerializeField] private bool poisonous;
    [SerializeField] private Poisonous poisonStats;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if(poisonous)
            {
                poisonStats.Poison(collision.gameObject);
            }
        }
    }
}
