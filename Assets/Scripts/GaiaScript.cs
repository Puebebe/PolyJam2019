using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaiaScript : MonoBehaviour
{
    [SerializeField] float attackTimer = 2;
    [SerializeField] Animator anim;
    [SerializeField] GameObject attack1Prefab;

    private float delay = 0;
    private float attackDelay = 0.25f;
    private float bigAttackDelay = 0.35f;

    void Start()
    {
        delay = attackTimer;
    }

    void Update()
    {
        if (delay < 0)
        {
            float rnd = Random.Range(0f, 1f);
            if (rnd > 0.65f)
            {
                BigAttack();
            }
            else
            {
                StartAttack();
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }

    public void BigAttack()
    {        
        anim.SetTrigger("DeadTrig");
        float rndDelay = Random.Range(0f, 1.0f);
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(bigAttackDelay);
        mySequence.OnComplete(SpawnBigWave);
        delay = attackTimer + rndDelay;
    }

    public void StartAttack()
    {
        anim.SetTrigger("AttackTrig");
        float rndDelay = Random.Range(0f, 1.0f);
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(attackDelay);
        mySequence.OnComplete(SpawnWave);
        delay = attackTimer + rndDelay;
    }

    public void SpawnWave()
    {
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
    }

    public void SpawnBigWave()
    {
        for (int i = 0; i < 11; i++)
            Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
    }
}