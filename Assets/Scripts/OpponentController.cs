using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private int opponentLevel = 1;
    private SoldierController targetOnFight;
    private int targetLevel;
    private bool running;
    private Animator opponentAnimator;
    private bool isDead;
    [SerializeField] private ParticleSystem deathParticle, runParticle;
    private void Start()
    {
        opponentAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        isDead = false;

        running = true;
    }
    public void PlayDeathParticle()
    {
        deathParticle.Play();
    }
    public void PlayRunParticle()
    {
        runParticle.Play();
    }
    public int GetOpponentLevel()
    {
        return opponentLevel;
    }
    public void PlayRunOpponent()
    {
        PlayRunParticle();
        opponentAnimator.Play("Running", 0, 0.0f);
    }
    public void PlayDeathOpponent()
    {
        PlayDeathParticle();
        opponentAnimator.SetBool("Death", true);
    }
    public void PlayFightOpponent()
    {
        opponentAnimator.SetBool("Fight", true);
    }

    public void FindTarget()
    {
        targetOnFight = ObjectPool.Instance.GetSoldierParentActive().GetChild(0).GetComponent<SoldierController>();

        for (int i = ObjectPool.Instance.GetSoldierParentActive().childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(ObjectPool.Instance.GetSoldierParentActive().GetChild(i).position, transform.position) <
                Vector3.Distance(targetOnFight.transform.position, transform.position))
            {
                targetOnFight = ObjectPool.Instance.GetSoldierParentActive().GetChild(i).GetComponent<SoldierController>();
            }
        }

        targetLevel = targetOnFight.GetComponent<SoldierController>().GetSoldierLevel();

        running = true;

        PlayRunOpponent();
        targetOnFight.PlayRunAnimation();
    }
    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameState.Fight &&
            running)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetOnFight.transform.position, 0.018f);
            targetOnFight.transform.position = Vector3.MoveTowards(targetOnFight.transform.position, transform.position, 0.018f);

            if (Vector3.Distance(transform.position, targetOnFight.transform.position) < 1.5f)
            {
                running = false;

                PlayFightOpponent();

                targetOnFight.PlayFightAnimation();
                StartCoroutine(FightComplete());
            }
        }
    }

    private IEnumerator FightComplete()
    {
        yield return new WaitForSeconds(1f);
        if (opponentLevel > targetLevel)
        {
            targetOnFight.PlayDeathAnimation();
            FindTarget();
        }
        else
        {
            targetOnFight.PlayIdleAnimation();
            PlayDeathOpponent();
            isDead = true;
        }
        StartCoroutine(CheckCompleteLevel());
    }
    private IEnumerator CheckCompleteLevel()
    {
        yield return new WaitForSeconds(1f);
        if (ObjectPool.Instance.AllOpponentsDead())
        {

            UIController.Instance.ShowUISuccess();
        }
    }
    public bool GetIsDead()
    {
        return isDead;
    }
}
