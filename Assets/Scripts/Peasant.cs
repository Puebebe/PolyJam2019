using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{

    [SerializeField] float attackTimmer = 2;
    [SerializeField] Transform atackSpawnPos;
    
    [SerializeField] Animator anim;

    [SerializeField] GameObject attack1Prefab;

    private float delay = 0;
    private float atackDelay = 0.25f;
    private void Start()
    {
        delay = attackTimmer;
    }

    void Update()
    {
        if (delay < 0)
        {
            anim.SetTrigger("AttackTrig");
            float rndDelay = Random.Range(0, 1);
            Sequence mySequence = DOTween.Sequence();
            mySequence.AppendInterval(0.25f);
            mySequence.OnComplete(SpawnWave);
            delay = attackTimmer + rndDelay;
        }
        else
        {
            delay -= Time.deltaTime;
        }

    }

    public void SpawnWave()
    {

        GameObject Created = Instantiate(attack1Prefab, atackSpawnPos, true);  
    }
}

