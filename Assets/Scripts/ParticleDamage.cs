using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour {

    public int damagePerParticle;

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<snailControler>().ApplyDamage(damagePerParticle);
        }
    }
}
