﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class snailControler : MonoBehaviour {

    public int baseDamage = 10;
    public int unlockedSkills = 1;
    public bool isUntouchable = false;

    //[SerializeField] GameObject HpBarScript; - NOW WE USE SINGLETON
    //[SerializeField] GameObject GameStateManagerScript; - NOW WE USE SINGLETON


    [SerializeField] Animator anim;
    [SerializeField] float jumpPower;
    [SerializeField] float skillDelayTime;
    [SerializeField] float animationHitDelay;
    [SerializeField] string[] skillsLeft;
    [SerializeField] string[] skillsRight;
    [SerializeField] public string LastSkil = "-";
    [SerializeField] GameObject FireParticle;

    public int[] skillsStatus;
    float leftX, leftY;
    float rightX, rightY;
    Vector3 startPos;
    Vector3 startScale;
    bool jump;
    float jumpActualPower;
    float skillDelay;
    float HitDelay;
    private GameObject EnemyHurtParticle;

    // Use this for initialization
    void Start () {
        EnemyHurtParticle = GameStateManager.Singleton.EnemyHurtParticle;
        skillsStatus = new int[skillsLeft.Length];
        for (int i = 0; i < skillsStatus.Length; i++)
            {
            skillsStatus[i] = 0;
            }
        startPos = this.gameObject.transform.localPosition;
        startScale = this.gameObject.transform.localScale;
        jump = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetJoystickNames().Length > 0 && ( Input.GetJoystickNames()[0] == "Controller (XBOX 360 For Windows)" || Input.GetJoystickNames()[0] == "Controller (Xbox 360 Wireless Receiver for Windows)"))
        {
            leftX = Input.GetAxis("HorizontalJoystickL");
            leftY = Input.GetAxis("VerticalJoystickL");
            rightX = Input.GetAxis("HorizontalJoystickR");
            rightY = Input.GetAxis("VerticalJoystickR");
        }
        else
        {
            leftX = Mathf.Clamp(Input.GetAxis("HorizontalL") * 5f, -1f, 1f);
            leftY = Mathf.Clamp(Input.GetAxis("VerticalL") * 5f, -1f, 1f);
            rightX = Mathf.Clamp(Input.GetAxis("HorizontalR") * 5f, -1f, 1f);
            rightY = Mathf.Clamp(Input.GetAxis("VerticalR") * 5f, -1f, 1f);
        }

        if(jump)
        {            
            this.gameObject.transform.localPosition += new Vector3(0, jumpActualPower, 0);
            jumpActualPower -= Time.deltaTime;
            if(jumpActualPower <= - jumpPower)
            {
                jump = false;
                Normal();
            }
        }
        else if (leftY == 1 && rightY == 1)
        {
            Jump();
        }
        else if (leftY == -1 && rightY == -1)
        {
            Dodge();
        }
        else if (leftX == -1 && rightX == -1)
        {
            Block();
        }
        else
        {
            Normal();
        }
        if(HitDelay > 0)
        {
            HitDelay -= Time.deltaTime;
            if (HitDelay <= 0)
            {
                for (int i = 0; i < skillsStatus.Length; i++)
                {
                    skillsStatus[i] = 0;
                }
            }
        }
        else if (skillDelay > 0)
        {
            CheckStatus();
            skillDelay -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < skillsStatus.Length; i++)
            {
                skillsStatus[i] = 0;
            }
            //Debug.Log("-----------------------");
            LastSkil = "";
            skillDelay = skillDelayTime;
        }
    }


    void Dodge()
    {
        this.gameObject.transform.localScale = new Vector3(startScale.x, startScale.y / 1.5f, startScale.z);
        this.gameObject.transform.localPosition = startPos - new Vector3(0f, 0.4f, 0f);
    }

    void Jump()
    {
        anim.SetTrigger("TrigJump");
        jumpActualPower = jumpPower;
        jump = true;
    }

    private void Block()
    {
        anim.SetBool("BoolBlock", true);
    }

    void Normal()
    {
        this.gameObject.transform.localScale = startScale;
        this.gameObject.transform.localPosition = startPos;
        anim.SetBool("BoolBlock", false);
    }

    void CheckStatus()
    {
        int LeftStatus = 0;
        int RightStatus = 0;
        bool LeftOK=false, RightOK=false;

        if (Mathf.Round(leftX) == 1)
            LeftStatus = 3;
        else if (Mathf.Round(leftX) == -1)
            LeftStatus = 2;
        if (Mathf.Round(leftY) == 1)
            LeftStatus = 1;
        else if (Mathf.Round(leftY) == -1)
            LeftStatus = 4;
        if (Mathf.Round(rightX) == 1)
            RightStatus = 3;
        else if (Mathf.Round(rightX) == -1)
            RightStatus = 2;
        if (Mathf.Round(rightY) == 1)
            RightStatus = 1;
        else if (Mathf.Round(rightY) == -1)
            RightStatus = 4;

        //Debug.Log("Right Status " + RightStatus);
        //Debug.Log("Left Status " + LeftStatus);
        for (int i = 0; i < unlockedSkills; i++)
        {
            LeftOK = false;
            RightOK = false;

            if (skillsLeft[i][skillsStatus[i]] - '0' == LeftStatus)
            {
                LeftOK = true;
            }
            else if(LeftStatus != 0 && skillsStatus[i] != 0 && skillsLeft[i][skillsStatus[i]-1] - '0' != LeftStatus)
            {
                skillsStatus[i] = 0;
            }

            if(skillsRight[i][skillsStatus[i]] - '0' == RightStatus)
            {
                RightOK = true;
            }
            else if(RightStatus != 0 && skillsStatus[i] != 0 && skillsRight[i][skillsStatus[i] - 1] - '0' != RightStatus)
            {
                skillsStatus[i] = 0;
            }
            if(RightOK && LeftOK)
            {
                skillsStatus[i]++;
                skillDelay = skillDelayTime;
            }
            if(skillsStatus[i] == skillsLeft[i].Length)
            {
                Debug.Log("<b>" + i + " wykonano</b>");
                LastSkil = "Skill nr " + i;

                #region Instantiate Particles

                if (GameStateManager.Singleton.OpponentHp > 0)
                {
                    Instantiate(EnemyHurtParticle, new Vector3(4f, 2f, 0f), EnemyHurtParticle.transform.rotation);
                }

                if (LastSkil == "Skill nr 3")
                {
                    Instantiate(FireParticle, FireParticle.transform.position, FireParticle.transform.rotation);
                }


                #endregion

                #region bayblade

                Debug.Log(LastSkil);
                if (LastSkil == "Skill nr 1")
                {
                    Debug.Log("test");
                    Sequence baybladeSeq = DOTween.Sequence();
                    baybladeSeq.Append(anim.gameObject.transform.DOMove(new Vector3(-4f + (2.1f * 4.5f), -0.2f, 0f), 0.8f));
                    baybladeSeq.Append(anim.gameObject.transform.DOMove(new Vector3(-4f + (0.654f * 4.5f), -0.2f, 0f), 0.5f));
                }
                

                #endregion

                int OpponentHp = GameStateManager.Singleton.OpponentHp;
                int rnd = UnityEngine.Random.Range(-i-1, i+2);
                GameStateManager.Singleton.OpponentHp = OpponentHp - (i + 1) * baseDamage + rnd;
                HitDelay = animationHitDelay;
                for (int x = 0; x < skillsStatus.Length; x++)
                {
                    if(x != i)
                        skillsStatus[x] = 0;
                }

                UpdateSnailAnimation(i);
            }
        }
       
    }

    void UpdateSnailAnimation(int i)
    {
        switch (i)
        {
            case 0:
                anim.SetTrigger("TrigAttack1");
                break;
            case 1:
                anim.SetTrigger("TrigBayblade");
                break;
            case 2:
                anim.SetTrigger("TrigAttack1");
                break;
            case 3:
                anim.SetTrigger("TrigAttack1");
                break;
            case 4:
                anim.SetTrigger("TrigAttack1");
                break;
            case 5:
                anim.SetTrigger("TrigAttack1");
                break;

        }

    }

    public void ApplyDamage(int damage)
    {
        if (!isUntouchable)
        {
            GameStateManager.Singleton.PlayerHp -= damage;
            Instantiate(GameStateManager.Singleton.EnemyHurtParticle, this.transform);
        }
    }

}
