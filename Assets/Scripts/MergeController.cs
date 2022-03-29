using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MergeController : MonoBehaviour
{
    public GridController clickDownGrid, clickUpGrid;
    private int tempSpawnLevel;
    private bool MergeCheck()
    {
        if (clickDownGrid.soldiersOnGrid.Count > 0 &&
            clickUpGrid.soldiersOnGrid.Count > 0)
        {
            if (clickDownGrid.soldiersOnGrid[0].GetSoldierLevel() == clickUpGrid.soldiersOnGrid[0].GetSoldierLevel() &&
                clickDownGrid != clickUpGrid)
            {
                return true;
            }
        }
        return false;
    }
    public void MergeSoldiers()
    {

        if (MergeCheck())
        {
            for (int i = clickDownGrid.soldiersOnGrid.Count - 1; i >= 0; i--)
            {
                clickDownGrid.soldiersOnGrid[i].transform.SetParent(ObjectPool.Instance.GetSoldierParentActive());

                clickDownGrid.soldiersOnGrid[i].PlayJumpAnimation();

                clickDownGrid.soldiersOnGrid[i].transform.
                    DOJump(ObjectPool.Instance.GetGridPos(ref clickUpGrid) - (Vector3.right * 0.6f) + (Vector3.right * 1.2f * i) + (Vector3.forward * 0.6f),
                            1f,
                            1,
                            0.7f);


            }
            StartCoroutine(MergeMovement());
        }
        else
        {
            ClickGridBackPlacement();
        }
    }
    public void ClickGridBackPlacement()
    {
        for (int i = clickDownGrid.soldiersOnGrid.Count - 1; i >= 0; i--)
        {
            clickDownGrid.soldiersOnGrid[i].transform.SetParent(ObjectPool.Instance.GetSoldierParentActive());

            clickDownGrid.soldiersOnGrid[i].PlayJumpAnimation();

            clickDownGrid.soldiersOnGrid[i].transform.
                DOJump((ObjectPool.Instance.GetGridPos(ref clickDownGrid) - (Vector3.right * 0.6f) + (Vector3.right * 1.2f * i)),
                        1f,
                        1,
                        1f);
        }
    }
    private IEnumerator MergeMovement()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = clickDownGrid.soldiersOnGrid.Count - 1; i >= 0; i--)
        {
            clickDownGrid.soldiersOnGrid[i].PlayWalkingAnimation();

            clickDownGrid.soldiersOnGrid[i].transform.
                DOMove(ObjectPool.Instance.GetGridPos(ref clickUpGrid),
                    0.75f);


        }
        for (int i = clickUpGrid.soldiersOnGrid.Count - 1; i >= 0; i--)
        {
            clickUpGrid.soldiersOnGrid[i].PlayWalkingAnimation();

            clickUpGrid.soldiersOnGrid[i].transform.
                DOMove(ObjectPool.Instance.GetGridPos(ref clickUpGrid),
                    0.75f);
        }

        StartCoroutine(MergeComplete());
    }
    private IEnumerator MergeComplete()
    {
        yield return new WaitForSeconds(1f);

        for (int i = clickDownGrid.soldiersOnGrid.Count - 1; i >= 0; i--)
        {
            ObjectPool.Instance.AddSoldierPool(clickDownGrid.soldiersOnGrid[i]);
        }
        for (int j = clickUpGrid.soldiersOnGrid.Count - 1; j >= 0; j--)
        {
            ObjectPool.Instance.AddSoldierPool(clickUpGrid.soldiersOnGrid[j]);
        }

        tempSpawnLevel = clickDownGrid.soldiersOnGrid[0].GetSoldierLevel();


        clickDownGrid.soldiersOnGrid.Clear();
        clickUpGrid.soldiersOnGrid.Clear();

        ObjectPool.Instance.ResetSoldierCounter();

        ObjectPool.Instance.SpawnSoldier(ObjectPool.Instance.GetGridPos
                                        (ref clickUpGrid),
                                        tempSpawnLevel + 1);
    }
}
