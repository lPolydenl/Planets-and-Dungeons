using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundKamikaze : Enemy
{
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float speed;

    private bool isDetectingWall;
    public override void FixedUpdate()
    {
        isDetectingWall = CheckWall();
        if(isDetectingWall)
        {
            Flip();
        }
    }

    public override void Update()
    {

        SetVelocityX(speed);
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            OnDestroyGameObject();
        }
        if (health.health <= 0)
        {           
            OnDestroyGameObject();          
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            OnDestroyGameObject();
        }
    }
}
