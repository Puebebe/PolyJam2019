using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTween : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MyCallback();
    }

    private void MyCallback()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMoveX(5, 1));
        mySequence.Append(transform.DOMoveX(0, 1));
        mySequence.OnComplete(MyCallback);
    }
        

    // Update is called once per frame
    void Update () {
		
	}
}
