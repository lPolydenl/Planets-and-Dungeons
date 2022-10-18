using UnityEngine;

public class GunShot : MonoBehaviour
{


    public string team;
    public Bullet bullet;
    public Transform[] shotPoints;
    public float startTimeBtwShots;
    public float spreadingAngle;
    public float speedChanger;
    public int minBulletCount;
    public int maxBulletCount;
    private float timeBtwShots;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource shotSound;
    void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    anim.SetTrigger("Shot");
                    timeBtwShots = startTimeBtwShots;
                }

            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }


    }

    public void Shot()
    {
        if (shotSound != null)
        {
            var ss = Instantiate(shotSound);
            ss.Play();
        }
        foreach (Transform shotPoint in shotPoints)
        {
            int bulletCount = Random.Range(minBulletCount, maxBulletCount + 1);
            for (int i = 0; i < bulletCount; i++)
            {
                Bullet newBullet = Instantiate(bullet, shotPoint.position, transform.rotation);
                newBullet.speed += Random.Range(-speedChanger, speedChanger);
                newBullet.transform.Rotate(0f, 0f, Random.Range(-spreadingAngle, spreadingAngle) / 2);
                newBullet.team = team;
            }
        }
    }
}
