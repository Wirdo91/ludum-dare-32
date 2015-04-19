using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{

    public delegate void EnemySpawned(Enemy enemy);
    public EnemySpawned myEnemySpawned;

    [SerializeField]
    float enemiesPerSecond = 5;

    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    Transform stash;

    float spawnDelay;

    [SerializeField]
    int Health = 10;

    // Use this for initialization
    void Start()
    {
        spawnDelay = 1 / enemiesPerSecond;
    }

    float spawnTimer = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (LevelController.GameOver)
        {
            return;
        }

        if (Health <= 0)
        {
            this.gameObject.SetActive(false);
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            spawnTimer = 0f;
            myEnemySpawned(SpawnEnemy());
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            this.gameObject.SetActive(true);
        }
    }

    Enemy SpawnEnemy()
    {
        GameObject currentEnemy = Instantiate(enemyPrefab);

        currentEnemy.GetComponent<Enemy>().Initiate(this.transform.position, stash.position, (EnemyMovement)Random.Range(0, 3));

        return currentEnemy.GetComponent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Bullet>() != null && col.tag == "Player")
        {
            Health--;
        }
    }
}
