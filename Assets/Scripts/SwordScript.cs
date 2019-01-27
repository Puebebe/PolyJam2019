using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{

    [SerializeField] float maxlifeTime = 1.5f;
    public int damage = 10;

    private void Start()
    {
        Destroy(this.gameObject, maxlifeTime);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.GetComponent<snailControler>().ApplyDamage(damage);

            Destroy(this.gameObject, maxlifeTime);
        }
    }
}
