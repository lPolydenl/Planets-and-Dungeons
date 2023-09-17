using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDownTrigger : MonoBehaviour
{
    public bool isTouching;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            isTouching = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            isTouching = false;
        }
    }
}
