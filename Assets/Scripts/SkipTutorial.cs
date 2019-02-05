using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorial : MonoBehaviour
{
	void Update ()
    {
        float leftX, rightX;

        if (Input.GetJoystickNames().Length > 0 && (Input.GetJoystickNames()[0] == "Controller (XBOX 360 For Windows)" || Input.GetJoystickNames()[0] == "Controller (Xbox 360 Wireless Receiver for Windows)"))
        {
            leftX = Input.GetAxis("HorizontalJoystickL");
            rightX = Input.GetAxis("HorizontalJoystickR");
        }
        else
        {
            leftX = Mathf.Sign(Input.GetAxis("HorizontalL"));
            rightX = Mathf.Sign(Input.GetAxis("HorizontalR"));
        }

        if (leftX == -1 && rightX == -1)
        {
            Destroy(this.gameObject);
        }
    }
}
