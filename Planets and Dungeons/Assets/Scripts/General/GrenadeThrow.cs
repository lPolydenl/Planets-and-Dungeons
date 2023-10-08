using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [SerializeField] private float xForce;
    [SerializeField] private float yForce;
    [SerializeField] private GameObject grenade;
    [SerializeField] private Transform ThrowPoint;
    private Player player;
    private Grenades grenades;

    private void Start()
    {
        player = GetComponent<Player>();
        grenades = GetComponent<Grenades>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonUp(1) && grenades.grenades > 0)
        {
            GameObject newGrenade = Instantiate(grenade, ThrowPoint.position, Quaternion.identity);
            newGrenade.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce * player.guns.transform.right.x, yForce * player.guns.transform.right.y + 2.5f));
            grenades.RemoveGrenade();
        }
    }
}
