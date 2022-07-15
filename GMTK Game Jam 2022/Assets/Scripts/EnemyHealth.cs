using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public bool diceDependant;
    public float maxHealth;
    GameObject textObject;
    
    // Start is called before the first frame update
    void Start()
    {
        textObject = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        textObject.GetComponent<TextMeshPro>().text = maxHealth.ToString();
    }
}
