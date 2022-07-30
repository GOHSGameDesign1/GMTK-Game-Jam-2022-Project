using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonManager : MonoBehaviour
{
    [HideInInspector]
    Vector2 direction;
    Vector2 mousePos;
    public GameObject barrel;
    public GameObject diceSlot;
    public GameObject bulletPrefab;
    public ParticleSystem shootParticles;
    private Transform firePoint;
    private SpriteRenderer barrelRenderer;

    [SerializeField]
    private InputAction fire;

    [HideInInspector]
    public float currentDiceValue;

    private void OnEnable()
    {
        fire.Enable();
        fire.performed += Shoot;
    }

    private void OnDisable()
    {
        fire.performed -= Shoot;
        fire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        firePoint = barrel.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        barrel.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot(InputAction.CallbackContext context)
    {
        diceSlot.TryGetComponent<Slot>(out var slot);
        if (slot.attached)
        {
            slot.transform.DetachChildren();
            slot.attached = false;
            StopAllCoroutines();
            StartCoroutine(squish());
            currentDiceValue = slot.diceValue;
            Destroy(slot.currentDiceAttached);
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Instantiate(shootParticles, firePoint);
        }
    }

    IEnumerator squish()
    {
        Vector3 velocity = Vector2.zero;
        while (barrel.transform.localScale.x >= 0.301f)
        {
            //barrel.transform.localScale -= new Vector3(0.1f, 0, 0) * Time.deltaTime;
            barrel.transform.localScale = Vector3.SmoothDamp(barrel.transform.localScale, new Vector3(0.3f, 1,1), ref velocity, 0.0001f);
            yield return null;
        }

        while(barrel.transform.localScale.x <= 0.599f)
        {
            barrel.transform.localScale = Vector3.SmoothDamp(barrel.transform.localScale, new Vector3(0.6f, 1, 1), ref velocity, 0.1f);
            yield return null;
        }

        barrel.transform.localScale = new Vector3(0.6f, 1, 1);

    }
}
