using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsVFXManager : MonoBehaviour
{
    private Animator anim;
    private Vector3 velocity = Vector3.zero;
    private TMP_Text tmp;
    public IEnumerator coroutine;
    // Start is called before the first frame update
    void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TMP_Text>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        coroutine = smoothUp();
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.5f * Time.deltaTime);

        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }
    }

    public void displayPointValue(string points)
    {
        tmp.text = points;
    }

    public IEnumerator smoothUp()
    {
        for (int i = 0; i < 30; i++)
        {
            transform.position += Vector3.Lerp(Vector3.zero, new Vector3(0, 1, 0), Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy()
    {
        if (PointsManager.bulletVFX.Contains(gameObject))
        {
            PointsManager.bulletVFX.Remove(gameObject);
        }
    }
}
