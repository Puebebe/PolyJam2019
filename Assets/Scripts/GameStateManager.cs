using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Singleton;

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
            else{
                UIManager.Singleton.UpdatePlayerHP(value);
                _playerHp = value;
            }
            
            
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
            else
            {
                UIManager.Singleton.UpdateEnemyHP(value);
                _enemyHP = value;
            }
        }
    }

    private void resetHP()
    {
        UIManager.Singleton.ClearHealthData();
        PlayerHp = 100;
        OpponentHp = 100;
    }

    int currentScene = 0;
    //list of root objects to spawn
    [SerializeField]
    [Tooltip("feed me gameobjects to spawn, ake sure they have lots of childern in them.")]
    private GameObject[] ScenesArray;
    //list of object to destroy in next scene change
    private List<GameObject> currentlySpawnedObjects = new List<GameObject>();

    private void PlayerDeath()
    {
        //chill-liderka
        //jakieś particle?
        //i kurwa wygrana ziomek, następna scena
        RestartScene();
    }

    private void EnemyDeath()
    {
        NextScene();
        //przechujałeś
        //smutne particle
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
        currentlySpawnedObjects.Add(Instantiate(toSpawn, toSpawn.transform.position, toSpawn.transform.rotation));
        
        currentScene++;
    }
}
