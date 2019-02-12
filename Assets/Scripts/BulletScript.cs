using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletScript : MonoBehaviour {

    [SerializeField] float speed = 100f;
    public int damage = 10;


    private void Start()
    {
        transform.DORotate(new Vector3(0,0,1400), 2.5f, RotateMode.Fast);
    }



    void Update () {
        this.gameObject.transform.localPosition -= new Vector3(speed * Time.deltaTime,0f,0f);

        if(this.gameObject.transform.localPosition.x < - 6f)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<SnailController>().ApplyDamage(damage);

            Destroy(this.gameObject);
            //destroy can wait a couple of frames before actually destroying the object, deactivating the bullet to avoid multiple damage calls from single bullet
            this.gameObject.SetActive(false);
        }
    }
}
