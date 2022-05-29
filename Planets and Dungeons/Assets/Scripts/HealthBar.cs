using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Health health;
    [SerializeField] private Vector3 offset;
    void Update()
    {
        healthBarFill.fillAmount = (float)health.health / health.maxHealth;
        //healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
