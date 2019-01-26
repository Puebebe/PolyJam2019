﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    [SerializeField] Color NormalArrowColor, ProgressArrowColor, NormalPanelColor, lockPanelColor;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject[] Panels;

    int[] SkillsStatus;
    int UnlockedSkils;

    private void Start()
    {
        SkillsStatus = Player.GetComponent<snailControler>().skillsStatus;
        UnlockedSkils = Player.GetComponent<snailControler>().unlockedSkills;
        for (int i = 0; i < UnlockedSkils; i++) 
        {
            Panels[i].GetComponent<Image>().color = NormalPanelColor;
            for (int x = 0; x < 5; x++)
            {
                Panels[i].transform.GetChild(x).gameObject.SetActive(true);
            }
        }
        for (int i = UnlockedSkils; i < Panels.Length; i++)
        {
            Panels[i].GetComponent<Image>().color = lockPanelColor;
            for (int x = 0; x < 5; x++)
            {
                Panels[i].transform.GetChild(x).gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        SkillsStatus = Player.GetComponent<snailControler>().skillsStatus;
        for (int i = 0; i < UnlockedSkils; i++)
        {
            for (int x = 0; x < SkillsStatus[i]; x++) 
            {
                Panels[i].transform.GetChild(x).GetComponent<Image>().color = ProgressArrowColor;
            }
            for (int x = SkillsStatus[i]; x < 5; x++)
            {
                Panels[i].transform.GetChild(x).GetComponent<Image>().color = NormalArrowColor;
            }
        }
    }



}
