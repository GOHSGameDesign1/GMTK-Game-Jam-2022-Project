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

    private bool canSplit;
    
    // Start is called before the first frame update
    void Awake()
    {
        canInstantiate = true;
        textObject = transform.GetChild(0).gameObject;
        diceDisplayObject = transform.GetChild(1).gameObject;

        splitEnemy = (GameObject)Resources.Load("Split Enemy");
        canSplit = false;
    }

    // Update is called once per frame
    void Update()
    {
        textObject.SetActive(!diceDependant);
        diceDisplayObject.SetActive(diceDependant);
        textObject.GetComponent<TextMeshPro>().text = maxHealth.ToString();
        diceDisplayObject.GetComponent<SpriteRenderer>().sprite = diceSprites[diceNumber];

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
            GameObject currentSplit = Instantiate(splitEnemy, (Vector2)transform.position + new Vector2(0f,1f), Quaternion.identity);
            currentSplit.GetComponent<EnemyHealth>().diceNumber = (int)Mathf.Floor((diceNumber + 1)/2) - 1;
            // currentspawnedpointVFX = Instantiate(pointVFX, transform.position, Quaternion.identity);
            //currentspawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("+200");

        }
        
    }

    private void OnApplicationQuit()
    {
        canInstantiate = false;
    }
}
