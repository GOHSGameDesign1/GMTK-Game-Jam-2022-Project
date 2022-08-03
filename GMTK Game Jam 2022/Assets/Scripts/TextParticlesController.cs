using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using TMPro;

public class TextParticlesController : MonoBehaviour
{
    private TextMeshPro tmp;

    public float riseSpeed;
    public float fadeAmount;

    // Start is called before the first frame update
    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.Translate(transform.up * riseSpeed * Time.deltaTime);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, Mathf.Lerp(tmp.color.a, 0, fadeAmount * Time.deltaTime));

        if(tmp.color.a <= 0.05)
        {
            Destroy(gameObject);
        }
    }

    public void displayPointValue(string points)
    {
        tmp.text = points;
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
