using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    private Health health;
    [SerializeField] private GameObject explosion;
    private Enemy enemy;
    [SerializeField] private bool explodeOnGround;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private float lifeTime;
    private void Start()
    {
        health = GetComponent<Health>();
        enemy = GetComponent<Enemy>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player) || explodeOnGround)
        {
            health.health = 0;
        }
    }
    private void Update()
    {
        if (health.health <= 0)
        {
            Explode();
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Explode();
        }
    }
    private void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        if (explosionSound != null)
        {
            var es = Instantiate(explosionSound, transform.position, Quaternion.identity);
            es.Play();
        }
        enemy.OnDestroyGameObject();
    }
}
