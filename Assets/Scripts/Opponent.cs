using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour {

    [SerializeField] float SpawnMaxDelay = 3;
    [SerializeField] float SpawnMinDelay = 1;
    [SerializeField] GameObject [] SpawnPos;
    float delay;

    [SerializeField] Animator anim;

    [SerializeField] GameObject BulletPrefab;

    private void Start()
    {
        anim = GetComponent<Animator>();
        delay = SpawnMaxDelay;
    }

    void Update () {
		if(delay < 0)
        {
            anim.SetTrigger("AttackTrig");
            Sequence eggSequence = DOTween.Sequence();
            eggSequence.AppendInterval(0.2f);
            eggSequence.OnComplete(SpawnEgg);
            //Created.transform.localScale = Vector3.one;
            float rnd2 = Random.Range(SpawnMinDelay, SpawnMaxDelay);
            delay = rnd2;
        }
        else
        {
            delay -= Time.deltaTime;
        }

	}

    void SpawnEgg()
    {
        GameObject Created = Instantiate(BulletPrefab, this.gameObject.transform);
        int rnd = Random.Range(0, SpawnPos.Length);
        Created.transform.position = SpawnPos[rnd].transform.position;
        Created.gameObject.SetActive(true);
    }
}
