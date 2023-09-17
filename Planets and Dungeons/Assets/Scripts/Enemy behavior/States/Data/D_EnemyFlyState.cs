using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyFlyStateData", menuName = "Data/Enemy State Data/Fly State")]
public class D_EnemyFlyState : ScriptableObject
{
    public float xFrequency = 2f;
    public float yFrequency = 2f;
    public float xAmplitude = 2f;
    public float yAmplitude = 2f;
    public float xOffset = 1f;
    public float yOffset = 0f;
    public bool moveSineX = true;
    public bool moveSineY = true;
}
