using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBigPrefab;
    public GameObject enemySplitPrefab;
    public GameObject enemyEvenOddPrefab;
    public int maxHealthMin, maxHealthMax;
    public float spawnTimer;
    Vector2 screenBounds;
   // private GameObject currentlySpawnedEnemy;
    public float points2LvlUp;
    private float pointsThreshold;
    Vector2 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        pointsThreshold = points2LvlUp;
        spawnPos = new Vector2(screenBounds.x + 5, Random.Range(screenBounds.y - 1.2f, screenBounds.y * -1 + 6));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Screen.width + "..." + Screen.height);

        if(PointsManager.points >= pointsThreshold)
        {
            pointsThreshold += points2LvlUp;
            maxHealthMin++;
            maxHealthMax++;

        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            spawnPos = new Vector2(screenBounds.x + 5, Random.Range(screenBounds.y - 1.2f, screenBounds.y * -1 + 6));
            yield return new WaitForSeconds(spawnTimer);

            if(PointsManager.points >= 10000)
            {
                int determine = Random.Range(0, 5);
                switch (determine)
                {
                    case 0:
                        SpawnNormalEnemy();
                        break;
                    case 1:
                        SpawnNormalEnemy();
                        break;
                    case 2:
                        SpawnEvenOdd();
                        break;
                    case 3:
                        SpawnBigEnemy();
                        break;
                    case 4:
                        SpawnSplitEnemy();
                        break;
                }
                continue;
            }

            if (PointsManager.points >= 5000)
            {
                int determine = Random.Range(0, 3);
                switch (determine)
                {
                    case 0:
                        SpawnNormalEnemy();
                        break;
                    case 1:
                        SpawnNormalEnemy();
                        break;
                    case 2:
                        SpawnEvenOdd();
                        break;
                }
                continue;
            }

            SpawnNormalEnemy();
        }
    }

    void SpawnNormalEnemy()
    {
        int determine = Random.Range(0, 2);
        GameObject currentlySpawnedEnemy =  Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        currentlySpawnedEnemy.TryGetComponent<EnemyHealth>(out var enemy);

        if(determine > 0)
        {
            enemy.diceDependant = true;
            enemy.diceNumber = Random.Range(1, 7);
        }
        else
        {
            enemy.diceDependant = false;
            enemy.maxHealth = Random.Range(maxHealthMin, maxHealthMax);
        }
    }

    void SpawnBigEnemy()
    {
        GameObject currentlySpawnedEnemy = Instantiate(enemyBigPrefab, spawnPos, Quaternion.identity);
        currentlySpawnedEnemy.TryGetComponent<EnemyHealth>(out var enemy);
        enemy.diceDependant = false;
        enemy.maxHealth = Random.Range(maxHealthMax, maxHealthMax + 4);
    }

    void SpawnSplitEnemy()
    {
        Vector2 splitSpawnPos = new Vector2(screenBounds.x + 5, Random.Range(screenBounds.y - 2.2f, screenBounds.y * -1 + 7)); //new spawnPos so a split doesnt spawn offScreen
        GameObject currentlySpawnedEnemy = Instantiate(enemySplitPrefab, splitSpawnPos, Quaternion.identity);

        currentlySpawnedEnemy.TryGetComponent<EnemyHealth>(out var enemy);
        enemy.canSplit = true;
        enemy.diceDependant = true;
        enemy.diceNumber = Random.Range(2, 7);
    }

    void SpawnEvenOdd()
    {
        GameObject currentlySpawnedEnemy = Instantiate(enemyEvenOddPrefab, spawnPos, Quaternion.identity);
        currentlySpawnedEnemy.TryGetComponent<EvenOddManager>(out var enemy);

        int determine = Random.Range(0, 2);

        if(determine == 0)
        {
            enemy.isEven = true;
        }
        else
        {
            enemy.isEven = false;
        }

    }
}
