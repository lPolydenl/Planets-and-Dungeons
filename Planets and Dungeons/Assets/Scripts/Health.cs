using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    private bool isEnemy;
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
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
