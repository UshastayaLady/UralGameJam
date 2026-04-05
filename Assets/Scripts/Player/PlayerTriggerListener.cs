using UnityEngine;

public class PlayerTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var zone = collision.GetComponentInParent<ITriggerZoneEnter>();
        zone?.OnEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var zone = collision.GetComponentInParent<ITriggerZoneExit>();
        zone?.OnExit();
    }
}
