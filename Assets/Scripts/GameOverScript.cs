using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour {

    GameObject cam;
    Vector3 cameraStartPos;
    Canvas canvas;

    public static bool isGameOver = false;

    public TMPro.TextMeshPro respawnText;
    int delay = 3;

    private void Awake()
    {
        if (isGameOver)
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
        else
        {
            isGameOver = true;
        }
    }

    void Start () {
        //assign
        cam = Camera.main.gameObject;
        canvas = UIManager.Singleton.gameObject.GetComponent<Canvas>();

        cameraStartPos = cam.transform.position;

        //display
        cam.transform.position += Vector3.up * 20f;
        canvas.enabled = false;

        StartCoroutine(delayCoroutine());
	}

    IEnumerator delayCoroutine()
    {
        while(delay > 0)
        {
            respawnText.text = "Next Chance in: " + delay;
            yield return new WaitForSecondsRealtime(1f);
            delay -= 1;
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        //recover
        cam.transform.position = cameraStartPos;
        canvas.enabled = true;
        isGameOver = false;
        GameStateManager.Singleton.RestartScene();
    }
}
