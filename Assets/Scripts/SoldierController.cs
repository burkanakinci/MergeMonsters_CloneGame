using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] private int soldierLevel;
    [SerializeField] private int soldierSpawnPrice;
    [SerializeField] private ParticleSystem spawnParticle, runParticle;
    private Animator soldierAnimator;

    private void Start()
    {
        soldierAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlaySapawnParticle();
    }
    public void PlayRunParticle()
    {
        runParticle.Play();
    }
    public void PlaySapawnParticle()
    {
        spawnParticle.Play();
    }
    public int GetSoldierLevel()
    {
        return soldierLevel;
    }
    public int GetSoldierSpawnPrice()
    {
        return soldierSpawnPrice;
    }

    public void PlayJumpAnimation()
    {
        soldierAnimator.Play("Jumping", 0, 0.0f);
    }
    public void PlayWalkingAnimation()
    {
        soldierAnimator.Play("Walking", 0, 0.0f);
    }
    public void PlayFightAnimation()
    {
        soldierAnimator.SetBool("Fight", true);
    }
    public void PlayDeathAnimation()
    {
        soldierAnimator.SetBool("Death", true);
    }
    public void PlayRunAnimation()
    {
        PlayRunParticle();
        soldierAnimator.Play("Running", 0, 0.0f);
    }
    public void PlayIdleAnimation()
    {
        soldierAnimator.SetBool("Death", false);
        soldierAnimator.SetBool("Fight", false);
        soldierAnimator.Play("Idle", 0, 0.0f);
    }
}
