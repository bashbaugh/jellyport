using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public PlayerController player;
    public Transform portalsParent;
    public FlagController flag;

    public Sprite openExitSprite;
    private Sprite closedExitSprite;
    public GameObject exit;

    public string nextLevelScene;

    private PortalController[] portals;

    private bool gameOver = false;
    private bool gameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        // Store a reference to all the portal controllers under the portals object:
        Array.Resize(ref portals, portalsParent.childCount);
        for (int i = 0; i < portalsParent.childCount; i++)
        {
            portals[i] = portalsParent.GetChild(i).GetComponent<PortalController>();
            portals[i].player = player;
        }

        closedExitSprite = exit.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gameOver)
            {
                Restart();
            }

            if (gameWon)
            {
                // Go to next level
                if (nextLevelScene != "") SceneManager.LoadScene(nextLevelScene);
                else SceneManager.LoadScene("LevelSelect");
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }

    public void OnFlagCapture ()
    {
        foreach (PortalController p in portals)
        {
            p.TriggerPortal();
        }


        player.OnFlagCaptured();

        exit.GetComponent<SpriteRenderer>().sprite = openExitSprite;
    }

    public void Restart ()
    {
        exit.GetComponent<SpriteRenderer>().sprite = closedExitSprite;
        player.Revive();
        flag.Reset();
        foreach (PortalController p in portals)
        {
            p.ResetPortal();
        }
        gameOver = false;
    }
    
    public void OnDie ()
    {
        gameOver = true;
    }

    public void OnWin ()
    {
        Debug.Log("Level won!");
        gameWon = true;
    }
}
