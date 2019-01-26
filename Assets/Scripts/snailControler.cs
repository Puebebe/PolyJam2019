using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class snailControler : MonoBehaviour {

    [SerializeField] GameObject snail;
    [SerializeField] float jumpPower;
    [SerializeField] float skillDelayTime;
    [SerializeField] string[] skillsLeft;
    [SerializeField] string[] skillsRight;
    [SerializeField] string LastSkil = "-";
    int[] skillsStatus;
    float leftX, leftY;
    float rightX, rightY;
    Vector3 startPos;
    Vector3 startScale;
    bool jump;
    float jumpActualPower;
    float skillDelay;

	// Use this for initialization
	void Start () {
        skillsStatus = new int[skillsLeft.Length];
        for (int i = 0; i < skillsStatus.Length; i++)
            {
            skillsStatus[i] = 0;
            }
        startPos = snail.transform.position;
        startScale = snail.transform.localScale;
        jump = false;
    }
	
	// Update is called once per frame
	void Update () {
        leftX = Input.GetAxis("HorizontalJoystickL");
        leftY = Input.GetAxis("VerticalJoystickL");
        rightX = Input.GetAxis("HorizontalJoystickR");
        rightY = Input.GetAxis("VerticalJoystickR");
        if(jump)
        {
            snail.transform.position += new Vector3(0, jumpActualPower, 0);
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
            Doge();
        }
        else
        {
            Normal();
        }
        if (skillDelay > 0)
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
            Debug.Log("-----------------------");
            LastSkil = "";
            skillDelay = skillDelayTime;
        }
    }

    void Doge()
    {
        snail.transform.localScale = startScale - new Vector3(0, 20, 0);
        snail.transform.position = startPos - new Vector3(0, 1, 0);
    }

    void Jump()
    {
        snail.transform.localScale = startScale - new Vector3(0, 20, 0);
        jumpActualPower = jumpPower;
        jump = true;
    }

    void Normal()
    {
        snail.transform.localScale = startScale;
        snail.transform.position = startPos;
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
        for (int i = 0; i < skillsLeft.Length; i++)
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
                skillsStatus[i] = 0;
            }
        }
       
    }
}
