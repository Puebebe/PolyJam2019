using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCombos : MonoBehaviour {

    public static UnlockCombos Singleton;
    public snailControler Player;
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
            Player = GameObject.FindObjectOfType<snailControler>();
        }
        
    }

    public void TeachNewCombo()
    {
        //unlock new skill
        if (Player.unlockedSkills < 5)
        {
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
        teach = true;
    }

    private void Update()
    {
        if (teach)
        {
            if (Player.LastSkil == ("Skill nr " + waitForCombo))
            {
                teach = false;
                GameStateManager.Singleton.NextScene();
            }
        }
    }
}
