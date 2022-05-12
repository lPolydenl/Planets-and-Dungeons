using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [SerializeField] private Collider2D doorHitbox;
    [SerializeField] private Animator doorAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorHitbox.enabled = false;
            doorAnim.SetBool("IsOpened", true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorHitbox.enabled = true;
            doorAnim.SetBool("IsOpened", false);
        }

    }


}
