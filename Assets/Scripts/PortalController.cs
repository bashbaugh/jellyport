using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private GameObject entrance;
    private GameObject exit;

    public PlayerController player;

    public bool alwaysActive;

    public Sprite activeEntranceSprite;
    public Sprite activeExitSprite;
    private Sprite inactiveEntraceSprite;
    private Sprite inactiveExitSprite;

    private bool portalsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        entrance = transform.GetChild(0).gameObject;
        exit = transform.GetChild(1).gameObject;

        // Store a reference to the inactive sprites
        inactiveEntraceSprite = entrance.GetComponent<SpriteRenderer>().sprite;
        inactiveExitSprite = exit.GetComponent<SpriteRenderer>().sprite;

        if (alwaysActive) ActivatePortal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerEnter()
    {
        if (portalsActive) player.TeleportTo(exit.transform.position);
    }

    public void ActivatePortal()
    {
        portalsActive = true;
        entrance.GetComponent<SpriteRenderer>().sprite = activeEntranceSprite;
        exit.GetComponent<SpriteRenderer>().sprite = activeExitSprite;
    }

    void DeactivatePortal()
    {
        portalsActive = false;
        entrance.GetComponent<SpriteRenderer>().sprite = inactiveEntraceSprite;
        exit.GetComponent<SpriteRenderer>().sprite = inactiveExitSprite;
    }

    public void ResetPortal ()
    {
        if (!alwaysActive) DeactivatePortal();
    }
}
