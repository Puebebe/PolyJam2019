using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public int PlayerHp=100;
    public int OpponentHp=100;

    int currentScene = 0;
    //list of root objects to spawn
    [SerializeField]
    [Tooltip("feed me gameobjects to spawn, ake sure they have lots of childern in them.")]
    private GameObject[] ScenesArray;
    //list of object to destroy in next scene change
    private List<GameObject> currentlySpawnedObjects = new List<GameObject>();
  

    public void NextScene()
    {
        //scene 1 is an exception because there is nothing to destroy yet
        if (currentScene == 0)
        {

                //prep the object
                GameObject toSpawn = ScenesArray[0];
                //spawn it and add it to the list of currently spawned objects
                currentlySpawnedObjects.Add(Instantiate(toSpawn, toSpawn.transform.position, toSpawn.transform.rotation));
        }
        else
        {
            
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
        }
        currentScene++;
    }
}
