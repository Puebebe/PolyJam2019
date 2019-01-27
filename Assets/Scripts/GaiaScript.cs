using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaiaScript : MonoBehaviour {
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
            float rnd = Random.Range(0f, 1f);
            if (rnd > 0.6f)
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
        float rndDelay = Random.Range(0, 1);
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(0.25f);
        mySequence.OnComplete(SpawnBigWave);
        delay = attackTimmer + rndDelay;
    }

    public void StartAttack()
    {
        anim.SetTrigger("AttackTrig");
        float rndDelay = Random.Range(0, 1);
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(0.25f);
        mySequence.OnComplete(SpawnWave);
        delay = attackTimmer + rndDelay;
    }

    public void SpawnWave()
    {
        GameObject Created = Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        //Created.transform.position = Vector3.zero;
    }


    public void SpawnBigWave()
    {
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
        Instantiate(attack1Prefab, transform.position, attack1Prefab.transform.rotation);
    }
}