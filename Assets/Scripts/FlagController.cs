using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public LevelController level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        level.OnFlagCapture();
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
    }
}
