using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxHealthMin, maxHealthMax;
    public float spawnTimer;
    Vector2 screenBounds;
    private GameObject currentlySpawnedEnemy;
    public float points2LvlUp;
    private float pointsThreshold;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        pointsThreshold = points2LvlUp;
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
            yield return new WaitForSeconds(spawnTimer);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector2(screenBounds.x + 5, Random.Range(screenBounds.y - 1.2f ,screenBounds.y * -1 + 6));
        int determine = Random.Range(0, 2);
        currentlySpawnedEnemy =  Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
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
}
