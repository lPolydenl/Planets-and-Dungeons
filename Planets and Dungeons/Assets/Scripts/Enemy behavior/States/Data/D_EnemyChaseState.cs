using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyChaseStateData", menuName = "Data/Enemy State Data/Chase State")]
public class D_EnemyChaseState : ScriptableObject
{
    public float speed = 6f;
    public float maxDistance = 15f;
    public LayerMask playerLayer;
}
