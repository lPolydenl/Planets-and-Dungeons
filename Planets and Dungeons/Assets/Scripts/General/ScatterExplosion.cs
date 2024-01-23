using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterExplosion : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] int bulletCount;
    public void Scatter()
    {
        float rotationOffset = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            Bullet newBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            newBullet.transform.Rotate(0f, 0f, rotationOffset * i);
            newBullet.team = "enemy";
        }
    }
}
