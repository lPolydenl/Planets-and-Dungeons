using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    public Collider2D[] doors;
    public EnemySpawner[] enemySpawners;
    public List<GameObject> enemies;
    private bool spawned;
    [SerializeField] int scoreAward;
    private int currentHealth;
    private Health playerHealth;
    private bool isDamaged;

    [Header("Final room")]
    [SerializeField] bool isBoss;
    [HideInInspector] public AudioSource globalMusic;
    [SerializeField] AudioClip bossMusic;
    [HideInInspector] public PlayerStats playerStats;
    private bool isCompleted;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isDamaged && !isCompleted && collision.gameObject.TryGetComponent(out Player player))
        {
            if(currentHealth > playerHealth.health)
            {
                isDamaged = true;
            }
            currentHealth = playerHealth.health;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !spawned)
        {
            spawned = true;
            if(collision.gameObject.TryGetComponent(out Player player))
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
            }

            if(isBoss)
            {
                globalMusic.clip = bossMusic;
                globalMusic.enabled = false;
                globalMusic.enabled = true;
            }

            foreach (EnemySpawner spawner in enemySpawners)
            {
                GameObject enemyType = spawner.enemyTypes[Random.Range(0, spawner.enemyTypes.Length)];
                GameObject enemy = Instantiate(enemyType, spawner.transform.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = transform;
                enemies.Add(enemy);
                enemy.GetComponent<Enemy>().playerStats = playerStats;
                enemy.GetComponent<Enemy>().room = this;
            }
            StartCoroutine(CheckEnemies());

        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        EnableDoors();
    }
    public void EnableDoors()
    {
        Debug.Log("ura, zarabotala eta huyeta");
        playerStats.roomsCompleted++;
        foreach (Collider2D door in doors)
        {
            if (door != null) door.enabled = true;
        }
        if(!isDamaged)
        {
            playerStats.scoreAmount += scoreAward * 2;
        }
        else
        {
            playerStats.scoreAmount += scoreAward;
        }
        if (isBoss)
        {
            playerStats = GameObject.Find("/Canvas").GetComponent<PlayerStats>();
            playerStats.isWin = true;
        }
    }
}
