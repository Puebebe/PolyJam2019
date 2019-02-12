using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ninja : MonoBehaviour
{
    [SerializeField] float attackTimer = 2;
    [SerializeField] Transform attackSpawnPos;
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
        anim.SetTrigger("PrepTrig");
        float rndDelay = Random.Range(0f, 1.5f);
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(1.30f);
        mySequence.AppendCallback(AttackAnimation);
        mySequence.AppendInterval(attackDelay);
        mySequence.OnComplete(SpawnWave);
        delay = attackTimer + rndDelay;
    }

    public void AttackAnimation()
    {
        anim.SetTrigger("AttackTrig");
    }

    public void SpawnWave()
    {    
        Instantiate(attack1Prefab, attackSpawnPos, true);
    }
}

