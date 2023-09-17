using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public TankIdleState IdleState { get; private set; }
    public TankMoveState MoveState { get; private set; }
    public TankShotState ShotState { get; private set; }

    [SerializeField] private D_EnemyIdleState IdleStateData;
    [SerializeField] private D_EnemyMoveState MoveStateData;
    [SerializeField] private D_EnemyShotState ShotStateData;

    [SerializeField] private Transform[] sightPoints;
    [SerializeField] LayerMask whatIsSolid;
    [SerializeField] float sightLenght;

    [HideInInspector] public float reloadTime;

    public Transform shotPoint;
    public override void Start()
    {
        base.Start();

        IdleState = new TankIdleState(this, stateMachine, "idle", IdleStateData, this);
        MoveState = new TankMoveState(this, stateMachine, "move", MoveStateData, this);
        ShotState = new TankShotState(this, stateMachine, "shot", ShotStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public override void Update()
    {
        base.Update();

        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }
    }

    public bool CheckPlayer()
    {
        bool isSpotted = false;
        foreach (Transform sightPoint in sightPoints)
        {
            isSpotted = Physics2D.Raycast(sightPoint.position, Vector2.right * facingDirection, sightLenght, whatIsSolid).collider.gameObject.CompareTag("Player");
            if (isSpotted)
            {
                break;
            }
            else
            {
                continue;
            }
        }
        return isSpotted;
    }
}
