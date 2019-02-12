using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class PanelManager : MonoBehaviour {

    public static PanelManager Singleton;

    [SerializeField] Color NormalArrowColor;
    [SerializeField] Color ProgressArrowColor;
    [SerializeField] Color NormalPanelColor;
    [SerializeField] Color LockPanelColor;
    [SerializeField] GameObject Player;
    [SerializeField] public GameObject[] Panels;

    int[] SkillsStatus;
    int UnlockedSkils;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogError("Attempted to create second PanelManager Singleton!");
            DestroyImmediate(this.gameObject);
        }
    }

    private void Start()
    {
        SkillsStatus = Player.GetComponent<SnailController>().skillsStatus;
        UnlockedSkils = Player.GetComponent<SnailController>().unlockedSkills;

        for (int i = 0; i < UnlockedSkils; i++) 
        {
            Panels[i].GetComponent<Image>().color = NormalPanelColor;
            for (int x = 0; x < Panels[i].transform.childCount; x++)
            {
                Panels[i].transform.GetChild(x).gameObject.SetActive(true);
            }
        }

        for (int i = UnlockedSkils; i < Panels.Length; i++)
        {
            Panels[i].GetComponent<Image>().color = LockPanelColor;
            for (int x = 0; x < Panels[i].transform.childCount; x++)
            {
                Panels[i].transform.GetChild(x).gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        SkillsStatus = Player.GetComponent<SnailController>().skillsStatus;
        for (int i = 0; i < UnlockedSkils; i++)
        {
            for (int x = 0; x < SkillsStatus[i]; x++) 
            {
                Panels[i].transform.GetChild(x).GetComponent<Image>().color = ProgressArrowColor;
            }
            for (int x = SkillsStatus[i]; x < Panels[i].transform.childCount; x++)
            {
                Panels[i].transform.GetChild(x).GetComponent<Image>().color = NormalArrowColor;
            }
        }
    }

    public void ForceUpdatePanels()
    {
        Start();
    }
}
