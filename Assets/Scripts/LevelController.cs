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
    Text stashText, gmText, startText;

    int enemiesKilled = 0;
    int enemiesSpawned = 0;

    public static bool GameOver
    {
        get { return instance.gameOver; }
    }
    public static bool GameStarted
    {
        get { return instance.gameStarted; }
    }

    List<EnemySpawn> currentEnemySpawns = new List<EnemySpawn>();
    List<EnemySpawn> hardEnemySpawnsExtention = new List<EnemySpawn>();

    static LevelController instance;

    Player player;

    int currentRound = 0;
    bool gameStarted = false;
    bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        LevelController.instance = this;

        stash = FindObjectOfType<PlayerStash>();
        player = FindObjectOfType<Player>();

        foreach (EnemySpawn spawn in GameObject.FindObjectsOfType<EnemySpawn>())
        {
            spawn.myEnemySpawned += EnemySpawned;

            if (spawn.gameObject.tag == "HardSpawn")
            {
                hardEnemySpawnsExtention.Add(spawn);
                spawn.gameObject.SetActive(false);
            }
            else
            {
                currentEnemySpawns.Add(spawn);
            }
        }
    }

    void EnemySpawned(Enemy enemy)
    {
        enemies.Add(enemy.gameObject);
        enemiesSpawned++;
    }

    public static void EnemyKilled()
    {
        instance.enemiesKilled++;
    }

    void Update()
    {
        if (!gameStarted || gameOver)
        {
            return;

        }
        enemies.RemoveAll(enemy => enemy == null);
        currentEnemySpawns.RemoveAll(spawn => spawn == null || !spawn.gameObject.activeSelf);

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
            else if (currentEnemySpawns.Count <= 0)
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
            gmText.gameObject.SetActive(true);

            foreach (Text text in gmText.GetComponentsInChildren<Text>())
            {
                if (stash.Count > 0 && player.Health > 0)
                {
                    if (text.name == "YouWin")
                        text.enabled = true;
                }
                if (text.name == "Stash")
                {
                    text.text = "Stash stolen: " + (stash.StashStartAmount - stash.Count) + "/" + stash.StashStartAmount;
                }
                else if (text.name == "EnemySpawned")
                {
                    text.text = "Enemies Spawned: " + enemiesSpawned;
                }
                else if (text.name == "EnemyKilled")
                {
                    text.text = "Enemies Killed: " + enemiesKilled;
                }
            }
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
        if (instance.currentEnemySpawns.Count <= 0)
        {
            return curPos + curDir;
        }

        float currentDistance = float.PositiveInfinity;
        Vector2 currentPosition = curPos + curDir;
        foreach (EnemySpawn spawn in instance.currentEnemySpawns)
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

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        //TODO Somethign
    }

    public void HardGame()
    {
        foreach (EnemySpawn spawn in hardEnemySpawnsExtention)
        {
            spawn.gameObject.SetActive(true);
        }

        NormalGame();
    }

    public void NormalGame()
    {
        gameStarted = true;
        startText.gameObject.SetActive(false);
    }
}
