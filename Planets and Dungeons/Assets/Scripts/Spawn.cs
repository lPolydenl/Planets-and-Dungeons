using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform spawnPoint;
    public float startTimeBtwSpawns;
    private float timeBtwSpawns;
    private Sight sight;
    private Enemy enemy;
    private bool isSpotted;
    [SerializeField] private float spawningTime;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private int maxEnemies;
    [SerializeField] private GameObject[] enemyTypes;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        sight = GetComponent<Sight>();
    }

    void Update()
    {
        isSpotted = sight.Spot();

        if (isSpotted)
        {
            enemy.canMove = false;
            enemy.anim.SetBool("IsMoving", false);
            if (timeBtwSpawns <= 0 && enemies.Count < maxEnemies)
            {
                timeBtwSpawns = startTimeBtwSpawns;
                enemy.anim.SetBool("IsSpawning", true);
            }
        }

        if (timeBtwSpawns > 0)
        {
            timeBtwSpawns -= Time.deltaTime;
        }
        if (timeBtwSpawns < startTimeBtwSpawns - spawningTime && !isSpotted)
        {
            enemy.EnableMove();
        }
    }
    public void SpawnEnemy()
    {
        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
        GameObject newEnemy = Instantiate(enemyType, spawnPoint.position, Quaternion.identity) as GameObject;
        enemies.Add(newEnemy);
        newEnemy.transform.parent = enemy.room.transform;
        newEnemy.GetComponent<Enemy>().room = enemy.room;
        enemy.room.enemies.Add(newEnemy);
        if(enemy.movingRight)
        {
            newEnemy.GetComponent<Enemy>().movingRight = true;
        }
        else
        {
            newEnemy.GetComponent<Enemy>().movingRight = false;
        }
    }

}
