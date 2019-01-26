using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{

    [SerializeField] float maxlifeTime = 1.5f;

    private void Start()
    {       
        Destroy(this.gameObject, maxlifeTime);
    }

    void Update()
    {            
    }
}
