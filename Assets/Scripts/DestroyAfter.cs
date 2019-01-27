using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

    public float delay;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, delay);
	}
	
	
}
