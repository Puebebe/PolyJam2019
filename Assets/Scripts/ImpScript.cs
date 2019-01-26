using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpScript : MonoBehaviour {

    public Animator anim;
    public Transform[] targetsList;

    public void StartUpgrade(float number)
    {
       Vector3 targ = targetsList[(int)number].position;
       Sequence mySequence = DOTween.Sequence();
       mySequence.Append(transform.DOMove(new Vector3(targ.x - 15, targ.y, 0), 0f));
       mySequence.Append(transform.DOMove(new Vector3(targ.x, targ.y, 0), 2f));
       mySequence.AppendCallback(AttackAnim);
       mySequence.AppendInterval(0.5f);
       mySequence.Append(transform.DOMove(new Vector3(targ.x + 15, targ.y, 0), 2f));
    }

    void AttackAnim()
    {
        anim.SetTrigger("AttackTrig");
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
