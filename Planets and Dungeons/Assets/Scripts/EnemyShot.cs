using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public string team;
    public Bullet bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;
    private float timeBtwShots;
    private Enemy enemy;
    private Sight sight;
    private bool isSpotted;
    private bool hasGun;
    private EnemyGunRotation gunRot;
    [HideInInspector] public Animator gunAnim;
    [SerializeField] private float shootingTime;
    [SerializeField] private float offset;
    [SerializeField] private float startDelay;
    private float delay;
    [SerializeField] private AudioSource shotSound;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        if(TryGetComponent(out Sight s))
        {
            sight = s;
        }
        if(enemy.gun != null && enemy.gun.TryGetComponent(out EnemyGunRotation gunRotation))
        {
            hasGun = true;
            gunRot = gunRotation;
        }
    }
    void Update()
    {
        if(sight != null)
        {
            isSpotted = sight.Spot();
        }
        else if(hasGun)
        {
            isSpotted = gunRot.Detect();
        }

        if (isSpotted)
        {
            delay -= Time.deltaTime;
            enemy.canMove = false;
            enemy.anim.SetBool("IsMoving", false);
            if (timeBtwShots <= 0)
            {
                timeBtwShots = startTimeBtwShots;
                if(hasGun)
                {
                    if(delay <= 0 || startDelay == 0)
                    {
                        enemy.gunAnim.SetBool("IsShooting", true);
                        Shot();
                        if(startDelay != 0)
                        {
                            enemy.gunAnim.SetBool("IsPromoting", false);
                        }
                    }
                    else
                    {
                        enemy.gunAnim.SetBool("IsPromoting", true);
                    }
                }
                else
                {
                    enemy.anim.SetBool("IsShooting", true);
                }
            }

        }
        else if (startDelay != 0)
        {
            delay = startDelay;
            enemy.gunAnim.SetBool("IsPromoting", false);
        }
        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (timeBtwShots < startTimeBtwShots - shootingTime && !isSpotted)
        {
            enemy.EnableMove();
        }
    }
    private void Shot()
    {
        if (shotSound != null)
        {
            var ss = Instantiate(shotSound);
            ss.Play();
        }
        Bullet newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        newBullet.transform.Rotate(0f, 0f, Random.Range(-offset, offset));
        newBullet.team = team;
    }


}
