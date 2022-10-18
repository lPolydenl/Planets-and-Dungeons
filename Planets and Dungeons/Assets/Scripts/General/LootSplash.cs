using UnityEngine;

public class LootSplash : MonoBehaviour
{
    public Transform objTrans;
    private Vector2 off;
    private Rigidbody2D rb;
    private Vector2 velocity;
    public float splashForce;

    private void Start()
    {
        off = new Vector3(Random.Range(-2f, 2f), Random.Range(1f, 4f));
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
        velocity.x = off.x * splashForce;
        velocity.y = off.y * splashForce;
        rb.velocity = velocity;
    }

    


}
