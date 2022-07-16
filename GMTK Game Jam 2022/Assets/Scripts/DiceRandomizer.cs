using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DiceRandomizer : MonoBehaviour



{
    public GameObject dicePrefab;

    int randomNumber;

    void Start()
    {

    }

    void RollTheDice()
    {
        randomNumber = Random.Range(1,6);
    }

}