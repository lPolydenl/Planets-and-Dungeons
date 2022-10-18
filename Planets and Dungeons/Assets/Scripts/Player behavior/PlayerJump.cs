using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlatforms;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private float timeBtwCheckGround;
    [SerializeField] private PlayerManager pm;
}
