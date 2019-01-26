using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public ImpScript imp;

	// Use this for initialization
	void Start () {
        imp.StartUpgrade(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
