using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletManager : MonoBehaviour
{

    [SerializeField]
    private int dmgValue;
    public float bulletSpeed;
    private GameObject cannon;
    private CannonManager cannonManager;
    private Vector2 direction;
    private Vector2 mousePos;
    private Rigidbody2D rb;
    public GameObject pointVFX;
    public ParticleSystem explodeParticles;
    GameObject currentspawnedpointVFX;
    public GameObject enemyTextVFX;
    GameObject enemySpawnedpointVFX;

    public Sprite[] diceSprites;

    // Start is called before the first frame update
    void Awake()
    {
        cannon = GameObject.Find("Cannon");
        cannonManager = cannon.GetComponent<CannonManager>();
        dmgValue = (int)cannonManager.currentDiceValue;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = mousePos - (Vector2)cannon.transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = diceSprites[dmgValue - 1];

        PointsManager.SpawnPoints(-100, (Vector2)cannon.transform.position, 1);

        StartCoroutine(LifetimeTimer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //direction = mousePos - (Vector2)transform.position;
        Move();
        Rotate();
    }

    void Move()
    {
        Vector2 newPos = rb.position + direction.normalized * bulletSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void Rotate()
    {
        rb.rotation += 15f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //CHECKS FOR NORMAL ENEMIES
        if(collision.tag == "Enemy")
        {
            collision.TryGetComponent<EnemyHealth>(out var enemy);

            //CHECKS FOR HEALTH RELIANT ENEMIES
            if (!enemy.diceDependant)
            {
                enemy.maxHealth -= dmgValue;

                if(enemy.maxHealth > 0)
                {
                    PointsManager.SpawnPoints(enemy.pointsOnHit, transform.position, 0);
                    Instantiate(explodeParticles, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    return;
                }

                PointsManager.SpawnPoints(enemy.pointsOnKill, transform.position, 0);
                Destroy(gameObject);
                return;
            }
            //CHECKS FOR DICE DEPENDANT ENEMIES
            if(enemy.diceNumber != dmgValue)
            {
                enemySpawnedpointVFX = Instantiate(enemyTextVFX, (Vector2)enemy.transform.position + new Vector2(0f,1f), Quaternion.identity);
                enemySpawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("Needs " + (enemy.diceNumber) + "!");
                return;
            }
            PointsManager.SpawnPoints(enemy.pointsOnKill, transform.position,0);
            enemy.OnHit();
            Destroy(gameObject);
        }

        //CHECKS FOR EVEN/ODD ENEMIES
        if(collision.tag == "EvenOdd")
        {
            collision.TryGetComponent<EvenOddManager>(out var enemy);

            //CHECKS FOR EVEN
            if (enemy.isEven)
            {
                if(dmgValue % 2 != 0)
                {
                    enemySpawnedpointVFX = Instantiate(enemyTextVFX, (Vector2)enemy.transform.position + new Vector2(0f, 1f), Quaternion.identity);
                    enemySpawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("Needs Even!");
                    return;
                }
                PointsManager.SpawnPoints(enemy.pointsOnKill, transform.position,0);
                enemy.OnHit();
                Destroy(gameObject);
                return;
            }
            //CHECKS FOR ODDS
            if (dmgValue % 2 == 0)
            {
                enemySpawnedpointVFX = Instantiate(enemyTextVFX, (Vector2)enemy.transform.position + new Vector2(0f, 1f), Quaternion.identity);
                enemySpawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("Needs Odd!");
                return;
            }
            PointsManager.SpawnPoints(enemy.pointsOnKill, transform.position,0);
            enemy.OnHit();
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}

 
