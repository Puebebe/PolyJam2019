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
            Player.unlockedSkills += 1;
            imp.StartUpgrade(Player.unlockedSkills - 1);
        }
        else
        {
            GameStateManager.Singleton.NextScene();
        }
        
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
