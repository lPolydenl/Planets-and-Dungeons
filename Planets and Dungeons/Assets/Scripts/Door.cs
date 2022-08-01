using UnityEngine;


public class Door : MonoBehaviour
{
    public int index;
    public RoomSpawner rs;
    private bool isTouching;
    private GameObject player;
    public Animator panel;
    public Animator doorAnimation;




    private void Start()
    {
        panel = GameObject.Find("/Canvas/Panel").GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isTouching)
        {
            panel.SetTrigger("DoorOpening");
            if(doorAnimation != null)
            {
                doorAnimation.SetTrigger("DoorOpening");
            }
            if (index % 2 == 1)
            {
                Animator connectedDoorAnimation = rs.Doors[index + 1].GetComponent<Door>().doorAnimation;
                if (connectedDoorAnimation != null)
                {
                    connectedDoorAnimation.SetTrigger("DoorClosing");
                }
            }
            else if (index % 2 == 0)
            {
                Animator connectedDoorAnimation = rs.Doors[index - 1].GetComponent<Door>().doorAnimation;
                if (connectedDoorAnimation != null)
                {
                    connectedDoorAnimation.SetTrigger("DoorClosing");
                }
            }
        }
    }

    public void PlayerTeleportation()
    {
        Debug.Log("ahuyenno rabotayet");
        if (index % 2 == 1)
        {
            player.transform.position = rs.Doors[index + 1].transform.position;

        }
        else if (index % 2 == 0)
        {
            player.transform.position = rs.Doors[index - 1].transform.position;

        }


    }

}