using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public bool isFacingRight;
    [HideInInspector] public bool canMove = true;
    public Rigidbody2D rb;
    public Animator anim;
    public bool canShot = true;
    public bool canFlip = true;
    public bool isGrounded;
    public bool isOnPlatform;
}
