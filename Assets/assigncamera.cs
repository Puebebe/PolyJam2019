using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class assigncamera : MonoBehaviour {

    private void Awake()
    {
        this.gameObject.GetComponent<VideoPlayer>().targetCamera = Camera.main;
    }
}
