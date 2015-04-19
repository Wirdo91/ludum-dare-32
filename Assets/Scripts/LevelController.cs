using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SingleEnemy : System.Object
{
    public Vector2 StartPos, EndPos;
    public EnemyMovement Movement;
}
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
    Wave[] Waves;

    int currentRound = 0;
    bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        stash = FindObjectOfType<PlayerStash>().GetComponent<PlayerStash>();
    }

    void Update()
    {
        if (gameOver)
        {
            return;

        }
        enemies.RemoveAll(enemy => enemy == null);

        if (enemies.Count == 0)
        {
            if (currentRound < Waves.Length)
            {
                SpawnEnemies();
                currentRound++;
            }
            else if (currentRound >= Waves.Length)
            {
                gameOver = true;
            }
            if (gameOver)
            {
                Debug.Log("Game Over");
                Debug.Log("Score: " + stash.Count + "/" + stash.StashStartAmount);
            }
        }
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
}
