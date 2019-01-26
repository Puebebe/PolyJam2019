using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [SerializeField] float speed = 100f;
    
    void Update () {
        this.gameObject.transform.localPosition -= new Vector3(speed * Time.deltaTime,0f,0f);

        if(this.gameObject.transform.localPosition.x < - 6f)
        {
            Destroy(this.gameObject);
        }
	}
}
