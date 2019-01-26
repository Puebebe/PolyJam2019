using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpScript : MonoBehaviour {

    public Animator anim;
    public Transform[] targetsList;
    Camera cam;

    public void StartUpgrade(float number)
    {
       Vector3 targ = targetsList[(int)number].position;
       
       Vector2 trag2D = cam.ScreenToWorldPoint(new Vector3(targ.x, targ.y, cam.nearClipPlane));


       Sequence mySequence = DOTween.Sequence();
       mySequence.Append(transform.DOMove(new Vector3(trag2D.x - 15, trag2D.y, 0), 0f));
       mySequence.Append(transform.DOMove(new Vector3(trag2D.x, trag2D.y, 0), 2f));
       mySequence.AppendCallback(AttackAnim);
       mySequence.AppendInterval(0.5f);
       mySequence.Append(transform.DOMove(new Vector3(trag2D.x + 15, trag2D.y, 0), 2f));
    }

    void AttackAnim()
    {
        anim.SetTrigger("AttackTrig");
    }

    // Use this for initialization
    void Start () {
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
