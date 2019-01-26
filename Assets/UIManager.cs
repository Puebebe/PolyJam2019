using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Singleton;

    [SerializeField]
    private Image PlayerHP;
    private RectTransform PlayerHPRT;
    [SerializeField]
    private Image EnemyHP;
    private RectTransform EnemyHPRT;

    private List<int> PlayerHPList = new List<int>();
    private List<int> EnemyHPList = new List<int>();

    private int PlayerHPMaxSize;
    private int EnemyHPMaxSize;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogError("Attempted to add second UIManager Singleton!");
            DestroyImmediate(this.gameObject);
        }

        if (PlayerHP == null)
        {
            PlayerHP = FindImageOfName("PlayerHealthBar");
        }
        if (EnemyHP == null)
        {
            PlayerHP = FindImageOfName("EnemyHealthBar");
        }

        PlayerHPRT = PlayerHP.GetComponent<RectTransform>();
        EnemyHPRT = EnemyHP.GetComponent<RectTransform>();

        PlayerHPMaxSize = Mathf.FloorToInt(PlayerHPRT.sizeDelta.x);

        EnemyHPMaxSize = Mathf.FloorToInt(EnemyHPRT.sizeDelta.x);
    }

    private Image FindImageOfName(string imageName)
    {
        var possibleObjects = GameObject.FindObjectsOfType<Image>();
        for (int i = 0; i < possibleObjects.Length; i++)
        {
            if (possibleObjects[i].name == imageName)
            {
                return possibleObjects[i];
            }
        }
        return null;
    }

    public void UpdatePlayerHP(int hp)
    {
        PlayerHPList.Add(hp);
        if (PlayerHPList.Count == 1)
        {
            InitalizePlayerHP();
        }
        else
        {
            StartCoroutine(animateImage(PlayerHPRT, PlayerHPMaxSize, hp, PlayerHPList[PlayerHPList.Count - 2], PlayerHPList[0]));
        }
    }

    public void UpdateEnemyHP(int hp)
    {
        EnemyHPList.Add(hp);
        if (EnemyHPList.Count == 1)
        {
            InitalizeEnemyHP();
        }
        else
        {
            StartCoroutine(animateImage(EnemyHPRT, EnemyHPMaxSize, hp, EnemyHPList[EnemyHPList.Count - 2], EnemyHPList[0]));
        }
    }

    private void InitalizeEnemyHP()
    {
        Debug.Log("Inicjalizacja EnemyHP");
    }

    private void InitalizePlayerHP()
    {
        //obecnie gówno trzeba robić w inicjalizacji lul
        Debug.Log("inicjalizacja PlayerHP");
    }

    IEnumerator animateImage(RectTransform img, int maxSize, int newHP, int oldHP, int maxHP)
    {
        Debug.Log("animuje");
        Debug.Log("MaxSize: " + maxSize + " newHP: " + newHP + " oldHP: " + oldHP + " maxHP: " + maxHP);
        //Debug.Log("SizeDelta: " + img.sizeDelta.x);
        var rect = img.sizeDelta;
        for (int i = 0; i < 2; i++)
        {
            rect.x = (float)maxSize * ((float)newHP / (float)maxHP);
            img.sizeDelta = rect;
            yield return new WaitForSecondsRealtime(0.1f);
            rect.x = (float)maxSize * ((float)oldHP / (float)maxHP);
            img.sizeDelta = rect;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        rect.x =(float)maxSize * ((float)newHP / (float)maxHP);
        img.sizeDelta = rect;

    }

    public void ForceUpdateUI()
    {
        PlayerHPList.Add(PlayerHPList[PlayerHPList.Count - 1]);
        EnemyHPList.Add(EnemyHPList[EnemyHPList.Count - 1]);
    }

    int x = 0;

    public void Test()
    {
        UpdatePlayerHP(100 - x);
        UpdateEnemyHP(100 - x);
        x += 20;
    }

    public void ClearHealthData()
    {
        PlayerHPList = new List<int>();
        EnemyHPList = new List<int>();

        PlayerHPRT.sizeDelta = new Vector2(PlayerHPMaxSize, PlayerHPRT.sizeDelta.y);
        EnemyHPRT.sizeDelta = new Vector2(EnemyHPMaxSize, EnemyHPRT.sizeDelta.y);
    }
    
}
