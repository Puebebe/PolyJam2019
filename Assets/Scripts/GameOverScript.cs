using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour {

    GameObject camera;
    Canvas canvas;

    public static bool isGameOver = false;

    Vector3 cameraStartpos;

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
        camera = Camera.main.gameObject;
        canvas = UIManager.Singleton.gameObject.GetComponent<Canvas>();

        cameraStartpos = camera.transform.position;

        //display
        camera.transform.position += Vector3.up * 20f;
        canvas.enabled = false;

        StartCoroutine(delayCouroutine());
	}

    IEnumerator delayCouroutine()
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
        camera.transform.position = cameraStartpos;
        canvas.enabled = true;
        isGameOver = false;
        GameStateManager.Singleton.RestartScene();
    }
}
