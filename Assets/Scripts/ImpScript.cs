using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpScript : MonoBehaviour
{

    public Animator anim;
    public Transform[] targetsList;
    //Camera cam;

    public float xOffset;
    public float yOffset;

    public GameObject UnlockSkillParticle;


    public void StartUpgrade(float number)
    {
        Vector2 trag2D = targetsList[(int)number].position;

        //Debug.Log(targ);

        //Vector2 trag2D = cam.ScreenToWorldPoint(new Vector3(targ.x, targ.y, cam.farClipPlane));



        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMove(new Vector3(trag2D.x - 16 + xOffset, trag2D.y + yOffset, 0), 0f));
        mySequence.Append(transform.DOMove(new Vector3(trag2D.x + xOffset, trag2D.y + yOffset, 0), 2f));
        mySequence.AppendCallback(AttackAnim);
        mySequence.AppendInterval(0.5f);
        mySequence.Append(transform.DOMove(new Vector3(trag2D.x + 16 + xOffset, trag2D.y + yOffset, 0), 2f));
        StartCoroutine(SpawnParticle());
    }

    void AttackAnim()
    {
        anim.SetTrigger("AttackTrig");
    }

    IEnumerator SpawnParticle()
    {
        yield return new WaitForSecondsRealtime(2f);
        Instantiate(UnlockSkillParticle, transform);
    }


}