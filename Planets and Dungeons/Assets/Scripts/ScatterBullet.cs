using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterBullet : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] int bulletCount;
    public void Scatter(Bullet parentBullet, Vector2 destroyPoint)
    {
        float rotationOffset = 360f / bulletCount;
        for(int i = 0; i < bulletCount; i++)
        {
            Bullet newBullet = Instantiate(bullet, destroyPoint, Quaternion.identity);
            newBullet.transform.Rotate(0f, 0f, rotationOffset * i);
            newBullet.team = parentBullet.team;
        }
    }
}
