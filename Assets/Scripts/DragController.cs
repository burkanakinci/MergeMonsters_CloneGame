using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour
{
    private MergeController mergeController;
    private RaycastHit hit;
    private Ray ray;
    [SerializeField] private Transform dragParent;
    private bool isDragging;
    private void Start()
    {
        isDragging = false;

        mergeController = GetComponent<MergeController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) &&
        !EventSystem.current.IsPointerOverGameObject())
        {
            mergeController.clickDownGrid = null;
            mergeController.clickUpGrid = null;

            ClickedOnGrid();

            isDragging = true;
        }
        if (Input.GetKey(KeyCode.Mouse0) && isDragging)
        {
            DraggedOnGrid();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && isDragging)
        {
            ClickUpOnGrid();

            isDragging = false;
        }
    }
    private void ClickedOnGrid()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) &&
            hit.collider.CompareTag("GridCell"))
        {
            dragParent.position = hit.point;

            mergeController.clickDownGrid =
                ObjectPool.Instance.GetClickedGrid(hit.transform);

            for (int i = mergeController.clickDownGrid.soldiersOnGrid.Count - 1; i >= 0; i--)
            {
                mergeController.clickDownGrid.soldiersOnGrid[i].transform.SetParent(dragParent);

                mergeController.clickDownGrid.soldiersOnGrid[i].PlayJumpAnimation();

                mergeController.clickDownGrid.soldiersOnGrid[i].transform.
                    DOLocalJump(mergeController.clickDownGrid.soldiersOnGrid[i].transform.localPosition + Vector3.up * 1f, 1f, 1, 0.7f);
            }
        }

    }
    private void DraggedOnGrid()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            dragParent.position = hit.point;
        }
    }
    private void ClickUpOnGrid()
    {
        if (Physics.Raycast(dragParent.position + Vector3.up * 2f,
                    (-transform.up),
                    out hit,
                    10f) &&
            hit.collider.CompareTag("GridCell"))
        {
            mergeController.clickUpGrid =
                ObjectPool.Instance.GetClickedGrid(hit.transform);

            mergeController.MergeSoldiers();
        }
        else
        {
            mergeController.ClickGridBackPlacement();
        }
    }
}
