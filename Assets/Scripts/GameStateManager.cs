using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Singleton;

    [SerializeField] private GameObject[] FixedContent;
    [SerializeField] public snailControler player;
    [SerializeField] public int startPlayerHP=100, startOpponentHP=100, levelHPOpponentMultiplier=10;

    [SerializeField] public GameObject EnemyHurtParticle;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogError("Attempted to add second GameStateManager Singleton!");
            DestroyImmediate(this.gameObject);
        }
    }

    private int _playerHp = 100;
    public int PlayerHp
    {
        get
        {
            return _playerHp;
        }
        set
        {
            if (value <= 0)
            {
                
                PlayerDeath();
            }
            UIManager.Singleton.UpdatePlayerHP(value);
            _playerHp = value;
            
            
        }
    }
    private int _enemyHP = 100;
    public int OpponentHp {
        get
        {
            return _enemyHP;
        }
        set
        {
            
            if (value <= 0)
            {
                
                EnemyDeath();
            }

            UIManager.Singleton.UpdateEnemyHP(value);
            _enemyHP = value;

        }
    }

    private void resetHP()
    {
        UIManager.Singleton.ClearHealthData();
        PlayerHp = startPlayerHP;
        OpponentHp = startOpponentHP + (currentScene * levelHPOpponentMultiplier);
    }

    int currentScene = 0;
    //list of root objects to spawn
    [SerializeField]
    [Tooltip("feed me gameobjects to spawn, ake sure they have lots of childern in them.")]
    private GameObject[] ScenesArray;

    //list of object to destroy in next scene change
    private List<GameObject> currentlySpawnedObjects = new List<GameObject>();

    [Space(20)]
    [SerializeField] GameObject GameOverScreen;

    private void PlayerDeath()
    {
        Time.timeScale = 0;
        Instantiate(GameOverScreen, transform);
    }

    private void EnemyDeath()
    {
        //destroy every currently spawned object
        for (int i = 0; i < currentlySpawnedObjects.Count; i++)
        {
            Destroy(currentlySpawnedObjects[i]);
        }
        if (!UnlockCombos.Singleton.teach)
        {
            UnlockCombos.Singleton.TeachNewCombo();
        }
        
    }
  

    public void RestartScene()
    {
        //Next scene pushes current scene + 1, so we need to bring it back to the scene we got killed in
        currentScene -= 1;
        //and just load the scene as usual
        NextScene();
    }

    //start current scene
    private void Start()
    {
        StartCoroutine(DestroyIntro());
    }

    IEnumerator DestroyIntro()
    {
        yield return new WaitForSecondsRealtime(8f);
        if (GameObject.FindObjectOfType<VideoPlayer>() != null)
        {
            Destroy(GameObject.FindObjectOfType<VideoPlayer>().gameObject);
            NextScene();
        }   
    }

    public void skipIntro()
    {
        Destroy(GameObject.FindObjectOfType<VideoPlayer>().gameObject);
        NextScene();
    }

    public void NextScene()
    {
        Time.timeScale = 1;

        resetHP();
            
        //destroy every currently spawned object
        for (int i = 0; i < currentlySpawnedObjects.Count; i++)
        {
            Destroy(currentlySpawnedObjects[i]);
        }
        //clear the list of currently spawned obejcts
        currentlySpawnedObjects = new List<GameObject>();
        //spawn new objects and add them to the list

        GameObject toSpawn = ScenesArray[currentScene];
        bool noUi = toSpawn.tag == "CutScene"; 
  
        foreach (var content in FixedContent)
        {
            try
            {
                content.SetActive(!noUi);
            }
            catch (System.Exception)
            {
                
            }
           
        }             

        currentlySpawnedObjects.Add(Instantiate(toSpawn, toSpawn.transform.position, toSpawn.transform.rotation));
        
        currentScene++;
    }
}
