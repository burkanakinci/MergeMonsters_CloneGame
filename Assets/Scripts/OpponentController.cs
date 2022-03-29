using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private int opponentLevel = 1;
    private Transform targetOnFight;
    private int targetLevel;
    private bool running;
    private Animator opponentAnimator;
    private void Start()
    {
        opponentAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        running = true;
    }
    public int GetOpponentLevel()
    {
        return opponentLevel;
    }
    public void PlayRunOpponent()
    {
        opponentAnimator.Play("Running", 0, 0.0f);
    }
    public void PlayDeathOpponent()
    {
        opponentAnimator.SetBool("Death", true);
    }
    public void PlayFightOpponent()
    {
        opponentAnimator.SetBool("Fight", true);
    }

    public void FindTarget()
    {
        targetOnFight = ObjectPool.Instance.GetSoldierParentActive().GetChild(0);

        for (int i = ObjectPool.Instance.GetSoldierParentActive().childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(ObjectPool.Instance.GetSoldierParentActive().GetChild(i).position, transform.position) <
                Vector3.Distance(targetOnFight.position, transform.position))
            {
                targetOnFight = ObjectPool.Instance.GetSoldierParentActive().GetChild(i);
            }
        }

        targetLevel = targetOnFight.GetComponent<SoldierController>().GetSoldierLevel();
    }
    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameState.Fight &&
            running)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetOnFight.position, 0.8f);
            targetOnFight.position = Vector3.MoveTowards(targetOnFight.position, transform.position, 0.8f);

            if (Vector3.Distance(transform.position, targetOnFight.position) < 1.5f)
            {
                running = false;
            }
        }
    }
}
