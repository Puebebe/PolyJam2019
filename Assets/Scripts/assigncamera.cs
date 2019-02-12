using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AssignCamera : MonoBehaviour {

    private void Awake()
    {
        this.gameObject.GetComponent<VideoPlayer>().targetCamera = Camera.main;
    }
}
