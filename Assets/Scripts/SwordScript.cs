using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{

    [SerializeField] float maxlifeTime = 0.5f;
    private float currentLifeTime = 0;

    private void Start()
    {
        currentLifeTime = 0;
    }

    void Update()
    {    
        if (currentLifeTime>= maxlifeTime)
        {
            Destroy(this.gameObject);
        }

        currentLifeTime += Time.deltaTime;
    }
}
