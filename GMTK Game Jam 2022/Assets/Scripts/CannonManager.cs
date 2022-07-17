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
    private Transform firePoint;

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
        diceSlot.TryGetComponent<ReceiverManager>(out var receiverManager);
        if (receiverManager.attached)
        {
            currentDiceValue = receiverManager.diceValue;
            receiverManager.attached = false;
            Destroy(receiverManager.currentDiceAttached);
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
