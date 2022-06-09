using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : MonoBehaviour
{
    private Player player;
    public float stunnedTime;
    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        player.canMove = false;
    }
    private void Update()
    {
        if(stunnedTime <= 0)
        {
            if(player.isGrounded || player.isOnPlatform)
            {
                player.canMove = true;
                Destroy(this);
            }
        }
        else
        {
            stunnedTime -= Time.deltaTime;
        }
    }

}
