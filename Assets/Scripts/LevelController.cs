using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public SingleEnemy[] enemies;
}

public class LevelController : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    List<GameObject> enemies = new List<GameObject>();
    PlayerStash stash;

    [SerializeField]
    bool useWaves = false;

    [SerializeField]
    Wave[] Waves;

    [SerializeField]
    Text stashText, gmText;

    public static bool GameOver
    {
        get { return instance.gameOver; }
    }

    List<EnemySpawn> activeEnemySpawns = new List<EnemySpawn>();

    static LevelController instance;

    Player player;

    int currentRound = 0;
    bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        LevelController.instance = this;

        stash = FindObjectOfType<PlayerStash>();
        player = FindObjectOfType<Player>();

        activeEnemySpawns.AddRange(GameObject.FindObjectsOfType<EnemySpawn>());

        foreach (EnemySpawn spawn in activeEnemySpawns)
        {
            spawn.myEnemySpawned += EnemySpawned;
        }
    }

    void EnemySpawned(Enemy enemy)
    {
        enemies.Add(enemy.gameObject);
    }

    void Update()
    {
        if (gameOver)
        {
            return;

        }
        enemies.RemoveAll(enemy => enemy == null);
        activeEnemySpawns.RemoveAll(spawn => spawn == null);

        if (enemies.Count == 0)
        {
            if (useWaves)
            {
                if (currentRound < Waves.Length)
                {
                    SpawnEnemies();
                    currentRound++;
                    Debug.Log("Round Over");
                    Debug.Log("Score: " + stash.Count + "/" + stash.StashStartAmount);
                }
                else if (currentRound >= Waves.Length)
                {
                    gameOver = true;
                }
            }
            else if (activeEnemySpawns.Count <= 0)
            {
                gameOver = true;
            }
            if (gameOver)
            {
                Debug.Log("Game Over");
                Debug.Log("Final Score: " + stash.Count + "/" + stash.StashStartAmount);
            }
        }
        if (player.Health <= 0 || stash.Count <= 0)
        {
            gameOver = true;

            if (gameOver)
            {
                Debug.Log("Game Over");
                Debug.Log("Final Score: " + stash.Count + "/" + stash.StashStartAmount);
            }
        }

        if (gameOver)
        {
            gmText.enabled = true;
        }

        stashText.text = stash.Count + " / " + stash.StashStartAmount;
    }

    GameObject currentEnemy;
    void SpawnEnemies()
    {
        Debug.Log(currentRound);
        Debug.Log(Waves[currentRound].enemies.Length);
        for (int i = 0; i < Waves[currentRound].enemies.Length; i++)
        {
            currentEnemy = Instantiate(enemyPrefab);

            currentEnemy.GetComponent<Enemy>().Initiate(Waves[currentRound].enemies[i]);

            enemies.Add(currentEnemy);
        }
    }

    internal static Vector2 GetClostestSpawn(Vector2 curPos, Vector2 curDir)
    {
        if (instance.activeEnemySpawns.Count <= 0)
        {
            return curPos + curDir;
        }

        float currentDistance = float.PositiveInfinity;
        Vector2 currentPosition = curPos + curDir;
        foreach (EnemySpawn spawn in instance.activeEnemySpawns)
        {
            float newDistance = Vector2.Distance(curPos + (curDir * 2), spawn.transform.position);
            if (newDistance < currentDistance)
            {
                currentDistance = newDistance;
                currentPosition = spawn.transform.position;
            }
        }

        return currentPosition;
    }
}
