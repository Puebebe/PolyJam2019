using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class SnailController : MonoBehaviour
{
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
    [SerializeField] public int[] skillsStatus;
    [SerializeField] public string lastSkill = "-";
    [Header("Attacks - particles")]
    [SerializeField] GameObject laser1;
    [SerializeField] GameObject FireParticle;
    [SerializeField] GameObject laser2;
    [Header("Sounds")]
    [SerializeField] GameObject audioJump;
    [SerializeField] GameObject audioBlock;

    float leftX, leftY;
    float rightX, rightY;
    Vector3 startPos;
    Vector3 startScale;
    bool jump;
    float jumpActualPower;
    float skillDelay;
    float hitDelay;
    GameObject EnemyHurtParticle;

    // Use this for initialization
    void Start ()
    {
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
        if (Input.GetKey(KeyCode.BackQuote) && Input.GetKey(KeyCode.K))
        {
            int OpponentHp = GameStateManager.Singleton.OpponentHp;
            GameStateManager.Singleton.OpponentHp = OpponentHp - 100;
        }

        if (Input.GetJoystickNames().Length > 0 && ( Input.GetJoystickNames()[0] == "Controller (XBOX 360 For Windows)" || Input.GetJoystickNames()[0] == "Controller (Xbox 360 Wireless Receiver for Windows)"))
        {
            leftX = Input.GetAxis("HorizontalJoystickL");
            leftY = Input.GetAxis("VerticalJoystickL");
            rightX = Input.GetAxis("HorizontalJoystickR");
            rightY = Input.GetAxis("VerticalJoystickR");
        }
        else
        {
            leftX = Math.Sign(Input.GetAxis("HorizontalL"));
            leftY = Math.Sign(Input.GetAxis("VerticalL"));
            rightX = Math.Sign(Input.GetAxis("HorizontalR"));
            rightY = Math.Sign(Input.GetAxis("VerticalR"));
        }

        if (hitDelay > 0)
        {
            hitDelay -= Time.deltaTime;
            if (hitDelay <= 0)
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
            lastSkill = "";
            skillDelay = skillDelayTime;
        }

        if (jump)
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
            bool isBusy = false;
            for (int i = 0; i < unlockedSkills; i++)
            {
                if (skillsStatus[i] > 1)
                    isBusy = true;
            }
            
            if (!isBusy && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
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
    }

    void Dodge()
    {
        this.gameObject.transform.localScale = new Vector3(startScale.x, startScale.y / 1.5f, startScale.z);
        this.gameObject.transform.localPosition = startPos - new Vector3(0f, 0.4f, 0f);
    }

    void Jump()
    {
        Instantiate(audioJump);
        jumpActualPower = jumpPower;
        jump = true;
    }

    GameObject blockaudio;

    void Block()
    {
        if (blockaudio == null && !anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
        {
            blockaudio = Instantiate(audioBlock);
        }
        
        anim.SetBool("BoolBlock", true);
    }

    bool fixPosition = true;

    void Normal()
    {
        this.gameObject.transform.localScale = startScale;
        this.gameObject.transform.localPosition = startPos;

        if (fixPosition)
        {
            anim.gameObject.transform.localPosition = new Vector3(0.654f, 0f, 0f);
        }
        
        anim.SetBool("BoolBlock", false);
    }

    void CheckStatus()
    {
        int LeftStatus = 0;
        int RightStatus = 0;
        bool LeftOK = false, RightOK = false;

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

        //Debug.Log("Left Status " + LeftStatus);
        //Debug.Log("Right Status " + RightStatus);
        for (int i = 0; i < unlockedSkills; i++)
        {
            LeftOK = false;
            RightOK = false;

            if (skillsLeft[i][skillsStatus[i]] - '0' == LeftStatus)
            {
                LeftOK = true;
            }
            else if (LeftStatus != 0 && skillsStatus[i] != 0 && skillsLeft[i][skillsStatus[i] - 1] - '0' != LeftStatus)
            {
                skillsStatus[i] = 0;
            }

            if (skillsRight[i][skillsStatus[i]] - '0' == RightStatus)
            {
                RightOK = true;
            }
            else if (RightStatus != 0 && skillsStatus[i] != 0 && skillsRight[i][skillsStatus[i] - 1] - '0' != RightStatus)
            {
                skillsStatus[i] = 0;
            }

            if (RightOK && LeftOK)
            {
                skillsStatus[i]++;
                skillDelay = skillDelayTime;
            }

            if (skillsStatus[i] == skillsLeft[i].Length)
            {
                Debug.Log("wykonano atak nr " + (i + 1));
                lastSkill = "Skill nr " + i;

                if (GameStateManager.Singleton.OpponentHp > 0)
                {
                    Instantiate(EnemyHurtParticle, new Vector3(4f, 2f, 0f), EnemyHurtParticle.transform.rotation);
                }

                int OpponentHp = GameStateManager.Singleton.OpponentHp;
                int rnd = UnityEngine.Random.Range(-i-1, i+2);
                int damage  = (i + 1) * baseDamage + rnd;
                GameStateManager.Singleton.OpponentHp = OpponentHp - damage;
                hitDelay = animationHitDelay;
                for (int x = 0; x < skillsStatus.Length; x++)
                {
                    if (x != i)
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
                BeybladeAttack();
                anim.SetTrigger("TrigBeyblade");
                break;
            case 2:
                Instantiate(laser1, transform.position + new Vector3(2.5f, 0, 0), laser1.transform.rotation);
                break;
            case 3:
                Instantiate(FireParticle, FireParticle.transform.position + new Vector3(0.5f, 0, 0), FireParticle.transform.rotation);
                break;
            case 4:
                Instantiate(laser2, transform.position + new Vector3(2.5f, 0, 0), laser2.transform.rotation);            
                break;
        }
    }

    void BeybladeAttack()
    {
        fixPosition = false;
        Sequence beybladeSeq = DOTween.Sequence();
        beybladeSeq.Append(anim.gameObject.transform.DOMove(new Vector3(-4f + (2.1f * 4.5f), -0.2f, 0f), 0.8f));
        beybladeSeq.Append(anim.gameObject.transform.DOMove(new Vector3(-4f + (0.654f * 4.5f), -0.2f, 0f), 0.5f));

        StartCoroutine(forceAnimationHeight());
    }

    IEnumerator forceAnimationHeight()
    {
        yield return new WaitForSecondsRealtime(1.4f);
        fixPosition = true;
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
