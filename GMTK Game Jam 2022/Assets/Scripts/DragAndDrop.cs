using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;

    private Vector2 velocity = Vector2.zero;
    public float mousDragSpeed;


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
        while(mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position, ray.origin, ref velocity, mousDragSpeed);
            yield return null;

        }
    }
}
