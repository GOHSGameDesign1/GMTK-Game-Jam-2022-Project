using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public bool diceDependant;
    public float maxHealth;
    public int diceNumber;
    GameObject textObject;
    GameObject diceDisplayObject;
    
    // Start is called before the first frame update
    void Start()
    {
        textObject = transform.GetChild(0).gameObject;
        diceDisplayObject = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        textObject.SetActive(!diceDependant);
        diceDisplayObject.SetActive(diceDependant);
        textObject.GetComponent<TextMeshPro>().text = maxHealth.ToString();
        diceDisplayObject.GetComponent<TextMeshPro>().text = new string(diceNumber.ToString() + "D");
    }
}
