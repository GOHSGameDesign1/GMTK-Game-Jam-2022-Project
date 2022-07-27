using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.ParticleSystemJobs;

public class EnemyHealth : MonoBehaviour
{
    public bool diceDependant;
    public float maxHealth;
    public int diceNumber;
    public Sprite[] diceSprites;
    GameObject textObject;
    GameObject diceDisplayObject;
    public GameObject pointVFX;
    GameObject currentspawnedpointVFX;
    bool canInstantiate;
    public ParticleSystem pSystem;
    public GameObject splitEnemy;
    public float pointsOnHit;
    public float pointsOnKill;

    public bool canSplit;
    
    // Start is called before the first frame update
    void Awake()
    {
        canInstantiate = true;
        textObject = transform.GetChild(0).gameObject;
        diceDisplayObject = transform.GetChild(1).gameObject;

        //splitEnemy = (GameObject)Resources.Load("Split Enemy");
        canSplit = false;
    }

    // Update is called once per frame
    void Update()
    {
        textObject.SetActive(!diceDependant);
        diceDisplayObject.SetActive(diceDependant);
        textObject.GetComponent<TextMeshPro>().text = maxHealth.ToString();
        diceDisplayObject.GetComponent<SpriteRenderer>().sprite = diceSprites[diceNumber - 1];

        if (!diceDependant)
        {
            if(maxHealth <= 0)
            {
                //PointsManager.points += 200;

                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (canInstantiate)
        {
            Instantiate(pSystem, transform.position, Quaternion.identity);

            /*if (canSplit)
            {
                int[] splitDiceNumbers = new int[2];
                splitDiceNumbers[0] = (int)Mathf.Floor((diceNumber + 1) / 2) - 1;
                splitDiceNumbers[1] = diceNumber - splitDiceNumbers[0];
                //GameObject currentSplit = Instantiate(splitEnemy, (Vector2)transform.position + new Vector2(0f, 1f), Quaternion.identity);
                //currentSplit.GetComponent<EnemyHealth>().diceNumber = (int)Mathf.Floor((diceNumber + 1) / 2) - 1;

                int counter = 0;
                for (int i = -1; i < 2; i += 2)
                {
                    GameObject currentSplit = Instantiate(splitEnemy, (Vector2)transform.position + new Vector2(0f, i), Quaternion.identity);
                    currentSplit.GetComponent<EnemyHealth>().diceNumber = splitDiceNumbers[counter];
                    counter++;
                }
            }*/
            // currentspawnedpointVFX = Instantiate(pointVFX, transform.position, Quaternion.identity);
            //currentspawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("+200");

        }
        
    }

    private void OnApplicationQuit()
    {
        canInstantiate = false;
    }

    public void OnHit()
    {
        if (canSplit)
        {
            int[] splitDiceNumbers = new int[2];
            splitDiceNumbers[0] = (int)Mathf.Floor((diceNumber) / 2);
            splitDiceNumbers[1] = diceNumber - splitDiceNumbers[0];
            //GameObject currentSplit = Instantiate(splitEnemy, (Vector2)transform.position + new Vector2(0f, 1f), Quaternion.identity);
            //currentSplit.GetComponent<EnemyHealth>().diceNumber = (int)Mathf.Floor((diceNumber + 1) / 2) - 1;

            int counter = 0;
            for (int i = -1; i < 2; i += 2)
            {
                GameObject currentSplit = Instantiate(splitEnemy, (Vector2)transform.position + new Vector2(0f, i), Quaternion.identity);
                currentSplit.TryGetComponent<EnemyHealth>(out var currentSplitHealth);
                currentSplitHealth.diceNumber = splitDiceNumbers[counter];
                currentSplitHealth.canSplit = false;
                
                counter++;
            }
        }

        Destroy(gameObject);
    }
}
