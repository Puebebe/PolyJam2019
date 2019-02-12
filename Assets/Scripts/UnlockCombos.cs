using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockCombos : MonoBehaviour {

    public static UnlockCombos Singleton;
    public SnailController Player;
    public ImpScript imp;
    int waitForCombo = 1;
    public bool teach = false;

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

    public void TeachNewCombo()
    {
        //unlock new skill
        if (Player.unlockedSkills < 5)
        {
            glowing = true;
            teach = true;
            StartCoroutine(waitForIMP());
            imp.StartUpgrade(Player.unlockedSkills);
        }
        else
        {
            GameStateManager.Singleton.NextScene();
        }
        
        
    }

    IEnumerator waitForIMP()
    {
        yield return new WaitForSecondsRealtime(2f);
        Player.unlockedSkills += 1;
        PanelManager.Singleton.ForceUpdatePanels();
        waitForCombo = Player.unlockedSkills - 1;
        glowing = false;
        wwaitforimp = false;
    }

    private bool wwaitforimp = true;
    private bool glowing = true;

    private void Update()
    {
        if (teach && !wwaitforimp)
        {
            if (!glowing)
            {
                glowing = true;
                StartCoroutine(tutorialGlow());
            }
            if (Player.lastSkill == ("Skill nr " + waitForCombo))
            {
                teach = false;
                glowing = true;
                wwaitforimp = true;
                StartCoroutine(showcaseskill());
            }
        }
    }

    IEnumerator showcaseskill()
    {
        yield return new WaitForSecondsRealtime(2f);
        GameStateManager.Singleton.NextScene();
    }

    IEnumerator tutorialGlow()
    {
        var panel = PanelManager.Singleton.Panels[Player.unlockedSkills - 1].GetComponent<Image>();
        for (int i = 0; i < 10; i++)
        {
            panel.color = Color.Lerp(Color.white, Color.yellow, i / 10f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        for (int i = 0; i < 10; i++)
        {
            panel.color = Color.Lerp(Color.yellow, Color.white, i / 10f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        panel.color = Color.white;
        glowing = false;
    }
}
