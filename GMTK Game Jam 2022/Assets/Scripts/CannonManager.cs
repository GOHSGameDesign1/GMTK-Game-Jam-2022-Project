using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonManager : MonoBehaviour
{
    Vector2 direction;
    Vector2 mousePos;
    public GameObject barrel;
    public GameObject diceSlot;

    [SerializeField]
    private InputAction fire;

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
            receiverManager.attached = false;
            Destroy(receiverManager.currentDiceAttached);
            Debug.Log(receiverManager.diceValue);
        }
    }
}
