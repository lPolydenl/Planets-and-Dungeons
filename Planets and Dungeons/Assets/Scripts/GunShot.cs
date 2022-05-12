using UnityEngine;

public class GunShot : MonoBehaviour
{


    public string team;
    public Bullet bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;
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
        Bullet newBullet = Instantiate(bullet, shotPoint.position, transform.rotation);
        newBullet.team = team;
    }
}
