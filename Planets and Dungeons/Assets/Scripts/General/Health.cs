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
            isEnemy = gameObject.TryGetComponent(out Enemy enemy);
            if(!isEnemy)
            {
                Debug.Log("NIGGER DESTROYED");
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
    public void TakeDamage(int damage, bool makeInvinsible, bool takeDamageAnyway)
    {
        if(healthBar)
        {
            healthBar.SetActive(true);
        }
        timeToHideHealthBar = StartTimeToHideHealthBar;
        if(TryGetComponent(out Player player))
        {
            if(!TryGetComponent(out Invincible invincible))
            {
                health -= damage;
                if(makeInvinsible)
                {
                    player.gameObject.AddComponent<Invincible>();
                }
            }
            else if (takeDamageAnyway)
            {
                health -= damage;
            }
        }
        else
        {
            health -= damage;
        }
    }
    public void TakeDamage(int damage, bool makeInvinsible, bool takeDamageAnyway, float invincibilityDuration)
    {
        if (healthBar)
        {
            healthBar.SetActive(true);
        }
        timeToHideHealthBar = StartTimeToHideHealthBar;
        if (TryGetComponent(out Player player))
        {
            if (!TryGetComponent(out Invincible invincible))
            {
                health -= damage;
                if (makeInvinsible)
                {
                    invincible = player.gameObject.AddComponent<Invincible>();
                    invincible.duration = invincibilityDuration;
                }
            }
            else if (takeDamageAnyway)
            {
                health -= damage;
            }
        }
        else
        {
            health -= damage;
        }
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
