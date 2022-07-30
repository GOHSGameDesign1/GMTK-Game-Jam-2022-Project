using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvenOddManager : MonoBehaviour
{
    public bool isEven;
    public float pointsOnKill;
    public ParticleSystem deathParticles;

    private GameObject textDisplay;
    private bool canInstantiate;
    // Start is called before the first frame update
    void Start()
    {
        textDisplay = transform.GetChild(0).gameObject;
        canInstantiate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEven)
        {
            textDisplay.GetComponent<TextMeshPro>().text = "E";
            return;
        }
        textDisplay.GetComponent<TextMeshPro>().text = "O";
    }

    private void OnDestroy()
    {
        if (canInstantiate)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);

        }
    }

    private void OnApplicationQuit()
    {
        canInstantiate=false;
    }
}
