using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    private float lt;
    public int damage;
    [SerializeField] public string team;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private AudioSource destroySound;
    [SerializeField] private float deathEffectOffset;
    [SerializeField] private Transform destroyPoint;
    [SerializeField] private bool makeInvincible = true;
    [SerializeField] private bool takeDamageAnyway;


    private void Start()
    {
        lt = lifetime;
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        lt -= Time.deltaTime;
        if(lt <= 0)
        {
            OnDestroyGameObject();
        }
    }

    private void OnDestroyGameObject()
    {
        if(destroyEffect != null)
        {
            if(destroyPoint != null)
            {
                Instantiate(destroyEffect, destroyPoint.position, transform.rotation);
            }
            else
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }
        }
        if(destroySound != null)
        {
            var ss = Instantiate(destroySound);
            ss.Play();
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (team == "Player")
        {
            if (collision.CompareTag("Enemy"))
            {
                if (TryGetComponent(out Poisonous poisonous))
                {
                    poisonous.Poison(collision.gameObject);
                }
                collision.GetComponent<Health>().TakeDamage(damage, makeInvincible, takeDamageAnyway);
                OnDestroyGameObject();
            }
        }

        if (team == "Enemy")
        {
            if (collision.CompareTag("Player") && !collision.TryGetComponent(out Invincible invincible))
            {
                if(TryGetComponent(out Poisonous poisonous))
                {
                    poisonous.Poison(collision.gameObject);
                }
                collision.GetComponent<Health>().TakeDamage(damage, makeInvincible, takeDamageAnyway);
                OnDestroyGameObject();
            }
        }

        if (collision.CompareTag("Ground"))
        {
            if(TryGetComponent(out ScatterBullet scatterBullet))
            {
                scatterBullet.Scatter(this, destroyPoint.position);
            }
            OnDestroyGameObject();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (team == "Player")
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (TryGetComponent(out Poisonous poisonous))
                {
                    poisonous.Poison(collision.gameObject);
                }
                collision.gameObject.GetComponent<Health>().TakeDamage(damage, makeInvincible, takeDamageAnyway);
                OnDestroyGameObject();
            }
        }

        if (team == "Enemy")
        {
            if (collision.gameObject.CompareTag("Player") && !collision.gameObject.TryGetComponent(out Invincible invincible))
            {
                if (TryGetComponent(out Poisonous poisonous))
                {
                    poisonous.Poison(collision.gameObject);
                }
                collision.gameObject.GetComponent<Health>().TakeDamage(damage, makeInvincible, takeDamageAnyway);
                OnDestroyGameObject();
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (TryGetComponent(out ScatterBullet scatterBullet))
            {
                scatterBullet.Scatter(this, destroyPoint.position);
            }
            OnDestroyGameObject();
        }
    }
}
