using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockCombos : MonoBehaviour
{
    public static UnlockCombos Singleton;
    public SnailController Player;
    public ImpScript imp;
    public bool teach = false;

    private int waitForCombo = 1;
    private bool waitForImp = true;
    private bool glowing = true;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogError("Attempted to create second UnlockCombos Singleton");
            DestroyImmediate(this.gameObject);
        }
    }

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindObjectOfType<SnailController>();
        }
    }

    private void Update()
    {
        if (teach && !waitForImp)
        {
            if (!glowing)
            {
                glowing = true;
                StartCoroutine(TutorialGlow());
            }
            if (Player.lastSkill == ("Skill nr " + waitForCombo))
            {
                teach = false;
                glowing = true;
                waitForImp = true;
                StartCoroutine(ShowcaseSkill());
            }
        }
    }

    public void TeachNewCombo()
    {
        //unlock new skill
        if (Player.unlockedSkills < 5)
        {
            glowing = true;
            teach = true;
            StartCoroutine(WaitForImp());
            imp.StartUpgrade(Player.unlockedSkills);
        }
        else
        {
            GameStateManager.Singleton.NextScene();
        }
    }

    IEnumerator WaitForImp()
    {
        yield return new WaitForSecondsRealtime(2f);
        Player.unlockedSkills += 1;
        PanelManager.Singleton.ForceUpdatePanels();
        waitForCombo = Player.unlockedSkills - 1;
        glowing = false;
        waitForImp = false;
    }

    IEnumerator ShowcaseSkill()
    {
        yield return new WaitForSecondsRealtime(2f);
        GameStateManager.Singleton.NextScene();
    }

    IEnumerator TutorialGlow()
    {
        var panel = PanelManager.Singleton.Panels[Player.unlockedSkills - 1].GetComponent<Image>();
        for (int i = 0; i < 10; i++)
        {
            panel.color = Color.Lerp(Color.white, Color.magenta, i / 10f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        for (int i = 0; i < 10; i++)
        {
            panel.color = Color.Lerp(Color.magenta, Color.white, i / 10f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        panel.color = Color.white;
        glowing = false;
    }
}
