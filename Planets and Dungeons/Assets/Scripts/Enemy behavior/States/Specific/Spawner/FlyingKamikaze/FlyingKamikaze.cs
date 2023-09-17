using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingKamikaze : Enemy
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float sightLenght;
    [SerializeField] private float speed;
    public override void Update()
    {

        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, sightLenght, playerLayer);
        if(rangeCheck != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, rangeCheck.transform.position, speed * Time.deltaTime);
            Vector2 direction = -(rangeCheck.transform.position - transform.position).normalized;
            transform.right = direction;

            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            Vector3 localScale = Vector3.one;

            if (rotZ > 90 || rotZ < -90)
            {
                localScale.y = -1f;
            }
            else
            {
                localScale.y = 1f;
            }
            transform.localScale = localScale;
        }
        if (health.health <= 0)
        {
            OnDestroyGameObject();
        }
    }
    public override void FixedUpdate()
    {

    }
    private void OnCollisionEnter2D(Collision2D collison)
    {
        OnDestroyGameObject();
    }
}
