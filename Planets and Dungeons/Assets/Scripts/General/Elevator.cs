using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform upPos;
    public Transform downPos;
    public float speed;
    [SerializeField] private SpriteRenderer elevatorState;
    [SerializeField] private Sprite active;
    [SerializeField] private Sprite passive;
    [SerializeField] private bool isDown;
    private bool isTouching;
    private float startTimeBtwGoingUp = 0.2f;
    private float timeBtwGoingUp = 0.2f;
    private ElevatorDownTrigger downTrigger;

    private void Start()
    {
        downTrigger = downPos.GetComponent<ElevatorDownTrigger>();
    }
    private void Update()
    {
        if (isTouching && Input.GetKeyDown(KeyCode.E))
        {
            if (transform.position.y <= downPos.position.y)
            {
                isDown = true;
            }
            else if (transform.position.y >= upPos.position.y)
            {
                isDown = false;
            }
        }
        if(transform.position.y >= upPos.position.y && downTrigger.isTouching && Input.GetKeyDown(KeyCode.E))
        {
            isDown = false;
        }
        if (isDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, upPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, downPos.position, speed * Time.deltaTime);
        }
        if (transform.position.y <= downPos.position.y || transform.position.y >= upPos.position.y)
        {
            elevatorState.sprite = passive;
        }
        else
        {
            elevatorState.sprite = active;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;

        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = false;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy) || other.gameObject.TryGetComponent(out Player player))
        {

            timeBtwGoingUp -= Time.deltaTime;
            isDown = true;
            timeBtwGoingUp = startTimeBtwGoingUp;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        timeBtwGoingUp = startTimeBtwGoingUp;
    }
}
