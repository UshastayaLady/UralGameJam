using UnityEngine;

public class PlayerTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var zone = collision.GetComponentInParent<ITriggerZone>();
        zone?.OnEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var zone = collision.GetComponentInParent<ITriggerZone>();
        zone?.OnExit();
    }
}
