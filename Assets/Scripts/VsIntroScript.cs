﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VsIntroScript : MonoBehaviour {

    public GameObject blackMask;

    public GameObject snail;
    public GameObject enemy;
    public Transform snailTarget;
    public Transform enemyTarget;

    // Use this for initialization
    void Start () {      
        blackMask.GetComponent<SpriteRenderer>().material.DOFade(0.0f, 0);
        blackMask.GetComponent<SpriteRenderer>().material.DOFade(1.0f, 1.2f);

        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(2.2f);
        mySequence.OnComplete(Finish);

        Sequence snailSequence = DOTween.Sequence();
        snailSequence.Append(snail.transform.DOMove(snailTarget.position, 1.3f)).SetEase(Ease.InOutCubic);

        Sequence enemySequence = DOTween.Sequence();
        enemySequence.Append(enemy.transform.DOMove(enemyTarget.position, 1.3f)).SetEase(Ease.InOutCubic);
        //transform.DO
    }
	
    void Finish()
    {
        GameStateManager.Singleton.NextScene();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
