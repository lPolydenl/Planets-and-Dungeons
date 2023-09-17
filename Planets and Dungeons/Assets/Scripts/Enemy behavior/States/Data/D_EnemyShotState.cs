using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyShotStateData", menuName = "Data/Enemy State Data/Shot State")]
public class D_EnemyShotState : ScriptableObject
{
    public Bullet bullet;
    public float reloadTime;
    public float offset;
    public AudioSource shotSound;
}
