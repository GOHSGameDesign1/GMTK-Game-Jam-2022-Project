using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBigPrefab;
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

            if(PointsManager.points >= 1000)
            {
                int determine = Random.Range(0, 3);
                switch (determine)
                {
                    case 0:
                        SpawnNormalEnemy();
                        break;
                    case 1:
                        SpawnBigEnemy();
                        break;
                    case 2:
                        SpawnBigEnemy();
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
            enemy.diceNumber = Random.Range(0, 6);
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
}
