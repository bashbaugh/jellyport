using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public PlayerController player;
    public PortalController[] portals;
    public FlagController flag;

    public Sprite openExitSprite;
    private Sprite closedExitSprite;
    public GameObject exit;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        closedExitSprite = exit.GetComponent<SpriteRenderer>().sprite;
        foreach (PortalController p in portals)
        {
            p.player = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Restart();
            }
        }
    }

    public void OnFlagCapture ()
    {
        foreach (PortalController p in portals)
        {
            p.ActivatePortal();
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
        Debug.Log("Ded");
        gameOver = true;
    }
}
