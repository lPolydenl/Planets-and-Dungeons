using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    [SerializeField] private GameObject healthBar;
    private bool isEnemy;
    [SerializeField] private float StartTimeToHideHealthBar;
    private float timeToHideHealthBar;
    private void Update()
    {
        if (health <= 0)
        {
            isEnemy = TryGetComponent(out Enemy enemy);
            if(!isEnemy)
            {
                Destroy(gameObject);
            }
        }
        if (timeToHideHealthBar > 0)
        {
            timeToHideHealthBar -= Time.deltaTime;
        }
        else if (healthBar)
        {
            healthBar.SetActive(false);
        }
        if (Time.timeScale == 0f && healthBar)
        {
            healthBar.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        if(healthBar)
        {
            healthBar.SetActive(true);
        }
        timeToHideHealthBar = StartTimeToHideHealthBar;
        health -= damage;
    }
    public void Heal(int heal)
    {
        if (healthBar)
        {
            healthBar.SetActive(true);
        }
        timeToHideHealthBar = StartTimeToHideHealthBar;
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
