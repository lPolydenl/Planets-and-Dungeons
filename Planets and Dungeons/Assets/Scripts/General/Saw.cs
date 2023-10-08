using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private int startPoint;
    [SerializeField] private bool reverse;
    [SerializeField] private bool loop;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage = 3;
    [SerializeField] private float invincibilityDuration = 2f;
    private int nextPoint;

    private void Start()
    {
        nextPoint = startPoint;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoints[nextPoint].position, speed * Time.deltaTime);
        Vector2 sPos = transform.position;
        Vector2 pPos = movePoints[nextPoint].position;
        if (sPos == pPos)
        {
            MoveToNextPoint(reverse);
        }
    }

    private void MoveToNextPoint(bool isReverse)
    {
        if(!isReverse)
        {
            if (nextPoint + 1 >= movePoints.Length)
            {
                if (loop)
                {
                    nextPoint = 0;
                }
                else
                {
                    reverse = true;
                    MoveToNextPoint(reverse);
                }
            }
            else
            {
                nextPoint++;
            }
        }
        else
        {
            if (nextPoint - 1 < 0)
            {
                if (loop)
                {
                    nextPoint = movePoints.Length - 1;
                }
                else
                {
                    reverse = false;
                    MoveToNextPoint(reverse);
                }
            }
            else
            {
                nextPoint--;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.GetComponent<Health>().TakeDamage(damage, true, false, invincibilityDuration);
        }
    }
}
