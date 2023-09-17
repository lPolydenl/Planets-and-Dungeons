using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotState : EnemyState
{
    protected D_EnemyShotState stateData;
    public EnemyShotState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_EnemyShotState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public void Shot(Transform shotPoint)
    {
        if (stateData.shotSound != null)
        {
            var ss = GameObject.Instantiate(stateData.shotSound);
            ss.Play();
        }
        Bullet newBullet = GameObject.Instantiate(stateData.bullet, shotPoint.position, shotPoint.rotation);
        newBullet.transform.Rotate(0f, 0f, Random.Range(-stateData.offset, stateData.offset));
        newBullet.team = "Enemy";
    }
}
