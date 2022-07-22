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
    
    // Start is called before the first frame update
    void Start()
    {
        canInstantiate = true;
        textObject = transform.GetChild(0).gameObject;
        diceDisplayObject = transform.GetChild(1).gameObject;
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
           // currentspawnedpointVFX = Instantiate(pointVFX, transform.position, Quaternion.identity);
            //currentspawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("+200");
        }
        
    }

    private void OnApplicationQuit()
    {
        canInstantiate = false;
    }
}
