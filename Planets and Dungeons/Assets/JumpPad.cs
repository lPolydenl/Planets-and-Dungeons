using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource triggerSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            anim.SetTrigger("Jump");
            if (triggerSound != null)
            {
                var ss = Instantiate(triggerSound);
                ss.Play();
            }
            rb.velocity = new Vector2(rb.velocity.x, force);
        }
    }
}
