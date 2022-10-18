using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    public Transform groundDetection;
    private RaycastHit2D groundInfo;
    private RaycastHit2D wallInfo;
    public LayerMask whatIsGround;
    private Enemy enemy;


    private void Start()
    {
        enemy = GetComponent<Enemy>();

    }



    void Update()
    {
        
            if (enemy.canMove)
            {
                Move();
            }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distanceY, whatIsGround);
        if (enemy.movingRight == true)
        {
            wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, distanceX, whatIsGround);
        }
        else
        {
            wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, distanceX, whatIsGround);
        }
        if (groundInfo.collider == false || wallInfo == true)
        {
            if (enemy.movingRight == true)
            {
                enemy.movingRight = false;
            }
            else
            {
                enemy.movingRight = true;
            }
        }
    }



}
