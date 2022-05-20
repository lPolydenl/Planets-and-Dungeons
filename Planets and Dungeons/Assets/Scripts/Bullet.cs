using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    private float lt;
    public int damage;
    [SerializeField] public string team;
    [SerializeField] private GameObject destroyEffect;


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
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (team == "Player")
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                OnDestroyGameObject();
            }
        }

        if (team == "Enemy")
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                OnDestroyGameObject();
            }
        }

        if (collision.CompareTag("Ground"))
        {
            OnDestroyGameObject();
        }

    }
}
