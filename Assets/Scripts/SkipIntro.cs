using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Cancel"))
            GameStateManager.Singleton.skipIntro();
	}
}
