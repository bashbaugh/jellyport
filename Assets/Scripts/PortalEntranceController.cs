using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEntranceController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.GetComponentInParent<PortalController>().OnPlayerEnter();
    }
}
