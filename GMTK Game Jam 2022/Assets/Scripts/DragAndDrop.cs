using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;

    [SerializeField]
    private InputAction mouseRightClick;

    [SerializeField]
    private InputAction shiftHold;
    public static bool shiftHeldDown;

    private Vector2 velocity = Vector2.zero;
    public float mouseDragSpeed;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    public static int orderLayer;

    public static bool isDragging;
    public static GameObject draggedObject;


    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;

        mouseRightClick.Enable();
        mouseRightClick.performed += MouseRightPressed;

        shiftHold.Enable();
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();

        mouseRightClick.Disable();
        mouseRightClick.performed -= MouseRightPressed;

        shiftHold.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        orderLayer = 0;
        isDragging = false;
        shiftHeldDown = false;
    }

    // Update is called once per frame
    void Update()
    {

        ShiftHeld();
    }

    void MousePressed(InputAction.CallbackContext context)
    {
        orderLayer += 2;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null && hit.collider.tag == "Draggable")
        {
            //StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    IEnumerator DragUpdate(GameObject clickedObject)
    {
        
        isDragging = true;
        draggedObject = clickedObject;

        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        clickedObject.TryGetComponent<IDrag>(out var IDragCompenent);
        IDragCompenent?.OnStartDrag();
        while(mouseClick.ReadValue<float>() != 0)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Vector2 direction = ray.origin - clickedObject.transform.position;
            //rb.velocity = direction.normalized * mouseDragSpeed;
            rb.position = Vector2.SmoothDamp(rb.position, ray.origin, ref velocity, 0.00001f);
            //clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position, ray.origin, ref velocity, mouseDragSpeed);

            yield return waitForFixedUpdate;

        }
        isDragging = false;
        IDragCompenent?.OnEn2dDrag();
    }

    void MouseRightPressed(InputAction.CallbackContext context) 
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        
        if (hit.collider != null && hit.collider.tag == "Draggable")
        {
            hit.collider.gameObject.TryGetComponent<DiceManager>(out var dice);
            dice?.OnRightClick();
        }
        



    }

    void ShiftHeld()
    {
        Debug.Log(shiftHeldDown);

        if(shiftHold.ReadValue<float>() != 0)
        {
            shiftHeldDown = true;
            return;
        }
        shiftHeldDown = false;
    }
}
