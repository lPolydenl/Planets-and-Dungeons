using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyDashStateData", menuName = "Data/Enemy State Data/Dash State")]
public class D_EnemyDashState : ScriptableObject
{
    public float maxDistance = 5f;
    public float speed = 10f;
    public LayerMask playerLayer;
    public float checkObstaclesRadius = 1.5f;
    public LayerMask obstacles;
    public float stateTime = 0.8f;
}
