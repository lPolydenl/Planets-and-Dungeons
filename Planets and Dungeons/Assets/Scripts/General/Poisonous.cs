using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisonous : MonoBehaviour
{
    [SerializeField] private float poisonDuration;
    [SerializeField] private int poisonDamage;
    [SerializeField] private float poisonCooldown;

    public void Poison(GameObject target)
    {
        if(target.TryGetComponent(out Poisoned poison))
        {
            if(poison.duration < poisonDuration)
            {
                poison.duration = poisonDuration;
            }
            if(poison.damage < poisonDamage)
            {
                poison.damage = poisonDamage;
            }
            if(poison.poisonCooldown > poisonCooldown)
            {
                poison.poisonCooldown = poisonCooldown;
            }
        }
        else
        {
            Poisoned poisoned = target.AddComponent<Poisoned>();
            poisoned.duration = poisonDuration;
            poisoned.damage = poisonDamage;
            poisoned.poisonCooldown = poisonCooldown;
        }
    }
}
