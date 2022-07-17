using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DiceRandomizer : MonoBehaviour



{
    public GameObject dicePrefab;
    public GameObject[] spawnPoints;
    [SerializeField]
    public static List<GameObject> dice = new List<GameObject>();
    [SerializeField]
    private GameObject[] spawnedDice = new GameObject[3];
    private GameObject currentSpawnedDie;
    private int counter;
    bool canRoll;

    int randomNumber;

    private void Start()
    {
        canRoll = true;
    }

    private void Update()
    {
        CheckIfRollable();
        if (canRoll)
        {
            RollDice();
        }
    }

    public void RerollDice()
    {
        PointsManager.points -= 200;
        RollDice();
    }


    public void RollDice()
    {

        foreach(GameObject die in spawnedDice)
        {
           if(die != null)
            {
                Destroy(die);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            currentSpawnedDie = Instantiate(dicePrefab, (Vector2)spawnPoints[i].transform.position, Quaternion.identity);
            spawnedDice[i] = currentSpawnedDie;
            dice.Add(currentSpawnedDie);
        }
    }

    private void CheckIfRollable()
    {
        counter = 0;
            foreach (GameObject die in dice)
            {
                if (die != null)
                {
                    counter++;
                }
            }

        if (counter == 0)
        {
            //Debug.Log("Use at least 2 dice first!");
            canRoll = true;
            return;
        }

        canRoll = false;
    }

}