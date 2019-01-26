using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour {

    [SerializeField] float SpawnMaxDelay = 3;
    [SerializeField] float SpawnMinDelay = 1;
    [SerializeField] GameObject [] SpawnPos;
    float delay;

    [SerializeField] Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        delay = SpawnMaxDelay;
    }

    void Update () {
		if(delay < 0)
        {
            GameObject Created = (GameObject)Instantiate(Resources.Load("Bullet"));
            int rnd = Random.Range(0, SpawnPos.Length);
            Created.transform.position = SpawnPos[rnd].transform.position;
            Created.transform.parent = this.gameObject.transform;
            Created.transform.localScale = new Vector3(1, 1, 1);
            float rnd2 = Random.Range(SpawnMinDelay, SpawnMaxDelay);
            delay = rnd2;
        }
        else
        {
            delay -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetInteger("state", 0);
        }

        if (Input.GetKey(KeyCode.R))
        {
            anim.SetInteger("state", 1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetTrigger("AttackTrig");
        }
	}
}
