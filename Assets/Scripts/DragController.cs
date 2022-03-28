using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private Transform draggedTransform;
    private RaycastHit hit;
    private Ray ray;
    private float screenWidth, changeOfMousePos, horizontalMovementChange;
    private Vector3 firstMousePos;
    [SerializeField] private float horizontalSpeed = 8f;
    private void Start()
    {
        screenWidth = Screen.width;

        draggedTransform = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                draggedTransform = hit.transform;
            }
        }
        if (Input.GetMouseButton(0))
        {
            horizontalMovementChange = 0;

            changeOfMousePos = Input.mousePosition.x - firstMousePos.x;
            if (Mathf.Abs(changeOfMousePos) > 0.1f)
            {

                horizontalMovementChange = (changeOfMousePos * 1 / screenWidth);
                firstMousePos = Input.mousePosition;
            }

            draggedTransform.localPosition = new Vector3(

                Mathf.Lerp(draggedTransform.localPosition.x, draggedTransform.localPosition.x + (horizontalMovementChange), horizontalSpeed * Time.fixedDeltaTime),
                draggedTransform.localPosition.y,
                draggedTransform.localPosition.z);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && draggedTransform != null)
        {
            horizontalMovementChange = 0;
            firstMousePos = Input.mousePosition;

            draggedTransform = null;
        }
    }
}
