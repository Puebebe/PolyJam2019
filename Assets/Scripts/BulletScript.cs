using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [SerializeField] float speed = 100f;
    
    void Update () {
        Vector3 pos = this.gameObject.transform.localPosition;
        pos.x -= speed * Time.deltaTime;
        this.gameObject.transform.localPosition = pos;

        if(this.gameObject.transform.localPosition.x < - Screen.width * 2)
        {
            Destroy(this.gameObject);
        }
	}
}
