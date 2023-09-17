using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemySummonStateData", menuName = "Data/Enemy State Data/Summon State")]
public class D_EnemySummonState : ScriptableObject
{
    public Enemy[] minionTypes;
    public float reloadTime;
}
