using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Animator anim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }
}
