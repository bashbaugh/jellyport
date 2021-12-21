using UnityEngine;

public class PortalEntranceController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<PortalController>().OnPlayerEnter();
    }
}
