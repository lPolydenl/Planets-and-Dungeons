using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [SerializeField] private Collider2D doorHitbox;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask player;
    private bool isOpened;
    
    private void FixedUpdate()
    {
        isOpened = Physics2D.OverlapCircle(transform.position, radius, player);
    }
    private void Update()
    {
        if (isOpened)
        {
            doorHitbox.enabled = false;
            doorAnim.SetBool("IsOpened", true);
        }
        else
        {
            doorHitbox.enabled = true;
            doorAnim.SetBool("IsOpened", false);
        }
    }


}
