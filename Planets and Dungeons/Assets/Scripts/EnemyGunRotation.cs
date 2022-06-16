using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRotation : MonoBehaviour
{
    [SerializeField] private float range;
    private GameObject target;
    private Vector2 direction;
    [SerializeField] private float offset;
    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private Enemy enemy;
    private Sight sight;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        sight = GetComponentInParent<Sight>();
    }

    private void Update()
    {
        if (target != null && Detect())
        {
            Vector2 targetPos = target.transform.position;
            direction = targetPos - (Vector2)transform.parent.position;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(enemy.movingRight)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offset);
            }
            Vector3 gunScale = Vector3.one;
            if (rotZ > 90 || rotZ < -90)
            {
                gunScale.y = -1f;
                enemy.movingRight = false;
            }
            else
            {
                gunScale.y = 1f;
                enemy.movingRight = true;
            }
            transform.localScale = gunScale;
        }
        //else if (enemy.movingRight)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        //}
    }
    public bool Detect()
    {
        //if (target == null)
        //{
        //    return false;
        //}
        //Vector2 targetPos = target.transform.position;
        //direction = targetPos - (Vector2)transform.parent.position;
        //RaycastHit2D rayInfo = Physics2D.Raycast(transform.parent.position, direction, range, whatIsSolid);
        //if(rayInfo)
        //{
        //    if(rayInfo.collider.gameObject.TryGetComponent(out Player player))
        //    {
        //        detected = true;
        //    }
        //    else
        //    {
        //        detected = false;
        //    }
        //}
        //else
        //{
        //    return false;
        //}
        //return detected;
        return sight.Spot();
    }

}
