using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;

    private Vector2 velocity = Vector2.zero;
    public float mouseDragSpeed;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();


    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    IEnumerator DragUpdate(GameObject clickedObject)
    {
        //float initial
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        clickedObject.TryGetComponent<IDrag>(out var IDragCompenent);
        IDragCompenent?.OnStartDrag();
        while(mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Vector2 direction = ray.origin - clickedObject.transform.position;
            rb.velocity = direction * mouseDragSpeed;
            //clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position, ray.origin, ref velocity, mouseDragSpeed);

            yield return waitForFixedUpdate;

        }
        IDragCompenent?.OnEndDrag();
    }
}
