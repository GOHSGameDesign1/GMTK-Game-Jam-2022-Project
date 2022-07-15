using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemyPrefab;
    Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector2(screenBounds.x + 2, Random.Range(screenBounds.y - 1.2f ,screenBounds.y * -1 + 1.2f));
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
