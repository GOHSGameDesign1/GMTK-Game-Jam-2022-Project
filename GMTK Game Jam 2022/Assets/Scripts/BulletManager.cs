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
    GameObject currentspawnedpointVFX;

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

        currentspawnedpointVFX = Instantiate(pointVFX, cannon.transform.position, Quaternion.identity);
        currentspawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("-100");
        PointsManager.points -= 100;
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
        if(collision.tag == "Enemy")
        {
            collision.TryGetComponent<EnemyHealth>(out var enemy);

            if (!enemy.diceDependant)
            {
                enemy.maxHealth -= dmgValue;

                if(enemy.maxHealth > 0)
                {
                    currentspawnedpointVFX = Instantiate(pointVFX, transform.position, Quaternion.identity);
                    currentspawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("+50");
                    PointsManager.points += 50;
                }

                Destroy(gameObject);
                return;
            }
            if(enemy.diceNumber + 1 != dmgValue)
            {
                return;
            }
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {

    }

}

 
