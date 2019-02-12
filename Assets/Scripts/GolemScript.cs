using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : MonoBehaviour
{
    [SerializeField] float attackTimer = 2;
    [SerializeField] Animator anim;
    [SerializeField] GameObject attack1Prefab;

    private float delay = 0;
    private float attackDelay = 0.25f;

    void Start()
    {
        delay = attackTimer;
    }

    void Update()
    {
        if (delay < 0)
        {
            StartAttack();
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }

    public void StartAttack()
    {
        anim.SetTrigger("AttackTrig");
        float rndDelay = Random.Range(0f, 1f);
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(attackDelay);
        mySequence.OnComplete(SpawnWave);
        delay = attackTimer + rndDelay;
    }

    public void SpawnWave()
    {
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
    }
}