using UnityEngine;

public class Sight : MonoBehaviour
{
    public Transform[] sightPoints;
    public float sightLenght;
    public LayerMask whatIsSolid;
    private Enemy enemy;
    [HideInInspector] public bool isSpotted;
    [Header("Advanced")]
    private Vector2 direction;
    [SerializeField] private bool advanced;
    [HideInInspector] public GameObject target;
    public float angle;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask playerLayer;
    [HideInInspector] public bool canSeePlayer;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        if(advanced)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public bool Spot()
    {
        if(advanced)
        {
            //if(target == null) return false;
            //Vector2 targetPos = target.transform.position;
            //direction = targetPos - (Vector2)transform.position;
            //RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, sightLenght, whatIsSolid);
            //if (rayInfo)
            //{
            //    if (rayInfo.collider.gameObject.TryGetComponent(out Player player))
            //    {
            //        isSpotted = true;
            //    }
            //    else
            //    {
            //        isSpotted = false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}
            //if (isSpotted)
            //{
            //    if (direction.x > 0)
            //    {
            //        enemy.movingRight = true;
            //    }
            //    else
            //    {
            //        enemy.movingRight = false;
            //    }
            //}
            //return isSpotted;
            return (FieldOfViewCheck());
        }
            else
            {
                foreach (Transform sightPoint in sightPoints)
                {
                    if (enemy.movingRight)
                    {
                        isSpotted = Physics2D.Raycast(sightPoint.position, Vector2.right, sightLenght, whatIsSolid).collider.gameObject.CompareTag("Player");
                    }
                    else
                    {
                        isSpotted = Physics2D.Raycast(sightPoint.position, Vector2.left, sightLenght, whatIsSolid).collider.gameObject.CompareTag("Player");
                    }
                    if (isSpotted)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                return isSpotted;
            }
        
        
    }

    private bool FieldOfViewCheck()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(transform.position, sightLenght, playerLayer);

        if (rangeCheck != null)
        {
            Transform target = rangeCheck.transform;
            direction = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.right, direction) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, direction, distanceToTarget, obstacles))
                {
                    canSeePlayer = true;
                }
                else canSeePlayer = false;
            }
            else canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
        return canSeePlayer;
    }
}
