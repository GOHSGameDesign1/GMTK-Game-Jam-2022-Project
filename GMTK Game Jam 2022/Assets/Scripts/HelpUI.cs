using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloseTimer());
    }

    IEnumerator CloseTimer()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
