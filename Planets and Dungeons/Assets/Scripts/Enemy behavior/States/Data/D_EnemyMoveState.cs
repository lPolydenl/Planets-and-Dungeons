using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyMoveStateData", menuName = "Data/Enemy State Data/Move State")]
public class D_EnemyMoveState : ScriptableObject
{
    public float movementSpeed = 3f;
    public float stateTime = 8f;
}
