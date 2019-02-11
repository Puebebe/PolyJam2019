using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour
{
    [SerializeField] snailControler snailController;
    string[] codesLeft = { "1014042323BA" };
    string[] codesRight = { "1014042323BA" };
    public int[] codesStatus;
    float leftX, leftY;
    float rightX, rightY;

    void Start()
    {
        codesStatus = new int[codesRight.Length];
        for (int i = 0; i < codesStatus.Length; i++)
        {
            codesStatus[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCodes();

        if (Input.GetButton("Submit") || Input.GetKey(KeyCode.Escape))
            GameStateManager.Singleton.skipIntro();
    }

    void CheckCodes()
    {
        if (Input.GetJoystickNames().Length > 0 && (Input.GetJoystickNames()[0] == "Controller (XBOX 360 For Windows)" || Input.GetJoystickNames()[0] == "Controller (Xbox 360 Wireless Receiver for Windows)"))
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

        if (Input.GetKey(KeyCode.B) || Input.GetButton("Fire2"))
            LeftStatus = RightStatus = 'B' - '0';
        if ((Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.A)) || Input.GetButton("Fire1"))
            LeftStatus = RightStatus = 'A' - '0';

        for (int i = 0; i < codesRight.Length; i++)
        {
            LeftOK = false;
            RightOK = false;
            
            if (codesLeft[i][codesStatus[i]] - '0' == LeftStatus)
            {
                LeftOK = true;
            }
            else if (LeftStatus != 0 && codesStatus[i] != 0 && codesLeft[i][codesStatus[i] - 1] - '0' != LeftStatus)
            {
                codesStatus[i] = 0;
            }

            if (codesRight[i][codesStatus[i]] - '0' == RightStatus)
            {
                RightOK = true;
            }
            else if (RightStatus != 0 && codesStatus[i] != 0 && codesRight[i][codesStatus[i] - 1] - '0' != RightStatus)
            {
                codesStatus[i] = 0;
            }

            if (RightOK && LeftOK)
            {
                codesStatus[i]++;
            }

            if (codesStatus[i] == codesLeft[i].Length)
            {
                Debug.LogWarning("kk");
                snailController.unlockedSkills = 4;

                for (int x = 0; x < codesStatus.Length; x++)
                {
                    codesStatus[x] = 0;
                }
            }
        }
    }
}
