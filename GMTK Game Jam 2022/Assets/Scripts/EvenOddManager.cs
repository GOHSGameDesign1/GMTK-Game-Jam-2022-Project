using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

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

    public void OnHit()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (canInstantiate)
        {
            GetComponent<AudioSource>().Play();
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();

        }
    }

    private void OnApplicationQuit()
    {
        canInstantiate=false;
    }
}
